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
    public class Ride : IEntity
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
        /// Gets or sets the athlete
        /// </summary>
        [JsonName("athlete")]
        public Athlete Athlete { get; set;}

        /// <summary>
        /// Gets or sets the start date
        /// </summary>
        [JsonName("startDate")]
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the start date local
        /// </summary>
        [JsonName("startDateLocal")]
        public string StartDateLocal { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset
        /// </summary>
        [JsonName("timeZoneOffset")]
        public int TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time
        /// </summary>
        [JsonName("elapsedTime")]
        public int ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the moving time
        /// </summary>
        [JsonName("movingTime")]
        public int MovingTime { get; set; }

        /// <summary>
        /// Gets or sets the distance
        /// </summary>
        [JsonName("distance")]
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the average speed
        /// </summary>
        [JsonName("averageSpeed")]
        public double AverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets the average watts
        /// </summary>
        [JsonName("averageWatts")]
        public double AverageWatts { get; set; }

        /// <summary>
        /// Gets or sets the maximum speed
        /// </summary>
        [JsonName("maximumSpeed")]
        public double MaximumSpeed { get; set; }

        /// <summary>
        /// Gets or sets the elevation gain
        /// </summary>
        [JsonName("elevationGain")]
        public double ElevationGain { get; set; }

        /// <summary>
        /// Gets or sets the location
        /// </summary>
        [JsonName("location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the bike
        /// </summary>
        [JsonName("bike")]
        public Bike Bike { get; set; }

        /// <summary>
        /// Gets or sets the commute boolean
        /// </summary>
        [JsonName("commute")]
        public bool IsCommute { get; set; }

        /// <summary>
        /// Gets or sets the trainer boolean
        /// </summary>
        [JsonName("trainer")]
        public bool IsTrainer { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("name: {0}\n", Name ?? string.Empty);
            builder.AppendFormat("athlete: [{0}]\n", Athlete != null ? Athlete.ToString() : string.Empty);
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
            builder.AppendFormat("location: {0}\n", Location ?? string.Empty);
            builder.AppendFormat("description: {0}\n", Description ?? string.Empty);
            builder.AppendFormat("bike: [{0}]\n", Bike != null ? Bike.ToString() : string.Empty);
            builder.AppendFormat("commute: {0}\n", IsCommute);
            builder.AppendFormat("trainer: {0}\n", IsTrainer);

            return builder.ToString();
        }
    }
}