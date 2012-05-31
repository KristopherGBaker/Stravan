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

using System.Collections.Specialized;
using Moq;

namespace Stravan.Tests
{
    /// <summary>
    /// Base class for all test fixtures
    /// </summary>
    public abstract class BaseServiceFixture
    {
        /// <summary>
        /// Ride id for one of my rides on Eastside
        /// </summary>
        public const int EastsideRideId = 8384559;

        /// <summary>
        /// An effort on Eastside ride above
        /// </summary>
        public const int EastsideToSinkerCreekEffort = 153532130;

        /// <summary>
        /// My athlete id
        /// </summary>
        public const int AthleteId = 476912;

        /// <summary>
        /// Eastside ride version
        /// </summary>
        public const string RideVersion = "1336883112";

        /// <summary>
        /// 
        /// </summary>
        protected Mock<IStravaWebClient> WebClientMock { get; set; }

        protected IStravaWebClient WebClient { get { return WebClientMock.Object; } }

        public virtual void Setup()
        {
            WebClientMock = new Mock<IStravaWebClient>();
            SetupRideService(WebClientMock);
        }

        private void SetupRideService(Mock<IStravaWebClient> mock)
        {
            SetupRideShow(mock);
            SetupRideShowV2(mock);
            SetupRideIsSyncedV2(mock);

        }

        private void SetupRideIsSyncedV2(Mock<IStravaWebClient> mock)
        {
            var response = @"
{
   ""id"":""8384559"",
   ""version"":""1336883112"",
   ""synced"":true
}";
            mock
                .Setup(client => client.Download(string.Format("rides/{0}", EastsideRideId), new NameValueCollection { { "version", RideVersion } }, null, false, ApiVersionType.VersionTwo))
                .Returns(() => response);
        }

        private void SetupRideShowV2(Mock<IStravaWebClient> mock)
        {
            var response = @"
{
   ""id"":""8384559"",
   ""ride"":{
      ""id"":8384559,
      ""name"":""05/12/2012 Eastside"",
      ""start_date_local"":""2012-05-12T11:09:23Z"",
      ""elapsed_time"":4910,
      ""moving_time"":4744,
      ""distance"":15785.9,
      ""average_speed"":3.327550590219224,
      ""elevation_gain"":473.84,
      ""location"":""Boise, ID"",
      ""start_latlng"":[
         43.73913406860083,
         -116.1272011231631
      ],
      ""end_latlng"":[
         43.73923611827195,
         -116.12712794914842
      ]
   },
   ""version"":""1336883112""
}";
            mock
                .Setup(client => client.Download(string.Format("rides/{0}", EastsideRideId), null, null, false, ApiVersionType.VersionTwo))
                .Returns(() => response);
        }

        private void SetupRideShow(Mock<IStravaWebClient> mock)
        {
            var response = @"
{
   ""ride"":{
      ""id"":8384559,
      ""startDate"":""2012-05-12T17:09:23Z"",
      ""startDateLocal"":""2012-05-12T11:09:23Z"",
      ""timeZoneOffset"":-25200,
      ""elapsedTime"":4910,
      ""movingTime"":4744,
      ""distance"":15785.9,
      ""averageSpeed"":3.327550590219224,
      ""averageWatts"":165.782,
      ""maximumSpeed"":49411.08,
      ""elevationGain"":473.84,
      ""location"":""Boise, ID"",
      ""name"":""05/12/2012 Eastside"",
      ""bike"":{
         ""id"":170363,
         ""name"":""Santa Cruz Nomad Carbon""
      },
      ""athlete"":{
         ""id"":476912,
         ""name"":""Kris Baker"",
         ""username"":""kris_baker""
      },
      ""description"":"""",
      ""commute"":false,
      ""trainer"":false
   }
}";

            mock
                .Setup(client => client.Download(string.Format("rides/{0}", EastsideRideId), null, null, false, ApiVersionType.VersionOne))
                .Returns(() => response);
        }
    }
}
