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

namespace Stravan
{
    public enum ApiVersionType
    {
        VersionOne,
        VersionTwo
    }

    /// <summary>
    /// Interface the services use for connecting to the actual Strava API.
    /// </summary>
    public interface IStravaWebClient
    {
        /// <summary>
        /// Downloads and returns the response as a json string for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="postParameters">post parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        string Download(string action, NameValueCollection querystringParameters = null, NameValueCollection postParameters = null,
            bool isSecure = false, ApiVersionType versionType = ApiVersionType.VersionOne);

        /// <summary>
        /// Downloads and returns the response as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="postParameters">post parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>byte array response for the specified action</returns>
        byte[] DownloadData(string action, NameValueCollection querystringParameters = null, NameValueCollection postParameters = null,
            bool isSecure = false, ApiVersionType versionType = ApiVersionType.VersionOne);

        /// <summary>
        /// Uploads json and returns the respon as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="json">Json to upload</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        string UploadJson(string action, string json, NameValueCollection querystringParameters = null, bool isSecure = false,
            ApiVersionType versionType = ApiVersionType.VersionOne);

        /// <summary>
        /// Uploads json and returns the respon as a byte array for the specified action and parameters
        /// </summary>
        /// <param name="action">Strava API action to download the response for</param>
        /// <param name="jsonData">Json to upload</param>
        /// <param name="querystringParameters">querystring parameters for the specified action</param>
        /// <param name="isSecure">Boolean indicating if the download should be over SSL</param>
        /// <returns>Json response for the specified action</returns>
        byte[] UploadJsonData(string action, byte[] jsonData, NameValueCollection querystringParameters = null, bool isSecure = false, 
            ApiVersionType versionType = ApiVersionType.VersionOne);
    }
}
