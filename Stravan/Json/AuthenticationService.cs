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
    /// Service implementation for authentication in the Strava API 
    /// </summary>
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        /// <summary>
        /// Initializes an AuthenticationService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public AuthenticationService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Logs the user into Strava. Authentication tokens never expire.
        /// </summary>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        public Authentication Login(string email, string password)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (email.Length == 0)
                throw new ArgumentException("email", "email cannot be empty");

            if (password == null)
                throw new ArgumentNullException("password");

            if (password.Length == 0)
                throw new ArgumentException("password", "password cannot be empty");

            var parameters = new NameValueCollection { { "email", email }, { "password", password } };

            var response = Client.Download("authentication/login", postParameters: parameters, isSecure: true);

            return JsonDeserializer.Deserialize<Authentication>(response);
        }

        /// <summary>
        /// Logs the user into Strava. Authentication tokens never expire.
        /// </summary>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <param name="agreedToTerms">Optional.  If set to 'true', will update the agreed to terms field on this athlete with the current date.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        public AuthenticationV2 LoginV2(string email, string password, bool? agreedToTerms = null)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (email.Length == 0)
                throw new ArgumentException("email", "email cannot be empty");

            if (password == null)
                throw new ArgumentNullException("password");

            if (password.Length == 0)
                throw new ArgumentException("password", "password cannot be empty");

            var parameters = new NameValueCollection { { "email", email }, { "password", password } };

            if (agreedToTerms.HasValue)
            {
                parameters.Add("agreed_to_terms", agreedToTerms.Value.ToString().ToLower());
            }

            var response = Client.Download("authentication/login", postParameters: parameters, isSecure: true, versionType: ApiVersionType.VersionTwo);

            return JsonDeserializer.Deserialize<AuthenticationV2>(response);
        }

        /// <summary>
        /// Creates a new user on Strava. The response is the athlete login token.
        /// </summary>
        /// <param name="firstName">Required. The user's first name.</param>
        /// <param name="lastName">Required The user's last name.</param>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <param name="latlng">Required. The user's location.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        public Authentication SignUp(string firstName, string lastName, string email, string password, string latlng)
        {
            if (firstName == null)
                throw new ArgumentNullException("firstName");

            if (firstName.Length == 0)
                throw new ArgumentException("firstName", "firstName cannot be empty");

            if (lastName == null)
                throw new ArgumentNullException("lastName");

            if (lastName.Length == 0)
                throw new ArgumentException("lastName", "lastName cannot be empty");

            if (email == null)
                throw new ArgumentNullException("email");

            if (email.Length == 0)
                throw new ArgumentException("email", "email cannot be empty");

            if (password == null)
                throw new ArgumentNullException("password");

            if (password.Length == 0)
                throw new ArgumentException("password", "password cannot be empty");

            if (latlng == null)
                throw new ArgumentNullException("latlng");

            if (latlng.Length == 0)
                throw new ArgumentException("latlng", "latlng cannot be empty");

            var parameters = new NameValueCollection 
            { 
                { "firstname", firstName }, { "lastname", lastName }, 
                { "email", email }, { "password", password },
                { "latlng", latlng }
            };

            var response = Client.Download("authentication/signup", postParameters: parameters, isSecure: true);

            return JsonDeserializer.Deserialize<Authentication>(response);
        }

        /// <summary>
        /// Creates a new user on Strava. The response is the athlete login token.
        /// </summary>
        /// <param name="firstName">Required. The user's first name.</param>
        /// <param name="lastName">Required The user's last name.</param>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <param name="latitude">Required. The latitude component of the user's location.</param>
        /// <param name="longitude">Required. The longitude component of the user's location.</param>
        /// <param name="attributes">Optional. Additional athlete attributes to set on athlete creation, e.g. 'sex' </param>
        /// <returns></returns>
        public AuthenticationV2 SignUpV2(string firstName, string lastName, string email, string password, double latitude, double longitude, string attributes = null)
        {
            if (firstName == null)
                throw new ArgumentNullException("firstName");

            if (firstName.Length == 0)
                throw new ArgumentException("firstName", "firstName cannot be empty");

            if (lastName == null)
                throw new ArgumentNullException("lastName");

            if (lastName.Length == 0)
                throw new ArgumentException("lastName", "lastName cannot be empty");

            if (email == null)
                throw new ArgumentNullException("email");

            if (email.Length == 0)
                throw new ArgumentException("email", "email cannot be empty");

            if (password == null)
                throw new ArgumentNullException("password");

            if (password.Length == 0)
                throw new ArgumentException("password", "password cannot be empty");

            var parameters = new NameValueCollection 
            { 
                { "firstname", firstName }, { "lastname", lastName }, 
                { "email", email }, { "password", password },
                { "latitude", latitude.ToString() }, { "longitude", longitude.ToString() }
            };

            if (!string.IsNullOrWhiteSpace(attributes))
            {
                parameters.Add("attributes", attributes);
            }

            var response = Client.Download("authentication/signup", postParameters: parameters, isSecure: true, versionType: ApiVersionType.VersionTwo);

            return JsonDeserializer.Deserialize<AuthenticationV2>(response);
        }
    }
}
