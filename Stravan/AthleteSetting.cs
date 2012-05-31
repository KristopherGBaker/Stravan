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

using System.Text;
using EmpyrealNight.Core.Json;

namespace Stravan
{
    /// <summary>
    /// Encapsulates athlete settings
    /// </summary>
    public class AthleteSetting
    {
        /// <summary>
        /// Gets or sets the sample rate
        /// </summary>
        [JsonName("sample_rate")]
        public int SampleRate { get; set; }

        /// <summary>
        /// Gets or sets the continuous gps boolean
        /// </summary>
        [JsonName("continuous_gps")]
        public bool IsContinuousGps { get; set; }

        /// <summary>
        /// Gets or sets the accuracy
        /// </summary>
        [JsonName("accuracy")]
        public int Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the distance filter
        /// </summary>
        [JsonName("distance_filter")]
        public int DistanceFilter { get; set; }

        /// <summary>
        /// Gets or sets the max search time
        /// </summary>
        [JsonName("max_search_time")]
        public int MaxSearchTime { get; set; }

        /// <summary>
        /// Gets or sets the max stale time
        /// </summary>
        [JsonName("min_stale_time")]
        public int MaxStaleTime { get; set; }

        /// <summary>
        /// Gets or sets the min accuracy
        /// </summary>
        [JsonName("min_accuracy")]
        public int MinAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the map threshold
        /// </summary>
        [JsonName("map_threshold")]
        public int MapThreshold { get; set; }

        /// <summary>
        /// Gets or sets the max sync time
        /// </summary>
        [JsonName("max_sync_time")]
        public int MaxSyncTime { get; set; }

        /// <summary>
        /// Gets or sets the max waypoint stale time
        /// </summary>
        [JsonName("max_waypoint_stale_time")]
        public int MaxWaypointStaleTime { get; set; }

        /// <summary>
        /// Gets or sets the update ride poll interval
        /// </summary>
        [JsonName("update_ride_poll_interval")]
        public int UpdateRidePollInterval { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nSampleRate: {0}\n", SampleRate);
            builder.AppendFormat("IsContinuousGps: {0}\n", IsContinuousGps);
            builder.AppendFormat("Accuracy: {0}\n", Accuracy);
            builder.AppendFormat("DistanceFilter: {0}\n", DistanceFilter);
            builder.AppendFormat("MaxSearchTime: {0}\n", MaxSearchTime);
            builder.AppendFormat("MaxStaleTime: {0}\n", MaxStaleTime);
            builder.AppendFormat("MinAccuracy: {0}\n", MinAccuracy);
            builder.AppendFormat("MapThreshold: {0}\n", MapThreshold);
            builder.AppendFormat("MaxSyncTime: {0}\n", MaxSyncTime);
            builder.AppendFormat("MaxWaypointStaleTime: {0}\n", MaxWaypointStaleTime);
            builder.AppendFormat("UpdateRidePollInterval: {0}\n", UpdateRidePollInterval);

            return builder.ToString();
        }
    }
}
