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
using System.Text;
using System.Text.RegularExpressions;

using ComLib;


namespace ComLib.ValidationSupport
{
    public class RequiredOptionalRegExValidator : ValidatorBase
    {
        protected string _text;
        protected bool _isRequired;
        protected string _key;
        protected string _messageIfEmptyAndRequired;
        protected string _messageIfRegExFails;
        protected string _regEx;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">"Email" or "Url"</param>
        /// <param name="text">kishore@h.com or "http://www.somesite.com"</param>
        /// <param name="isRequired">Whether or not the text is required.</param>
        /// <param name="message">The message to use if validation fails.</param>
        public RequiredOptionalRegExValidator(string key, string text, bool isRequired)
        {
            _text = text;
            _isRequired = isRequired;
            _key = key;
        }


        /// <summary>
        /// Validate the target object( text ).
        /// </summary>
        /// <param name="target"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results;
            bool useTarget = validationEvent.Target != null;
            
            string text = useTarget ? (string)target : _text;

            // Not required and email not supplied.
            if (!_isRequired && string.IsNullOrEmpty(text)) { return true; }

            int initialErrorCount = results.Count;

            bool isNullOrEmpty = string.IsNullOrEmpty(text);
            ValidationUtils.Validate(isNullOrEmpty, results, _key, _messageIfEmptyAndRequired, text);

            if (!isNullOrEmpty)
            {
                // Now check the format.
                Regex regex = new Regex(_regEx);

                if (!regex.IsMatch(text))
                {
                    results.Add(_key, _messageIfRegExFails, null);
                }
            }
            return results.Count == initialErrorCount;
        }
    }
}
