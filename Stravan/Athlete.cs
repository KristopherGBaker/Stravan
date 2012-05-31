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
    /// Encapsulates an athlete in the Strava API
    /// </summary>
    public class Athlete : IEntity
    {
        /// <summary>
        /// Gets or sets the id of the athlete
        /// </summary>
        [JsonName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the athlete
        /// </summary>
        [JsonName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username of the athlete
        /// </summary>
        [JsonName("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the agreed to terms boolean
        /// </summary>
        [JsonName("agreed_to_terms")]
        public bool HasAgreedToTerms { get; set; }

        /// <summary>
        /// Gets or sets the super user boolean
        /// </summary>
        [JsonName("super_user")]
        public bool IsSuperUser { get; set; }

        /// <summary>
        /// Gets or sets the iphone tester boolean
        /// </summary>
        [JsonName("iphone_tester")]
        public bool IsIphoneTester { get; set; }

        /// <summary>
        /// Gets or sets the push token
        /// </summary>
        [JsonName("push_token")]
        public string PushToken { get; set; }

        /// <summary>
        /// Gets or sets the default settings
        /// </summary>
        [JsonName("default_settings")]
        public AthleteSetting DefaultSettings { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("name: {0}\n", Name ?? string.Empty);
            builder.AppendFormat("username: {0}\n", Username ?? string.Empty);
            builder.AppendFormat("agreed_to_terms: {0}\n", HasAgreedToTerms);
            builder.AppendFormat("super_user: {0}\n", IsSuperUser);
            builder.AppendFormat("iphone_tester: {0}\n", IsIphoneTester);
            builder.AppendFormat("push_token: {0}\n", PushToken ?? string.Empty);
            builder.AppendFormat("default_settings: [{0}]\n", DefaultSettings != null ? DefaultSettings.ToString() : string.Empty);

            return builder.ToString();
        }
    }
}