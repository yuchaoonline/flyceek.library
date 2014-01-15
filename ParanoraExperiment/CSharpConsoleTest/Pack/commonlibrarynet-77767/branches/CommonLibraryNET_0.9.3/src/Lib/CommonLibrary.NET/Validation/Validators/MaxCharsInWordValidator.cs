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

using ComLib;


namespace ComLib.ValidationSupport
{
    /// <summary>
    /// Rule to prevent a single word from having more than x number of characters.
    /// </summary>
    public class MaxCharsInWordRule : ValidatorBase
    {
        private string _errorMessage;
        private string _text;
        private int _maxCharsInWord = 26;


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxChars"></param>
        /// <param name="errorMessage"></param>
        public MaxCharsInWordRule(string text, int maxChars, string errorMessage)
        {
            _text = text;
            _maxCharsInWord = maxChars;
            _errorMessage = errorMessage;
        }


        /// <summary>
        /// Is valid - text doesn't contain any word that has
        /// more than maxChars specified.
        /// </summary>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results;
            bool useTarget = validationEvent.Target != null;
            
            string text = useTarget ? (string)target : _text;

            if (string.IsNullOrEmpty(text)) { return true; }

            bool isSpacerNewLine = false;
            int currentPosition = 0;
            int ndxSpace = StringHelpers.GetIndexOfSpacer(text, currentPosition, ref isSpacerNewLine);

            //Check if single very long word ( no spaces )
            if (ndxSpace < 0 && text.Length > _maxCharsInWord)
            {
                results.Add(_errorMessage + _maxCharsInWord + " chars.");
                return false;
            }

            while ((currentPosition < text.Length && ndxSpace > 0))
            {
                //Lenght of word 
                int wordLength = ndxSpace - (currentPosition + 1);
                if (wordLength > _maxCharsInWord)
                {
                    results.Add(_errorMessage + _maxCharsInWord + " chars.");
                    return false;
                }
                currentPosition = ndxSpace;
                ndxSpace = StringHelpers.GetIndexOfSpacer(text, (currentPosition + 1), ref isSpacerNewLine);
            }

            // Final check.. no space found but check complete length now.
            if (currentPosition < text.Length && ndxSpace < 0)
            {
                //Lenght of word 
                int wordLength = (text.Length - 1) - currentPosition;
                if (wordLength > _maxCharsInWord)
                {
                    results.Add(_errorMessage + _maxCharsInWord + " chars.");
                    return false;
                }
            }
            return true;
        }
    }
}
