﻿#region MIT License

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
using EmpyrealNight.Core.Json;

namespace Stravan
{
    /// <summary>
    /// Encapsulates a segment and a list of its efforts in the Strava API
    /// </summary>
    public class SegmentEfforts
    {
        /// <summary>
        /// Gets or sets the list of efforts
        /// </summary>
        [JsonName("efforts")]
        public List<Effort> Efforts { get; set; }

        /// <summary>
        /// Gets or sets the segment
        /// </summary>
        [JsonName("segment")]
        public Segment Segment { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nsegment: [{0}]\n", Segment != null ? Segment.ToString() : string.Empty);
            builder.Append("efforts: [");

            if (Efforts != null)
            {
                Efforts.Each(effort => builder.AppendFormat("effort: [{0}]", effort));
            }

            builder.Append("]\n");

            return builder.ToString();
        }
    }
}
