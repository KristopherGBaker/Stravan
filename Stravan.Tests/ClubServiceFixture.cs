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

using System.Text.RegularExpressions;
using NUnit.Framework;
using log4net;
using EmpyrealNight.Core;
using Stravan.Json;

namespace Stravan.Tests
{
    /// <summary>
    /// Encapsulates tests for the club service
    /// </summary>
    [TestFixture]
    public class ClubServiceFixture : BaseServiceFixture
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ClubServiceFixture));

        /// <summary>
        /// Gets or sets the club service
        /// </summary>
        private IClubService ClubService { get; set; }

        /// <summary>
        /// Setup for all tests
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            base.Setup();

#if UNIT
            ClubService = new ClubService(WebClient);
#elif INT
            ClubService = ServiceLocator.Get<IClubService>();
#endif
        }

        /// <summary>
        /// Test that searches for clubs
        /// </summary>
        [Test]
        public void Index()
        {
            var clubs = ClubService.Index("nema");
            Assert.That(clubs != null);
            Assert.That(clubs.Count > 0);

            clubs.Each(club => Log.Debug(club));
        }

        /// <summary>
        /// Test that gets the details for the club
        /// </summary>
        [Test]
        public void Show()
        {
            var club = ClubService.Show(ClubId);
            Assert.That(club != null);
            Assert.That(club.Id == ClubId);
            Assert.That(Regex.IsMatch(club.Name, ClubName, RegexOptions.IgnoreCase));
            Assert.That(!string.IsNullOrWhiteSpace(club.Description));
            Assert.That(!string.IsNullOrWhiteSpace(club.Location));

            Log.Debug(club);
        }

        /// <summary>
        /// Test that gets the members of the club
        /// </summary>
        [Test]
        public void Members()
        {
            var clubMembers = ClubService.Members(ClubId);
            Assert.That(clubMembers.Club != null);

            var club = clubMembers.Club;
            Assert.That(club != null);
            Assert.That(club.Id == ClubId);
            Assert.That(!string.IsNullOrWhiteSpace(club.Name));

            Assert.That(clubMembers.Members != null);
            Assert.That(clubMembers.Members.Count > 0);

            Log.Debug(club);
            clubMembers.Members.Each(member => Log.Debug(member));
        }
    }
}
