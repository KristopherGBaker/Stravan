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

namespace Stravan
{
    /// <summary>
    /// Service interface for rides in the Strava API
    /// </summary>
    public interface IRideService
    {
        /// <summary>
        /// Searches for Rides by Athlete, Club, date, or Id range. Returns a list of Rides. The response includes the Id and Name of each matching Ride. Rides are ordered by their start date.
        /// </summary>
        /// <param name="clubId">Optional. Id of the Club for which to search for member's Rides.</param>
        /// <param name="athleteId">Optional. Id of the Athlete for which to search for Rides.</param>
        /// <param name="athleteName">Optional. Username of the Athlete for which to search for Rides.</param>
        /// <param name="startDate">Optional. Day on which to start search for Rides. The date should be formatted YYYY-MM-DD. The date is the local time of when the ride started.</param>
        /// <param name="endDate">Optional. Day on which to end search for Rides. The date should be formatted YYYY-MM-DD. The date is the local time of when the ride started.</param>
        /// <param name="startId">Optional. Only return Rides with an Id greater than or equal to the startId.</param>
        /// <param name="offset">Optional. Any search will return at most 50 rows. To retrieve results after the 50th row use the offset parameter. For example, to retrieve rows 51-100 use an offset of 50.</param>
        /// <returns>List of matching rides</returns>
        List<Ride> Index(int? clubId = null, int? athleteId = null, string athleteName = null, DateTime? startDate = null, DateTime? endDate = null, int? startId = null, int? offset = null);

        /// <summary>
        /// Returns extended information for a Ride.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>Ride for the specified id with extended information.</returns>
        Ride Show(int id);

        /// <summary>
        /// Shows details about a specific ride.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>Ride for the specified id with extended information.</returns>
        RideV2 ShowV2(int id);

        /// <summary>
        /// Determines if a ride is synced.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <param name="version">Required. A 'synced' boolean is returned, which indicates whether or not the device has the current version of the ride.</param>
        /// <returns>true iff the ride is synced, false otherwise</returns>
        bool IsSyncedV2(int id, string version);

        /// <summary>
        /// Returns the list of lat/lng points to be used to generate a map of the ride.
        /// </summary>
        /// <param name="rideId">Required. The Id of the Ride.</param>
        /// <param name="token">Required. Authentication token.</param>
        /// <param name="threshold">Optional. If provided, the array of lat/lng points will be reduced to exclude "uninteresting" points within a tolerance of threshold meters.</param>
        /// <returns>The list of lat/lng points to be used to generate a map of the ride</returns>
        MapDetailV2 MapDetailsV2(int rideId, string token, int? threshold = null);

        /// <summary>
        /// Returns a list of Efforts on a Ride. The response includes the Id of the Effort, the elapsed time of the Effort, and the Id and Name of the Segment associated with the Effort. 
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>List of Efforts on the specified Ride</returns>
        RideEfforts Efforts(int id);

        /// <summary>
        /// Shows details the segment efforts on a specific ride.
        /// The elapsed_time and moving_time properties are returned as seconds.  The average_speed property is returned as meters/sec.  The elevation_gain and distance properties are returned in meters.
        /// </summary>
        /// <param name="id">Required. The id of the ride.</param>
        /// <returns>List of Efforts and Segments on the specified Ride</returns>
        RideEffortsV2 EffortsV2(int id);

        /// <summary>
        /// Returns information about a specific segment effort on a specific ride.
        /// There are 3 sections to the JSON response: 
        /// effort - Summary information about the effort (you already have this from /api/v2/rides/98715/efforts)
        /// segment - Information about the segment
        /// leaderboard - Information to populate the leaderboard. More on the leaderboard below...
        ///  
        /// The leadboard payload will have between 2 and 4 sections:
        /// rank - Rank of this effort compared to the best efforts of all other athletes on this segment. This property will always exist. If this isn't the athletes best effort (case #3) this rank will be "virtual" (e.g. you'll see it in parenthesis).
        /// best - This section will only exist in case #3. Best will contain information on the athlete's best effort (since this effort isn't their best). "best" will have 2 properties:
        /// rank - The rank of their best effort on the leaderboard.
        /// effort - Properties of this best effort
        /// prev_best - This section will only exist in case #2. Prev_best will contain information on the athlete's best effort before this effort beat it. It will contain the same properties as best above, except in this case "rank" refers to their old rank on the leaderboard. NOTE: it's possible for their time to be better but for the rank to remain the same, e.g. if they improved their time by 10 seconds, but there was no one on the leaderboard between their old time and new time, so their rank remains unchanged.
        /// context - Information on the "nearby" efforts on the leaderboard. This property will always exist. It's will be an array of items. Each item will have 3 properties:
        /// rank - The rank of this effort on the leaderboard.
        /// effort - Properties of this effort.
        /// athlete - Summary information about the athlete, e.g. name.
        ///  
        /// The context section contains the information you need to build out the leaderboard. There are a few corner cases with context. As mentioned it's possible for there to be fewer than 4 items in it (2 above, 2 below) if fewer than 4 other athletes have ridden the segment. It's possible for context to actually be empty if this effort is the first effort on this segment (no one has ridden it before). Finally, if this effort is within the top 5, I'll return the top 4, instead of 2 below and 2 above, e.g. if they are 5th I'll return efforts 1 thru 4. This is just to be able to show the top of the leaderboard if they're close. Since all the context efforts are ranked, you'll always put the current effort in it's spot within those rankings (it's possible for the current effort to be ranked the same as a context effort, see case #3).
        ///  
        /// There are 3 possible cases when viewing an effort. These different cases will determine what information is sent back in the leaderboard payload (and of course determines what is displayed).
        ///  
        /// This effort is the athlete's first on this segment.  In this case both the best and prev_best sections won't exist. 
        /// This effort is a new best (or it ties their current best). In this case there will be no best section in the leaderboard. The rank will be an actual rank on the leaderboard. Context will contain the 2 efforts above it and below it on the leaderboard. If this isn't my first effort (not case #3) then prev_best will be populated.
        /// This effort is slower than their current best. In this case their will always be a best section. therefore the rank will be a "virtual" rank on the leaderboard. Context will still contain the 2 efforts above it and below it, but the first one below it will have the same rank. When ordering the leaderboard you'll always put the new effort above the effort in context with the same rank.
        /// </summary>
        /// <param name="rideId">Required. The given segment effort's ride.</param>
        /// <param name="effortId">Required. The segment effort's id.</param>
        /// <returns>Returns information about a specific segment effort on a specific ride.</returns>
        EffortDetailsV2 ShowEffortsV2(int rideId, int effortId);
    }
}
