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

using System.Collections.Generic;

namespace Stravan
{
    /// <summary>
    /// Service interface for clubs in the Strava API
    /// </summary>
    public interface IClubService
    {
        /// <summary>
        /// Searches Clubs by name. Returns a list of Clubs. The response includes the Id and Name of each matching Club.
        /// </summary>
        /// <param name="name">Required. Part of the name of the Club to match.</param>
        /// <returns>List of clubs matching the name</returns>
        List<Club> Index(string name);

        /// <summary>
        /// Returns extended information for a Club.
        /// </summary>
        /// <param name="id">Required. The Id of the Club.</param>
        /// <returns>Club for the specified id with extended information.</returns>
        Club Show(int id);

        /// <summary>
        /// Returns a list of members (Athletes) in a Club. The response includes the Id and Name of each Athlete whose a member of the Club. 
        /// </summary>
        /// <param name="id">Required. The Id of the Club.</param>
        /// <returns>Club and list of its members.</returns>
        ClubMembers Members(int id);
    }
}
