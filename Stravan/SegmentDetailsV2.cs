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

namespace Stravan
{
    /// <summary>
    /// Encapsulates segment details
    /// </summary>
    public class SegmentDetailsV2
    {
        /// <summary>
        /// Gets or sets the start coordinate
        /// </summary>
        public Coordinate StartCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the end coordinate
        /// </summary>
        public Coordinate EndCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the climb category
        /// </summary>
        public int ClimbCategory { get; set; }

        /// <summary>
        /// Gets or sets the number of times ridden
        /// </summary>
        public int TimeRidden { get; set; }

        /// <summary>
        /// Gets or sets the number o riders ridden
        /// </summary>
        public int RidersRidden { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the elevation difference
        /// </summary>
        public double ElevationDifference { get; set; }

        /// <summary>
        /// Gets or sets the kom
        /// </summary>
        public LeaderV2 KingOfMountain { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("name: {0}\n", Name ?? string.Empty);
            builder.AppendFormat("climbCategory: {0}\n", ClimbCategory);
            builder.AppendFormat("TimeRidden: {0}\n", TimeRidden);
            builder.AppendFormat("RidersRidden: {0}\n", RidersRidden);
            builder.AppendFormat("StartCoordinate: {0}\n", StartCoordinate != null ? StartCoordinate.ToString() : string.Empty);
            builder.AppendFormat("EndCoordinate: {0}\n", EndCoordinate != null ? EndCoordinate.ToString() : string.Empty);
            builder.AppendFormat("ElevationDifference: {0}\n", ElevationDifference);
            builder.AppendFormat("KingOfMountain: {0}\n", KingOfMountain != null ? KingOfMountain.ToString() : string.Empty);

            return builder.ToString();
        }
    }
}
