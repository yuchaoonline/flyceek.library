/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections;
using System.Collections.Generic;


namespace ComLib.Validation
{
    /// <summary>
    /// Interface for action results
    /// </summary>
    public interface IStatusResults : IEnumerable<StatusResult>
    {
        /// <summary>
        /// Add single status / error message.
        /// </summary>
        /// <param name="message"></param>
        void Add(string message);

        
        /// <summary>
        /// Add single action result to represent status/error.
        /// </summary>
        /// <param name="result"></param>
        void Add(StatusResult result);


        /// <summary>
        /// Add single action result.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="target"></param>
        void Add(string key, string message, object target);


        /// <summary>
        /// Add a set of actionresults.
        /// </summary>
        /// <param name="sourceResults"></param>
        void AddAll(IEnumerable<StatusResult> sourceResults);


        /// <summary>
        /// Get the number of action results in this list. 
        /// </summary>
        int Count { get; }


        /// <summary>
        /// Determine whether or not they are valid.
        /// </summary>
        bool IsValid { get; }
    }
}
