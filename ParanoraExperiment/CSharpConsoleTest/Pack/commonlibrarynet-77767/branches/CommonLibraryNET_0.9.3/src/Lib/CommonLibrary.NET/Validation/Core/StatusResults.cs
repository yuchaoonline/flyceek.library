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
using System.Text;

namespace ComLib.Validation
{

    /// <summary> 
    /// Stores validation results. 
    /// </summary> 
    /// <remarks>NOTE : Errors could be a read-only collection. 
    /// </remarks> 
    public class StatusResults : IStatusResults
    {
        protected List<StatusResult> _results;


        /// <summary>
        /// Null object.
        /// </summary>
        public static readonly StatusResults Empty = new StatusResults();



        /// <summary>
        /// Default constructor.
        /// </summary>
        public StatusResults()
        {
            _results = new List<StatusResult>();
        }


        /// <summary> 
        /// Construtor. 
        /// </summary> 
        /// <param name="isValid"></param> 
        public StatusResults(List<StatusResult> results)
        {
        }


        /// <summary>
        /// Returns the number of items in the results.
        /// </summary>
        public int Count
        {
            get
            {
                if (_results == null)
                    return 0;

                return _results.Count;
            }
        }


        /// <summary>
        /// <para>Adds a <see cref="ValidationResult"/>.</para>
        /// </summary>
        /// <param name="validationResult">The validation result to add.</param>
        public void Add(StatusResult result)
        {
            _results.Add(result);
        }


        /// <summary>
        /// Add a new validation result.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="target"></param>
        public virtual void Add(string key, string message, object target)
        {
            Add(new StatusResult(key, message, target));
        }


        /// <summary>
        /// Add a new validation result with the supplied message
        /// </summary>
        /// <param name="message"></param>
        public virtual void Add(string message)
        {
            Add(new StatusResult(string.Empty, message, null));
        }
       

        /// <summary>
        /// <para>Adds all the <see cref="results"/> instances from <paramref name="sourceResults"/>.</para>
        /// </summary>
        /// <param name="sourceResults">The source results to add.</param>
        public void AddAll(IEnumerable<StatusResult> sourceResults)
        {
            _results.AddRange(sourceResults);
        }


        /// <summary> 
        /// For this base class, isValid doesn't mean anything.
        /// Specifically if this is used to store status messages.
        /// So return true here and allow derived classes to override
        /// the implementation.
        /// </summary> 
        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }


        /// <summary>
        /// This is exposed to allow results to be built into a single message
        /// in the ValidationUtils class.
        /// </summary>
        internal List<StatusResult> Results
        {
            get { return _results; }
        }


        /// <summary>
        /// Get the typed enumerator for the results.
        /// </summary>
        /// <returns></returns>
        IEnumerator<StatusResult> IEnumerable<StatusResult>.GetEnumerator()
        {
            return _results.GetEnumerator();
        }


        /// <summary>
        /// Get the enumerator for the results.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }
    }
}
