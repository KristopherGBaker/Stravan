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
    /// Encapsulates an effort in the Strava API
    /// </summary>
    public class Effort
    {
        /// <summary>
        /// Gets or sets the id of the activity
        /// </summary>
        [JsonName("activityId")]
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the id of the effort
        /// </summary>
        [JsonName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the start date of the effort
        /// </summary>
        [JsonName("startDate")]
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the start date local of the effort
        /// </summary>
        [JsonName("startDateLocal")]
        public string StartDateLocal { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset of the effort
        /// </summary>
        [JsonName("timeZoneOffset")]
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time of the effort
        /// </summary>
        [JsonName("elapsedTime")]
        public int ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the moving time of the effort
        /// </summary>
        [JsonName("movingTime")]
        public int MovingTime { get; set; }

        /// <summary>
        /// Gets or sets the distance of the effort
        /// </summary>
        [JsonName("distance")]
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the average speed of the effort
        /// </summary>
        [JsonName("averageSpeed")]
        public double AverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets the average watts of the effort
        /// </summary>
        [JsonName("averageWatts")]
        public double AverageWatts { get; set; }

        /// <summary>
        /// Gets or sets the maximum speed of the effort
        /// </summary>
        [JsonName("maximumSpeed")]
        public double MaximumSpeed { get; set; }

        /// <summary>
        /// Gets or sets the elevation gain of the effort
        /// </summary>
        [JsonName("elevationGain")]
        public double ElevationGain { get; set; }

        /// <summary>
        /// Gets or sets the segment of the effort
        /// </summary>
        [JsonName("segment")]
        public Segment Segment { get; set; }

        /// <summary>
        /// Gets or sets the athlete of the effort
        /// </summary>
        [JsonName("athlete")]
        public Athlete Athlete { get; set;}

        /// <summary>
        /// Gets or sets the ride of the effort
        /// </summary>
        [JsonName("ride")]
        public Ride Ride { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("actvityId: {0}\n", ActivityId);
            builder.AppendFormat("startDate: {0}\n", StartDate ?? string.Empty);
            builder.AppendFormat("startDateLocal: {0}\n", StartDateLocal ?? string.Empty);
            builder.AppendFormat("timeZoneOffset: {0}\n", TimeZoneOffset);
            builder.AppendFormat("elapsedTime: {0}\n", ElapsedTime);
            builder.AppendFormat("movingTime: {0}\n", MovingTime);
            builder.AppendFormat("distance: {0}\n", Distance);
            builder.AppendFormat("averageSpeed: {0}\n", AverageSpeed);
            builder.AppendFormat("averageWatts: {0}\n", AverageWatts);
            builder.AppendFormat("maximumSpeed: {0}\n", MaximumSpeed);
            builder.AppendFormat("elevationGain: {0}\n", ElevationGain);
            builder.AppendFormat("segment: [{0}]\n", Segment != null ? Segment.ToString() : string.Empty);
            builder.AppendFormat("athlete: [{0}]\n", Athlete != null ? Athlete.ToString() : string.Empty);
            builder.AppendFormat("ride: [{0}]\n", Ride != null ? Ride.ToString() : string.Empty);

            return builder.ToString();
        }
    }
}
