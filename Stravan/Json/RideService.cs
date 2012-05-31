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
using EmpyrealNight.Core;
using EmpyrealNight.Core.Json;

namespace Stravan.Json
{
    /// <summary>
    /// Service implementation for rides in the Strava API
    /// </summary>
    public class RideService : BaseService, IRideService
    {
        /// <summary>
        /// Date format for start/end dates
        /// </summary>
        private const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Initializes a RideService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public RideService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Searches for Rides by Athlete, Club, date, or Id range. Returns a list of Rides. The response includes the Id and Name of each matching Ride. Rides are ordered by their start date.
        /// </summary>
        /// <param name="clubId">Optional. Id of the Club for which to search for member's Rides.</param>
        /// <param name="athleteId">Optional. Id of the Athlete for which to search for Rides.</param>
        /// <param name="athleteName">Optional. Username of the Athlete for which to search for Rides.</param>
        /// <param name="startDate">Optional. Day on which to start search for Rides. The date is the local time of when the ride started.</param>
        /// <param name="endDate">Optional. Day on which to end search for Rides. The date is the local time of when the ride started.</param>
        /// <param name="startId">Optional. Only return Rides with an Id greater than or equal to the startId.</param>
        /// <param name="offset">Optional. Any search will return at most 50 rows. To retrieve results after the 50th row use the offset parameter. For example, to retrieve rows 51-100 use an offset of 50.</param>
        /// <returns>List of matching rides</returns>
        public List<Ride> Index(int? clubId = null, int? athleteId = null, string athleteName = null, DateTime? startDate = null, DateTime? endDate = null, int? startId = null, int? offset = null)
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

            if (offset.HasValue && offset.Value > 0)
            {
                parameters.Add("offset", offset.Value.ToString());
            }

            var response = Client.Download("rides", parameters);

            return JsonDeserializer.Deserialize<RidesWrapper>(response).Rides;
        }

        /// <summary>
        /// Returns extended information for a Ride.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>Ride for the specified id with extended information.</returns>
        public Ride Show(int id)
        {
            var response = Client.Download(string.Format("rides/{0}", id));

            return JsonDeserializer.Deserialize<RideWrapper>(response).Ride;
        }

        /// <summary>
        /// Shows details about a specific ride.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>Ride for the specified id with extended information.</returns>
        public RideV2 ShowV2(int id)
        {
            var response = Client.Download(string.Format("rides/{0}", id), versionType: ApiVersionType.VersionTwo);
            var rideWrapper = JsonDeserializer.Deserialize<RideV2Wrapper>(response);
            var ride = rideWrapper.Ride.ToRideV2();
            ride.Version = rideWrapper.Version;
            return ride;
        }

        /// <summary>
        /// Determines if a ride is synced.
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <param name="version">Required. A 'synced' boolean is returned, which indicates whether or not the device has the current version of the ride.</param>
        /// <returns>true iff the ride is synced, false otherwise</returns>
        public bool IsSyncedV2(int id, string version)
        {
            if (version == null)
                throw new ArgumentNullException("version");

            if (version.Length == 0)
                throw new ArgumentException("version", "version cannot be empty");

            var parameters = new NameValueCollection { { "version", version } };
            var response = Client.Download(string.Format("rides/{0}", id), parameters, versionType: ApiVersionType.VersionTwo);

            var rideWrapper = JsonDeserializer.Deserialize<RideV2Wrapper>(response);
            return rideWrapper.IsSynced;
        }

        /// <summary>
        /// Returns the list of lat/lng points to be used to generate a map of the ride.
        /// </summary>
        /// <param name="rideId">Required. The Id of the Ride.</param>
        /// <param name="token">Required. Authentication token.</param>
        /// <param name="threshold">Optional. If provided, the array of lat/lng points will be reduced to exclude "uninteresting" points within a tolerance of threshold meters.</param>
        /// <returns>The list of lat/lng points to be used to generate a map of the ride</returns>
        public MapDetailV2 MapDetailsV2(int rideId, string token, int? threshold = null)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            if (token.Length == 0)
                throw new ArgumentException("token", "token cannot be empty");

            var parameters = new NameValueCollection { { "token", token } };

            if (threshold.HasValue)
            {
                parameters.Add("threshold", threshold.Value.ToString());
            }

            var response = Client.Download(string.Format("rides/{0}/map_details", rideId), parameters, versionType: ApiVersionType.VersionTwo);

