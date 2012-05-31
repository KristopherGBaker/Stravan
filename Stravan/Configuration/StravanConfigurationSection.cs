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

using System.Configuration;

namespace Stravan.Configuration
{
    /// <summary>
    /// Encapsulates Stravan configuration information.
    /// </summary>
    public class StravanConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets configuration for StravaClient
        /// </summary>
        [ConfigurationProperty("client", IsRequired = true)]
        public ClientElement Client
        {
            get { return this["client"] as ClientElement; }
            set { this["client"] = value; }
        }

        /// <summary>
        /// Gets or sets Strava API info
        /// </summary>
        [ConfigurationProperty("api", IsRequired = true)]
        public ApiElement Api
        {
            get { return this["api"] as ApiElement; }
            set { this["api"] = value; }
        }
    }
}
