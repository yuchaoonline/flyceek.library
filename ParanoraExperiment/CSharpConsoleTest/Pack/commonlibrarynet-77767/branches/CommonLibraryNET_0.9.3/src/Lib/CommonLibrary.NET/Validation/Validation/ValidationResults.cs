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
    public class ValidationResults : StatusResults, IValidationResults
    {
        /// <summary>
        /// Null object.
        /// </summary>
        public static readonly ValidationResults Empty = new ValidationResults();


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ValidationResults()
        {
        }


        /// <summary> 
        /// Construtor. 
        /// </summary> 
        /// <param name="isValid"></param> 
        public ValidationResults(List<StatusResult> results) : base(results)
        {
        }        


        /// <summary> 
        /// Passed validation ? 
        /// </summary> 
        public override bool IsValid
        {
            get
            {
                if (_results == null) return true;
                return _results.Count == 0;
            }
        }
    } 
}
