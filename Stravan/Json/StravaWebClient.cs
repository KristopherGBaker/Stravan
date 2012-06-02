#region MIT License

/*
 * Copyright (c) 2012 Kristopher Baker (kris@empyrealnight.com)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

#endregion

using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using Stravan.Configuration;
using log4net;

namespace Stravan.Json
{
    /// <summary>
    /// Implementation of IStravaWebClient using System.Net.WebClient for connecting to the actual Strava API, 
    /// and concurrent collections and semaphores for resource control.
    /// </summary>
    public class StravaWebClient : IStravaWebClient
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(StravaWebClient));

        /// <summary>
        /// Size of the pool for web clients
        /// </summary>
        private static readonly int PoolSize = Config.Stravan.Client.PoolSize;

        /// <summary>
        /// Semaphore for limiting the number of concurrent web clients
        /// </summary>
        private static readonly Semaphore WebClientPool = new Semaphore(PoolSize, PoolSize);

        /// <summary>
        /// Base url for the Strava API v1
        /// </summary>
        private static readonly string BaseUrlV1 = Config.Stravan.Api.Versions["1"].BaseUrl;

        /// <summary>
        /// Secure base url for the Strava API v1
        /// </summary>
        private static readonly string SecureBaseUrlV1 = Config.Stravan.Api.Versions["1"].SecureBaseUrl;

        /// <summary>
        /// Base url for the Strava API v2
        /// </summary>
        private static readonly string BaseUrlV2 = Config.Stravan.Api.Versions["2"].BaseUrl;

        /// <summary>
        /// Secure base url for the Strava API v1
        /// </summary>
        private static readonly string SecureBaseUrlV2 = Config.Stravan.Api.Versions["2"].SecureBaseUrl;

        /// <summary>
        /// Static constructor
        /// </summary>
        static StravaWebClient()
        {
        }

        /// <summary>
        /// Downloads and returns the response as a json string for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="postParameters">post parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        public string Download(string action, NameValueCollection querystringParameters = null, NameValueCollection postParameters = null,
            bool isSecure = false, ApiVersionType versionType = ApiVersionType.VersionOne)
        {
            var data = DownloadData(action, querystringParameters, postParameters, isSecure, versionType);

            return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        /// <summary>
        /// Downloads and returns the response as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="postParameters">post parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>byte array response for the specified action</returns>
        public byte[] DownloadData(string action, NameValueCollection querystringParameters = null, NameValueCollection postParameters = null,
            bool isSecure = false, ApiVersionType versionType = ApiVersionType.VersionOne)
        {
            return MakeRequest(action, querystringParameters, isSecure, versionType,
                (url, client) => postParameters != null ? client.UploadValues(url, postParameters) : client.DownloadData(url));
        }

        /// <summary>
        /// Uploads json and returns the respon as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="json">Json to upload</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        public string UploadJson(string action, string json, NameValueCollection querystringParameters = null, bool isSecure = false,
            ApiVersionType versionType = ApiVersionType.VersionOne)
        {
            var data = UploadJsonData(action, Encoding.UTF8.GetBytes(json), querystringParameters, isSecure, versionType);

            return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        /// <summary>
        /// Uploads json and returns the respon as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="jsonData">Json to upload</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        public byte[] UploadJsonData(string action, byte[] jsonData, NameValueCollection querystringParameters = null, bool isSecure = false,
            ApiVersionType versionType = ApiVersionType.VersionOne)
        {
            return MakeRequest(action, querystringParameters, isSecure, versionType, 
                (url, client) => client.UploadData(url, "POST", jsonData));
        }

        private byte[] MakeRequest(string action, NameValueCollection querystringParameters, bool isSecure,
            ApiVersionType versionType, Func<string, WebClient, byte[]> actualRequest)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            byte[] data = null;
            WebClient client = null;

            try
            {
                string baseUrl;
                if (versionType == ApiVersionType.VersionOne)
                {
                    baseUrl = isSecure ? SecureBaseUrlV1 : BaseUrlV1;
                }
                else
                {
                    baseUrl = isSecure ? SecureBaseUrlV2 : BaseUrlV2;
                }

                var url = string.Concat(baseUrl, action);
                client = GetClient();
                client.QueryString = querystringParameters ?? new NameValueCollection();

                data = actualRequest(url, client);
            }
            catch (WebException webException)
            {
                if (Log.IsDebugEnabled)
                    Log.Debug(webException.Message, webException);

                throw new ApiException(webException.Message);
            }
            finally
            {
                if (client != null)
                    ReleaseClient(client);
            }

            return data;
        }

        /// <summary>
        /// Gets a new web client.  If no client allocations are available, the method will wait until one is available.
        /// </summary>
        /// <returns>Web client from the pool.</returns>
        private static WebClient GetClient()
        {
            WebClientPool.WaitOne();
            return new WebClient();
        }

        /// <summary>
        /// Releases a web client.
        /// </summary>
        /// <param name="client">Web client to release.</param>
        private static void ReleaseClient(WebClient client)
        {
            WebClientPool.Release();
            client.Dispose();
        }
    }
}