            var mapWrapper = JsonDeserializer.Deserialize<MapDetailsWrapper>(response);
            return mapWrapper.ToMapDetail();
        }

        /// <summary>
        /// Returns a list of Efforts on a Ride. The response includes the Id of the Effort, the elapsed time of the Effort, and the Id and Name of the Segment associated with the Effort. 
        /// </summary>
        /// <param name="id">Required. The Id of the Ride.</param>
        /// <returns>List of Efforts on the specified Ride</returns>
        public RideEfforts Efforts(int id)
        {
            var response = Client.Download(string.Format("rides/{0}/efforts", id));

            return JsonDeserializer.Deserialize<RideEfforts>(response);
        }

        /// <summary>
        /// Shows details the segment efforts on a specific ride.
        /// The elapsed_time and moving_time properties are returned as seconds.  The average_speed property is returned as meters/sec.  The elevation_gain and distance properties are returned in meters.
        /// </summary>
        /// <param name="id">Required. The id of the ride.</param>
        /// <returns>List of Efforts and Segments on the specified Ride</returns>
        public RideEffortsV2 EffortsV2(int id)
        {
            var response = Client.Download(string.Format("rides/{0}/efforts", id), versionType: ApiVersionType.VersionTwo);

            var rideEffortWrapper = JsonDeserializer.Deserialize<RideEffortsV2Wrapper>(response);
            return rideEffortWrapper.ToRideEfforts();
        }

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
        public EffortDetailsV2 ShowEffortsV2(int rideId, int effortId)
        {
            var response = Client.Download(string.Format("rides/{0}/efforts/{1}", rideId, effortId), versionType: ApiVersionType.VersionTwo);

            var effortDetailsWrapper = JsonDeserializer.Deserialize<EffortDetailsV2Wrapper>(response);
            return effortDetailsWrapper.ToEffortDetails();
        }

        #region wrapper classes

        /// <summary>
        /// Wrapper class for EffortDetailsV2
        /// </summary>
        protected class EffortDetailsV2Wrapper
        {
            /// <summary>
            /// Gets or sets the effort
            /// </summary>
            [JsonName("effort")]
            public EffortV2 Effort { get; set; }

            /// <summary>
            /// Gets or sets the segment
            /// </summary>
            [JsonName("segment")]
            public SegmentDetailsV2Wrapper Segment { get; set; }

            /// <summary>
            /// Gets or sets the leaderboard
            /// </summary>
            [JsonName("leaderboard")]
            public LeaderboardV2 Leaderboard { get; set; }

            public EffortDetailsV2 ToEffortDetails()
            {
                var effort = new EffortDetailsV2
                {
                    Effort = Effort,
                    Leaderboard = Leaderboard
                };

                if (Segment != null)
                {
                    effort.Segment = Segment.ToSegmentDetails();
                }

                return effort;
            }
        }

        /// <summary>
        /// Wrapper class for SegmentDetailsV2
        /// </summary>
        protected class SegmentDetailsV2Wrapper
        {
            /// <summary>
            /// Gets or sets the start coordinate
            /// </summary>
            [JsonName("start_latlng")]
            public double[] StartCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the end coordinate
            /// </summary>
            [JsonName("end_latlng")]
            public double[] EndCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the climb category
            /// </summary>
            [JsonName("climb_category")]
            public int ClimbCategory { get; set; }

            /// <summary>
            /// Gets or sets the number of times ridden
            /// </summary>
            [JsonName("num_times_ridden")]
            public int TimeRidden { get; set; }

            /// <summary>
            /// Gets or sets the number of riders ridden
            /// </summary>
            [JsonName("num_riders_ridden")]
            public int RidersRidden { get; set; }

            /// <summary>
            /// Gets or sets the name
            /// </summary>
            [JsonName("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [JsonName("id")]
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the elevation difference
            /// </summary>
            [JsonName("elev_difference")]
            public double ElevationDifference { get; set; }

            /// <summary>
            /// Gets or sets the kom
            /// </summary>
            [JsonName("kom")]
            public LeaderV2 KingOfMountain { get; set; }

            public SegmentDetailsV2 ToSegmentDetails()
            {
                var seg = new SegmentDetailsV2
                {
                    KingOfMountain = KingOfMountain,
                    ClimbCategory = ClimbCategory,
                    TimeRidden = TimeRidden,
                    RidersRidden = RidersRidden,
                    Name = Name,
                    Id = Id,
                    ElevationDifference = ElevationDifference
                };

                if (StartCoordinate != null && StartCoordinate.Length == 2)
                {
                    seg.StartCoordinate = new Coordinate { Latitude = StartCoordinate[0], Longitude = StartCoordinate[1] };
                }

                if (EndCoordinate != null && EndCoordinate.Length == 2)
                {
                    seg.EndCoordinate = new Coordinate { Latitude = EndCoordinate[0], Longitude = EndCoordinate[1] };
                }

                return seg;
            }
        }

        /// <summary>
        /// Wrapper class for a list of rides
        /// </summary>
        protected class RidesWrapper
        {
            /// <summary>
            /// Gets or sets the list of rides
            /// </summary>
            [JsonName("rides")]
            public List<Ride> Rides { get; set; }
        }

        /// <summary>
        /// Wrapper class for a ride
        /// </summary>
        protected class RideWrapper
        {
            /// <summary>
            /// Gets or sets the ride
            /// </summary>
            [JsonName("ride")]
            public Ride Ride { get; set; }
        }

        /// <summary>
        /// Wrapper class for a V2 ride
        /// </summary>
        protected class RideV2Wrapper
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [JsonName("id")]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the ride
            /// </summary>
            [JsonName("ride")]
            public RideV2Temp Ride { get; set; }

            /// <summary>
            /// Gets or sets the version
            /// </summary>
            [JsonName("version")]
            public string Version { get; set; }

            /// <summary>
            /// Gets or sets the synced boolean
            /// </summary>
            [JsonName("synced")]
            public bool IsSynced { get; set; }

        }

        /// <summary>
        /// Wrapper class for a V2 ride
        /// </summary>
        protected class RideV2Temp
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [JsonName("id")]
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name
            /// </summary>
            [JsonName("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the start date local
            /// </summary>
            [JsonName("start_date_local")]
            public string StartDateLocal { get; set; }

            /// <summary>
            /// Gets or sets the elapsed time
            /// </summary>
            [JsonName("elapsed_time")]
            public int ElapsedTime { get; set; }

            /// <summary>
            /// Gets or sets the moving time
            /// </summary>
            [JsonName("moving_time")]
            public int MovingTime { get; set; }

            /// <summary>
            /// Gets or sets the distance
            /// </summary>
            [JsonName("distance")]
            public double Distance { get; set; }

            /// <summary>
            /// Gets or sets the average speed
            /// </summary>
            [JsonName("average_speed")]
            public double AverageSpeed { get; set; }

            /// <summary>
            /// Gets or sets the elevation gain
            /// </summary>
            [JsonName("elevation_gain")]
            public double ElevationGain { get; set; }

            /// <summary>
            /// Gets or sets the location
            /// </summary>
            [JsonName("location")]
            public string Location { get; set; }

            /// <summary>
            /// Gets or sets the start coordinate
            /// </summary>
            [JsonName("start_latlng")]
            public double[] StartCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the end coordinate
            /// </summary>
            [JsonName("end_latlng")]
            public double[] EndCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the version
            /// </summary>
            [JsonName("version")]
            public string Version { get; set; }

            public RideV2 ToRideV2()
            {
                var ride = new RideV2
                {
                    Id = Id,
                    Name = Name,
                    StartDateLocal = StartDateLocal,
                    ElapsedTime = ElapsedTime,
                    MovingTime = MovingTime,
                    Distance = Distance,
                    AverageSpeed = AverageSpeed,
                    ElevationGain = ElevationGain,
                    Location = Location,
                    Version = Version
                };

                if (StartCoordinate != null && StartCoordinate.Length == 2)
                {
                    ride.StartCoordinate = new Coordinate { Latitude = StartCoordinate[0], Longitude = StartCoordinate[1] };
                }

                if (EndCoordinate != null && EndCoordinate.Length == 2)
                {
                    ride.EndCoordinate = new Coordinate { Latitude = EndCoordinate[0], Longitude = EndCoordinate[1] };
                }

                return ride;
            }
        }

        /// <summary>
        /// Wrapper class for a list of coordinates (latitude/longitude pairs)
        /// </summary>
        protected class CoordinatesWrapper
        {
            [JsonName("latlng")]
            public List<Coordinate> Coordinates { get; set; }
        }

        /// <summary>
        /// Wrapper class for map details v2
        /// </summary>
        protected class MapDetailsWrapper
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [JsonName("id")]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the coordinates
            /// </summary>
            [JsonName("latlng")]
            public List<List<double>> Coordinates { get; set; }

            /// <summary>
            /// Gets or sets the version
            /// </summary>
            [JsonName("version")]
            public string Version { get; set; }

            public MapDetailV2 ToMapDetail()
            {
                var map = new MapDetailV2
                {
                    RideId = ConvertWrapper.ToInt32(Id),
                    Version = Version,
                    Coordinates = new List<Coordinate>()
                };

                if (Coordinates != null)
                {
                    Coordinates.Each(pair =>
                    {
                        if (pair.Count == 2)
                        {
                            map.Coordinates.Add(new Coordinate { Latitude = pair[0], Longitude = pair[1] });
                        }
                    });
                }

                return map;
            }
        }

        /// <summary>
        /// Wrapper class for RideEffortsV2
        /// </summary>
        protected class RideEffortsV2Wrapper
        {
            /// <summary>
            /// Gets or sets the ride id
            /// </summary>
            [JsonName("id")]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the effort-segment list
            /// </summary>
            [JsonName("efforts")]
            public List<EffortSegmentV2Wrapper> Efforts { get; set; }

            /// <summary>
            /// Gets or sets the version
            /// </summary>
            [JsonName("version")]
            public string Version { get; set; }

            public RideEffortsV2 ToRideEfforts()
            {
                var rideEffort = new RideEffortsV2
                {
                    RideId = ConvertWrapper.ToInt32(Id),
                    Version = Version,
                    Efforts = new List<EffortSegmentV2>()
                };

                if (Efforts != null)
                {
                    Efforts.Each(effort => rideEffort.Efforts.Add(effort.ToEffortSegment()));
                }

                return rideEffort;
            }
        }

        /// <summary>
        /// Wrapper class for EffortSegmentV2
        /// </summary>
        protected class EffortSegmentV2Wrapper
        {
            /// <summary>
            /// Gets or sets the effort
            /// </summary>
            [JsonName("effort")]
            public EffortV2 Effort { get; set; }

            /// <summary>
            /// Gets or sets the segment
            /// </summary>
            [JsonName("segment")]
            public SegmentV2Wrapper Segment { get; set; }

            public EffortSegmentV2 ToEffortSegment()
            {
                var effortSeg = new EffortSegmentV2
                {
                    Effort = Effort
                };

                if (Segment != null)
                {
                    effortSeg.Segment = Segment.ToSegment();
                }

                return effortSeg;
            }
        }

        /// <summary>
        /// Wrapper class for SegmentV2
        /// </summary>
        protected class SegmentV2Wrapper
        {
            /// <summary>
            /// Gets or sets the id
            /// </summary>
            [JsonName("id")]
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name
            /// </summary>
            [JsonName("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the climb category
            /// </summary>
            [JsonName("climb_category")]
            public int ClimbCategory { get; set; }

            /// <summary>
            /// Gets or sets the average grade
            /// </summary>
            [JsonName("avg_grade")]
            public double AverageGrade { get; set; }

            /// <summary>
            /// Gets or sets the start coordinate
            /// </summary>
            [JsonName("start_latlng")]
            public double[] StartCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the end coordinate
            /// </summary>
            [JsonName("end_latlng")]
            public double[] EndCoordinate { get; set; }

            /// <summary>
            /// Gets or sets the elevation difference
            /// </summary>
            [JsonName("elev_difference")]
            public double ElevationDifference { get; set; }

            public SegmentV2 ToSegment()
            {
                var seg = new SegmentV2
                {
                    Id = Id,
                    Name = Name,
                    ClimbCategory = ClimbCategory,
                    AverageGrade = AverageGrade,
                    ElevationDifference = ElevationDifference
                };

                if (StartCoordinate != null && StartCoordinate.Length == 2)
                {
                    seg.StartCoordinate = new Coordinate { Latitude = StartCoordinate[0], Longitude = StartCoordinate[1] };
                }

                if (EndCoordinate != null && EndCoordinate.Length == 2)
                {
                    seg.EndCoordinate = new Coordinate { Latitude = EndCoordinate[0], Longitude = EndCoordinate[1] };
                }

                return seg;
            }
        }

        #endregion
    }
}
