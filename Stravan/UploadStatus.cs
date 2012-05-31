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

using System.Text;
using EmpyrealNight.Core.Json;

namespace Stravan
{
    /// <summary>
    /// Encapsulates the status of an upload
    /// </summary>
    public class UploadStatus
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [JsonName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the upload id
        /// </summary>
        [JsonName("upload_id")]
        public int UploadId { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [JsonName("upload_status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the progress
        /// </summary>
        [JsonName("upload_progress")]
        public string Progress { get; set; }

        /// <summary>
        /// Gets or sets the activity id
        /// </summary>
        [JsonName("activity_id")]
        public int ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time
        /// </summary>
        [JsonName("elapsed_time")]
        public int ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the distance
        /// </summary>
        [JsonName("distance")]
        public double Distance { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("\nid: {0}\n", Id);
            builder.AppendFormat("UploadId: {0}\n", UploadId);
            builder.AppendFormat("Status: {0}\n", Status ?? string.Empty);
            builder.AppendFormat("Progress: {0}\n", Progress ?? string.Empty);
            builder.AppendFormat("ActivityId: {0}\n", ActivityId);
            builder.AppendFormat("ElapsedTime: {0}\n", ElapsedTime);
            builder.AppendFormat("Distance: {0}\n", Distance);

            return builder.ToString();
        }
    }
}
