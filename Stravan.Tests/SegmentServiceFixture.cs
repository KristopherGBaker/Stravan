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

namespace Stravan.Tests
{
    /// <summary>
    /// Encapsulates tests for segment service
    /// </summary>
    [TestFixture]
    public class SegmentServiceFixture
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SegmentServiceFixture));

        /// <summary>
        /// Segment id for Eastside
        /// </summary>
        private const int UpperEastsideSegmentId = 686627;

        /// <summary>
        /// My athelete id
        /// </summary>
        private const int AthleteId = 476912;

        /// <summary>
        /// Gets or sets the segment service
        /// </summary>
        private ISegmentService SegmentService { get; set; }

        /// <summary>
        /// Setup for all tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            SegmentService = ServiceLocator.Get<ISegmentService>();
        }

        /// <summary>
        /// Test that gets the segment details for Eastside
        /// </summary>
        [Test]
        public void ShowUpperEastside()
        {
            var segment = SegmentService.Show(UpperEastsideSegmentId);
            Assert.That(segment != null);
            Assert.That(segment.Id == UpperEastsideSegmentId);

            Log.Debug(segment);
        }

        /// <summary>
        /// Test that gets segment efforts by athlete id
        /// </summary>
        [Test]
        public void EffortsByAthleteId()
        {
            var segmentEfforts = SegmentService.Efforts(segmentId: UpperEastsideSegmentId, athleteId: AthleteId);
            Assert.That(segmentEfforts != null);
            Assert.That(segmentEfforts.Segment != null);
            Assert.That(segmentEfforts.Efforts != null);
            Assert.That(segmentEfforts.Efforts.Count > 0);

            Log.Debug(segmentEfforts);
        }

        /// <summary>
        /// Test that searches for segments with the name "Eastside"
        /// </summary>
        [Test]
        public void Index()
        {
            var segments = SegmentService.Index("Eastside");
            Assert.That(segments != null);

            segments.Each(segment => Log.Debug(segment));
        }
    }
}
