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
    /// Encapsulates a club in the Strava API
    /// </summary>
    public class Club : IEntity
    {
        /// <summary>
        /// Gets or sets the id of the club
        /// </summary>
        [JsonName("id")]
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the club
        /// </summary>
        [JsonName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the club
        /// </summary>
        [JsonName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location of the club
        /// </summary>
        [JsonName("location")]
        public string Location { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("name: {0}\n", Name ?? string.Empty);
            builder.AppendFormat("description: {0}\n", Description ?? string.Empty);
            builder.AppendFormat("location: {0}\n", Location ?? string.Empty);

            return builder.ToString();
        }
    }
}