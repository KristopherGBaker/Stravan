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
    /// Service implementation for segments in the Strava API
    /// </summary>
    public class SegmentService : BaseService, ISegmentService
    {
        /// <summary>
        /// Date format for start/end dates
        /// </summary>
        private const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Initializes a SegmentService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public SegmentService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Searches for Segments by name. Returns a list of Segments. The response includes the Id and Name of each matching Segment.
        /// </summary>
        /// <param name="name">Required. Part of the name of the Segment to match.</param>
        /// <param name="offset">Optional. Any search will return at most 50 rows. To retrieve results after the 50th row use the offset parameter. For example, to retrieve rows 51-100 use an offset of 50.</param>
        /// <returns></returns>
        public List<Segment> Index(string name, int? offset = null)
        {
            var parameters = new NameValueCollection { { "name", name } };

            if (offset.HasValue && offset.Value > 0)
            {
                parameters.Add("offset", offset.Value.ToString());
            }

            var response = Client.Download("segments", parameters);

            return JsonDeserializer.Deserialize<SegmentsWrapper>(response).Segments;
        }

        /// <summary>
        /// Returns extended information for a Segment.
        /// </summary>
        /// <param name="id">Required. The Id of the Effort.</param>
        /// <returns>Segment for the specified id with extended information.</returns>
        public Segment Show(int id)
        {
            var response = Client.Download(string.Format("segments/{0}", id));

            return JsonDeserializer.Deserialize<SegmentWrapper>(response).Segment;
        }

        /// <summary>
        /// Returns a list of matching Efforts on a Segment. The response includes the Id of the Effort, the elapsed time of the Effort, and the Id, Name, and Username of the Athlete who ride the Effort. Efforts are ordered by start date, oldest to newest.
        /// </summary>
        /// <param name="segmentId">Required. The Id of the Segment.</param>
        /// <param name="clubId">Optional. Id of the Club for which to search for member's Efforts.</param>
        /// <param name="athleteId">Optional. Id of the Athlete for which to search for Efforts.</param>
        /// <param name="athleteName">Optional. Username of the Athlete for which to search for Rides.</param>
        /// <param name="startDate">Optional. Day on which to start search for Efforts. The date is the local time of when the effort started.</param>
        /// <param name="endDate">Optional. Day on which to end search for Efforts. The date is the local time of when the effort started.</param>
        /// <param name="startId">Optional. Only return Effforts with an Id greater than or equal to the startId.</param>
        /// <param name="isBest">Optional. Shows an best efforts per athlete sorted by elapsed time ascending (segment leaderboard).</param>
        /// <returns>List of matching Efforts on a Segment.</returns>
        public SegmentEfforts Efforts(int segmentId, int? clubId = null, int? athleteId = null, string athleteName = null, DateTime? startDate = null, DateTime? endDate = null, int? startId = null, bool? isBest = null)
        {
            var parameters = new NameValueCollection();

            if (clubId.HasValue && clubId.Value > 0)
            {
                parameters.Add("clubId", clubId.Value.ToString());
            }

            if (athleteId.HasValue && athleteId.Value > 0)
            {
                parameters.Add("athleteId", athleteId.Value.ToString());
            }

            if (!string.IsNullOrWhiteSpace(athleteName))
            {
                parameters.Add("athleteName", athleteName);
            }

            if (startDate.HasValue)
            {
                parameters.Add("startDate", startDate.Value.ToString(DateFormat));
            }

            if (endDate.HasValue)
            {
                parameters.Add("endDate", endDate.Value.ToString(DateFormat));
            }

            if (startId.HasValue && startId.Value > 0)
            {
                parameters.Add("startId", startId.Value.ToString());
            }

            if (isBest.HasValue)
            {
                parameters.Add("best", isBest.Value.ToString().ToLower());
            }

            var response = Client.Download(string.Format("segments/{0}/efforts", segmentId), parameters);

            return JsonDeserializer.Deserialize<SegmentEfforts>(response);
        }

        #region wrapper classes

        /// <summary>
        /// Wrapper class for a list of segments
        /// </summary>
        protected class SegmentsWrapper
        {
            /// <summary>
            /// Gets or sets the list of segments
            /// </summary>
            [JsonName("segments")]
            public List<Segment> Segments { get; set; }
        }

        /// <summary>
        /// Wrapper class for a segment
        /// </summary>
        protected class SegmentWrapper
        {
            /// <summary>
            /// Gets or sets the segment
            /// </summary>
            [JsonName("segment")]
            public Segment Segment { get; set; }
        }

        #endregion
    }
}
