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
using Stravan.Json;

namespace Stravan.Tests
{
    /// <summary>
    /// Encapsulates test for the authentication service
    /// </summary>
    [TestFixture]
    public class AuthenticationServiceFixture : BaseServiceFixture
    {
        /// <summary>
        /// Log
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthenticationServiceFixture));

        /// <summary>
        /// Gets or sets the authentication service
        /// </summary>
        private IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Setup for all tests
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            base.Setup();

#if UNIT
            AuthenticationService = new AuthenticationService(WebClient);
#elif INT
            AuthenticationService = ServiceLocator.Get<IAuthenticationService>();
#endif
        }

        ///// <summary>
        ///// Tests for successful login
        ///// </summary>
        [Test]
        public void LoginSuccess()
        {
            var auth = AuthenticationService.Login(Email, Password);
            Assert.That(auth != null);
            Assert.That(auth.IsSuccess);
            Assert.That(auth.AthleteId > 0);
            Assert.That(auth.Token != null);
            Assert.That(auth.Token.Length > 0);

            Log.Debug(auth);
        }

        ///// <summary>
        ///// Tests for successful login
        ///// </summary>
        [Test]
        public void LoginSuccessV2()
        {
            var auth = AuthenticationService.LoginV2(Email, Password, true);
            Assert.That(auth != null);
            Assert.That(string.IsNullOrWhiteSpace(auth.Error));
            Assert.That(auth.Athlete != null);
            Assert.That(auth.Token != null);
            Assert.That(auth.Token.Length > 0);

            Log.Debug(auth);
        }
    }
}
