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
using EmpyrealNight.Core.Json;

namespace Stravan
{
    /// <summary>
    /// Encapsulates a leaderboard
    /// </summary>
    public class LeaderboardV2
    {
        /// <summary>
        /// Gets or sets the rank
        /// </summary>
        [JsonName("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the best effort
        /// </summary>
        [JsonName("best")]
        public LeaderV2 Best { get; set; }

        /// <summary>
        /// Gets or sets the context
        /// </summary>
        [JsonName("context")]
        public List<LeaderV2> Context { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nRank: {0}\n", Rank);
            builder.AppendFormat("Best: {0}\n", Best != null ? Best.ToString() : string.Empty);
            builder.Append("Context: [");
            if (Context != null)
            {
                Context.Each((leader, index) =>
                {
                    builder.Append(leader);
                    if (index < (Context.Count - 1))
                        builder.Append(", ");
                });
            }
            builder.Append("]\n");

            return builder.ToString();
        }
    }
}
