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

using NUnit.Framework;
using log4net;
using EmpyrealNight.Core;
using Stravan.Json;

namespace Stravan.Tests
{
    /// <summary>
    /// Encapsulates tests for the ride service.
    /// </summary>
    [TestFixture]
    public class RideServiceFixture : BaseServiceFixture
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(RideServiceFixture));

        /// <summary>
        /// Gets or sets the ride service
        /// </summary>
        private IRideService RideService { get; set; }

        /// <summary>
        /// Setup for all tests.
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            base.Setup();
#if UNIT
            RideService = new RideService(WebClient);
#elif INT
            RideService = ServiceLocator.Get<IRideService>();
#endif
        }

        /// <summary>
        /// Test that gets the details for a ride.
        /// </summary>
        [Test]
        public void Show()
        {
            var ride = RideService.Show(RideId);
            Assert.That(ride != null);
            Assert.That(ride.Id == RideId);
            Assert.That(!string.IsNullOrWhiteSpace(ride.Name));
            Log.Debug(ride);
        }

        /// <summary>
        /// Test that gets the details for a ride (v2).
        /// </summary>
        [Test]
        public void ShowV2()
        {
            //System.Diagnostics.Debugger.Launch();
            var ride = RideService.ShowV2(RideId);
            Assert.That(ride != null);
            Assert.That(ride.Id == RideId);
            Assert.That(!string.IsNullOrWhiteSpace(ride.Name));
            Log.Debug(ride);
        }

        /// <summary>
        /// Tests for a synced ride.
        /// </summary>
        [Test]
        public void SyncedV2()
        {
            Assert.That(RideService.IsSyncedV2(RideId, RideVersion));
        }

        /// <summary>
        /// Test that searches for all rides by athlete id
        /// </summary>
        [Test]
        public void IndexByAthleteId()
        {
            var rides = RideService.Index(athleteId: AthleteId);
            Assert.That(rides != null);
            Assert.That(rides.Count > 0);

            rides.Each(ride => Log.Debug(ride));
        }

        /// <summary>
        /// Test that searches for all rides by athlete username
        /// </summary>
        [Test]
        public void IndexByAthleteName()
        {
            var rides = RideService.Index(athleteName: AthleteName);
            Assert.That(rides != null);
            Assert.That(rides.Count > 0);

            rides.Each(ride => Log.Debug(ride));
        }

        /// <summary>
        /// Test the gets the efforts for a ride
        /// </summary>
        [Test]
        public void EffortsForRide()
        {
            var rideEfforts = RideService.Efforts(RideId);
            Assert.That(rideEfforts != null);
            Assert.That(rideEfforts.Ride != null);
            Assert.That(rideEfforts.Efforts != null);
            Assert.That(rideEfforts.Efforts.Count > 0);

            Log.Debug(rideEfforts.Ride);
            rideEfforts.Efforts.Each(effort => Log.Debug(effort));
        }

        /// <summary>
        /// Test the gets the efforts for a ride (v2)
        /// </summary>
        [Test]
        public void EffortsForRideV2()
        {
            var rideEfforts = RideService.EffortsV2(RideId);
            Assert.That(rideEfforts != null);
            Assert.That(rideEfforts.RideId == RideId);
            Assert.That(rideEfforts.Efforts != null);
            Assert.That(rideEfforts.Efforts.Count > 0);

            Log.Debug(rideEfforts);
        }

        [Test]
        public void MapDetailsV2()
        {
            var mapDetails = RideService.MapDetailsV2(RideId, Token);
            Assert.That(mapDetails != null);
            Assert.That(mapDetails.RideId == RideId);
            Assert.That(!string.IsNullOrWhiteSpace(mapDetails.Version));
            Assert.That(mapDetails.Coordinates != null);
            Assert.That(mapDetails.Coordinates.Count > 0);
            Log.Debug(mapDetails);
        }

        [Test]
        public void EffortDetailsForRideV2()
        {
            var effortDetails = RideService.ShowEffortsV2(RideId, EffortId);
            Assert.That(effortDetails != null);
            Log.Debug(effortDetails);
        }
    }
}
