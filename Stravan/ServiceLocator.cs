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

using Stravan.Json;
using Ninject;
using Ninject.Modules;

namespace Stravan
{
    /// <summary>
    /// Service locator for Strava services
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Gets or sets the Ninject kernel
        /// </summary>
        private static IKernel Kernel { get; set; }

        /// <summary>
        /// Gets a new instance of the specified interface/class
        /// </summary>
        /// <typeparam name="T">Type of the interface/class</typeparam>
        /// <returns>New instance of the specified interface/class</returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Initializes the service locator with the specified Ninject module
        /// </summary>
        /// <param name="module"></param>
        public static void Init(INinjectModule module)
        {
            Kernel = new StandardKernel(module);
        }

        /// <summary>
        /// Initializes the service locator with the default Ninject module for Strava services
        /// </summary>
        static ServiceLocator()
        {
            Kernel = new StandardKernel(new JsonModule());
        }
    }
}
