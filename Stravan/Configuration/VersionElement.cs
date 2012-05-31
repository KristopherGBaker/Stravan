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
using EmpyrealNight.Core;
using EmpyrealNight.Core.Configuration;

namespace Stravan.Configuration
{
    /// <summary>
    /// Encapsulates version configuration information
    /// </summary>
    public class VersionElement : ConfigurationElement, INameConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the version of the Strava API
        /// </summary>
        [ConfigurationProperty("version", IsRequired = true)]
        public int Version
        {
            get { return ConvertWrapper.ToInt32(this["version"]); }
            set { this["version"] = value; }
        }

        /// <summary>
        /// Gets or sets the Strava API base URL
        /// </summary>
        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get { return this["baseUrl"] as string; }
            set { this["baseUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets the secure (HTTPS/SSL) Strava API base URL
        /// </summary>
        [ConfigurationProperty("secureBaseUrl", IsRequired = true)]
        public string SecureBaseUrl
        {
            get { return this["secureBaseUrl"] as string; }
            set { this["secureBaseUrl"] = value; }
        }
    }
}
