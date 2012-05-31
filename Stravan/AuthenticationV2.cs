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

using System.Text;
using EmpyrealNight.Core.Json;

namespace Stravan
{
    /// <summary>
    /// Encapsulates authentication information from the Strava API v2
    /// </summary>
    public class AuthenticationV2
    {
        /// <summary>
        /// Gets or sets the authentication token
        /// </summary>
        [JsonName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the athlete id
        /// </summary>
        [JsonName("athlete")]
        public Athlete Athlete { get; set; }

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        [JsonName("error")]
        public string Error { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\ntoken: {0}\n", Token ?? string.Empty);
            builder.AppendFormat("athlete: {0}\n", Athlete != null ? Athlete.ToString() : string.Empty);
            builder.AppendFormat("error: {0}\n", Error ?? string.Empty);

            return builder.ToString();
        }
    }
}