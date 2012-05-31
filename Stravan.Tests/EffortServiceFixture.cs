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

namespace Stravan.Tests
{
    /// <summary>
    /// Encapsulates tests for the effort service
    /// </summary>
    [TestFixture]
    public class EffortServiceFixture
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EffortServiceFixture));

        /// <summary>
        /// Effort id for one of my rides on Eastside
        /// </summary>
        private const int UpperEastsideEffortId = 153532048;

        /// <summary>
        /// Gets or sets the effort service
        /// </summary>
        private IEffortService EffortService { get; set; }

        /// <summary>
        /// Setup for all tests
        /// </summary>
        [SetUp]
        public void Setup()
        {
            EffortService = ServiceLocator.Get<IEffortService>();
        }

        /// <summary>
        /// Test that gets the details for an effort
        /// </summary>
        [Test]
        public void Show()
        {
            var effort = EffortService.Show(UpperEastsideEffortId);
            Assert.That(effort != null);
            Assert.That(effort.Id == UpperEastsideEffortId);
            Log.Debug(effort);
        }
    }
}
