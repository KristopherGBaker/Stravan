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
using EmpyrealNight.Core.Json;

namespace Stravan.Json
{
    /// <summary>
    /// Service implementation for athletes in the Strava API 
    /// </summary>
    public class AthleteService : BaseService, IAthleteService
    {
        /// <summary>
        /// Initializes an AuthenticationService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public AthleteService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Shows details about a specific athlete, including his device settings. 
        /// </summary>
        /// <param name="token">Required. The login token.</param>
        /// <param name="id">Required. The id of the athlete.</param>
        /// <returns></returns>
        public Athlete Show(string token, int id)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            if (token.Length == 0)
                throw new ArgumentException("token", "token cannot be empty");

            if (id <= 0)
                throw new ArgumentException("id", "athlete id must be greater than 0");

            var parameters = new NameValueCollection { { "token", token } };

            var response = Client.Download(string.Format("athletes/{0}", id), parameters, versionType: ApiVersionType.VersionTwo);

            return JsonDeserializer.Deserialize<Athlete>(response);
        }

        /// <summary>
        /// Updates details about a specific athlete.
        /// </summary>
        /// <param name="token">Required.  The login token.</param>
        /// <param name="id">Required.  The the id of the athlete.</param>
        /// <param name="firstname">Required. The updated first name of the athlete.</param>
        /// <param name="lastname">Required. The updated last name of the athlete.</param>
        /// <returns></returns>
        public Athlete Update(string token, int id, string firstname, string lastname)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            if (token.Length == 0)
                throw new ArgumentException("token", "token cannot be empty");

            if (id <= 0)
                throw new ArgumentException("id", "athlete id must be greater than 0");

            if (firstname == null)
                throw new ArgumentNullException("firstname");

            if (firstname.Length == 0)
                throw new ArgumentException("firstname", "firstname cannot be empty");

            if (lastname == null)
                throw new ArgumentNullException("lastname");

            if (lastname.Length == 0)
                throw new ArgumentException("lastname", "lastname cannot be empty");

            var requestJson = JsonSerializer.Serialize(new { token, id, athlete = new { firstname, lastname } });

            var response = Client.UploadJson(string.Format("athletes/{0}", id), requestJson, isSecure: true, versionType: ApiVersionType.VersionTwo);
            return JsonDeserializer.Deserialize<Athlete>(response);
        }
    }
}
