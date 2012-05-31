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
using System.Text;
using EmpyrealNight.Core;

namespace Stravan
{
    /// <summary>
    /// Encapsulates map details (v2)
    /// </summary>
    public class MapDetailV2
    {
        /// <summary>
        /// Gets or sets the ride id
        /// </summary>
        public int RideId { get; set; }

        /// <summary>
        /// Gets or sets the coordinates
        /// </summary>
        public List<Coordinate> Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public string Version { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nRideId: {0}\n", RideId);
            builder.Append("Coordinates: [");
            if (Coordinates != null)
            {
                Coordinates.Each((coord, index) => 
                {
                    builder.Append(coord);
                    if (index < (Coordinates.Count - 1))
                        builder.Append(", ");
                });
            }
            builder.Append("]\n");
            builder.AppendFormat("Version: {0}\n", Version ?? string.Empty);

            return builder.ToString();
        }
    }
}
