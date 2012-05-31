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
using System.Collections.Generic;
using System.Collections.Specialized;
using EmpyrealNight.Core.Json;

namespace Stravan.Json
{
    /// <summary>
    /// Service implementation for clubs in the Strava API
    /// </summary>
    public class ClubService : BaseService, IClubService
    {
        /// <summary>
        /// Initializes a ClubService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public ClubService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Searches Clubs by name. Returns a list of Clubs. The response includes the Id and Name of each matching Club.
        /// </summary>
        /// <param name="name">Required. Part of the name of the Club to match.</param>
        /// <returns>List of clubs matching the name</returns>
        public List<Club> Index(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Length == 0)
                throw new ArgumentException("name", "name cannot be empty");

            var parameters = new NameValueCollection { { "name", name } };

            var response = Client.Download("clubs", parameters);

            return JsonDeserializer.Deserialize<ClubsWrapper>(response).Clubs;
        }

        /// <summary>
        /// Returns extended information for a Club.
        /// </summary>
        /// <param name="id">Required. The Id of the Club.</param>
        /// <returns>Club for the specified id with extended information.</returns>
        public Club Show(int id)
        {
            var response = Client.Download(string.Format("clubs/{0}", id));

            return JsonDeserializer.Deserialize<ClubWrapper>(response).Club;
        }

        /// <summary>
        /// Returns a list of members (Athletes) in a Club. The response includes the Id and Name of each Athlete whose a member of the Club. 
        /// </summary>
        /// <param name="id">Required. The Id of the Club.</param>
        /// <returns>Club and list of its members.</returns>
        public ClubMembers Members(int id)
        {
            var response = Client.Download(string.Format("clubs/{0}/members", id));

            return JsonDeserializer.Deserialize<ClubMembers>(response);
        }

        #region wrapper classes

        /// <summary>
        /// Wrapper class for a list of clubs
        /// </summary>
        protected class ClubsWrapper
        {
            /// <summary>
            /// Gets or sets the list of clubs
            /// </summary>
            [JsonName("clubs")]
            public List<Club> Clubs { get; set; }
        }

        /// <summary>
        /// Wrapper class for a club
        /// </summary>
        protected class ClubWrapper
        {
            /// <summary>
            /// Gets or sets the club
            /// </summary>
            [JsonName("club")]
            public Club Club { get; set; }
        }

        #endregion
    }
}
