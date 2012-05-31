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

namespace Stravan
{
    /// <summary>
    /// Service interface for authentication in the Strava API 
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Logs the user into Strava. Authentication tokens never expire.
        /// </summary>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        Authentication Login(string email, string password);

        /// <summary>
        /// Logs the user into Strava. Authentication tokens never expire.
        /// </summary>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <param name="agreedToTerms">Optional.  If set to 'true', will update the agreed to terms field on this athlete with the current date.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        AuthenticationV2 LoginV2(string email, string password, bool? agreedToTerms = null);

        /// <summary>
        /// Creates a new user on Strava. The response is the athlete login token.
        /// </summary>
        /// <param name="firstName">Required. The user's first name.</param>
        /// <param name="lastName">Required The user's last name.</param>
        /// <param name="email">Required. The user's email address.</param>
        /// <param name="password">Required. The user's password.</param>
        /// <param name="latlng">Required. The user's location.</param>
        /// <returns>Authentication information including token if successful, or error message if not</returns>
        Authentication SignUp(string firstName, string lastName, string email, string password, string latlng);

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
        AuthenticationV2 SignUpV2(string firstName, string lastName, string email, string password, double latitude, double longitude, string attributes = null);
    }
}
