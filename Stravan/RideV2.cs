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
    /// Encapsulates a ride in the Strava API
    /// </summary>
    public class RideV2 : IEntity
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
        public Coordinate StartCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the end coordinate
        /// </summary>
        [JsonName("end_latlng")]
        public Coordinate EndCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        [JsonName("version")]
        public string Version { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("name: {0}\n", Name ?? string.Empty);
            builder.AppendFormat("startDateLocal: {0}\n", StartDateLocal ?? string.Empty);
            builder.AppendFormat("elapsedTime: {0}\n", ElapsedTime);
            builder.AppendFormat("movingTime: {0}\n", MovingTime);
            builder.AppendFormat("distance: {0}\n", Distance);
            builder.AppendFormat("averageSpeed: {0}\n", AverageSpeed);
            builder.AppendFormat("elevationGain: {0}\n", ElevationGain);
            builder.AppendFormat("location: {0}\n", Location ?? string.Empty);
            builder.AppendFormat("StartCoordinate: {0}\n", StartCoordinate != null ? StartCoordinate.ToString() : string.Empty);
            builder.AppendFormat("EndCoordinate: {0}\n", EndCoordinate != null ? EndCoordinate.ToString() : string.Empty);

            return builder.ToString();
        }
    }
}