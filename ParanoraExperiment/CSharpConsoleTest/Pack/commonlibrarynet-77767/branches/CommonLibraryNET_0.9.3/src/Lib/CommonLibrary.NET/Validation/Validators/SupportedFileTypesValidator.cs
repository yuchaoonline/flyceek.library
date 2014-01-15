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
using ComLib.Collections;


namespace ComLib.ValidationSupport
{
    /// <summary>
    /// Validate the type of the import file.
    /// Currently must be .txt format.
    /// </summary>
    public class SupportFileTypesRule : ValidatorBase
    {
        private string _fileName;
        private DictionaryReadOnly<string, string> _supportedTypes;
        private string _errorMessagePrefix;


        /// <summary>
        /// email address
        /// </summary>
        /// <param name="email"></param>
        public SupportFileTypesRule(string fileName, string errorMessagePrefix,
            DictionaryReadOnly<string, string> supporedTypesLookUp)
        {
            _fileName = fileName;
            _supportedTypes = supporedTypesLookUp;
            _errorMessagePrefix = errorMessagePrefix;
        }


        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results;
            bool useTarget = validationEvent.Target != null;
            string fileName = useTarget ? (string)target : _fileName;

            int initialErrorCount = results.Count;

            // Check file name was provided.
            bool isNullOrEmpty = string.IsNullOrEmpty(fileName);
            if (!ValidationUtils.Validate(isNullOrEmpty, results, string.Empty, "File not provided.", null))
                return false;

            // Check that the file has an extension.
            int ndxExtensionPeriod = fileName.LastIndexOf(".");
            if (!ValidationUtils.Validate(ndxExtensionPeriod < 0, results, string.Empty, _errorMessagePrefix, null))
                return false;

            // Error could occurr with file name = test. (ok for now)
            // Check for .txt extension.
            string fileExtension = fileName.Substring(ndxExtensionPeriod + 1);
            fileExtension = fileExtension.Trim().ToLower();

            // Check if valid format.
            if (!IsValidFormat(fileExtension))
                results.Add(string.Empty, _errorMessagePrefix, null);

            return results.Count == initialErrorCount;
        }


        /// <summary>
        /// Determines if the format is supported.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        protected bool IsValidFormat(string format)
        {
            return _supportedTypes.ContainsKey(format);
        }
    }
}
