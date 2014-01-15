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
using System.Collections.Generic;


namespace ComLib.Validation
{

    /// <summary>
    /// A single Validation result
    /// </summary>
    public class ValidationResult : StatusResult, IValidationResult
    {
        private IValidator _validator;


        /// <summary>
        /// Initalized all the read-only
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <param name="isValid"></param>
        public ValidationResult(string key, string message, object target, IValidator validator) 
            : base(key, message, target)
        {
            _validator = validator;
        }


        /// <summary>
        /// Initalize
        /// </summary>
        /// <param name="error"></param>
        /// <param name="isValid"></param>
        public ValidationResult(string key, string message, object target) 
            : base(key, message, target)
        {            
        }


        /// <summary>
        /// Initalize
        /// </summary>
        /// <param name="error"></param>
        /// <param name="isValid"></param>
        public ValidationResult(string key, string message)
            : base(key, message, null)
        {
        }


        /// <summary>
        /// Get the validation ( if applicable ) that is associated with this result.
        /// </summary>
        public IValidator Validator
        {
            get { return _validator; }
        }
    }
}