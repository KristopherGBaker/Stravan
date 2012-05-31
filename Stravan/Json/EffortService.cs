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

using EmpyrealNight.Core.Json;

namespace Stravan.Json
{
    /// <summary>
    /// Service interface for efforts in the Strava API
    /// </summary>
    public class EffortService : BaseService, IEffortService
    {
        /// <summary>
        /// Initializes an EffortService with the specified client
        /// </summary>
        /// <param name="client">IStravaWebClient to use with the service</param>
        public EffortService(IStravaWebClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Returns extended information for an Effort on a Ride.
        /// </summary>
        /// <param name="id">Required. The Id of the Effort.</param>
        /// <returns>Effort for the specified id with extended information.</returns>
        public Effort Show(int id)
        {
            var response = Client.Download(string.Format("efforts/{0}", id));

            return JsonDeserializer.Deserialize<EffortWrapper>(response).Effort;
        }

        #region wrapper classes

        /// <summary>
        /// Wrapper class for an effort
        /// </summary>
        protected class EffortWrapper
        {
            /// <summary>
            /// Gets or sets the effort
            /// </summary>
            [JsonName("effort")]
            public Effort Effort { get; set; }
        }

        #endregion
    }
}
