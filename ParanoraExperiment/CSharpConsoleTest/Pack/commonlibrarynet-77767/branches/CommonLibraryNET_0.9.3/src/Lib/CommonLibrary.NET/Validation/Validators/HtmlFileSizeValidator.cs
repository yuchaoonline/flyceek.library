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
using System.Web.UI.HtmlControls;

using ComLib;


namespace ComLib.ValidationSupport
{
    /// <summary>
    /// Import file size rule.
    /// </summary>
    public class HtmlFileSizeRule : ValidatorBase
    {
        private HtmlInputFile _file;
        private bool _restrictSize;
        private int _restrictedFileSizeInKiloBytes;
        private string _errorMessageNoFile;
        private string _errorMessageEmptyFileContent;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="restrictSize"></param>
        /// <param name="restrictedFileSizeInKiloBytes"></param>
        public HtmlFileSizeRule(HtmlInputFile inputFile, bool restrictSize, int restrictedFileSizeInKiloBytes,
            string errorMsgForNoFile, string errorMsgForEmptyFileContent)
        {
            _file = inputFile;
            _restrictSize = restrictSize;
            _restrictedFileSizeInKiloBytes = restrictedFileSizeInKiloBytes;
            _errorMessageNoFile = errorMsgForNoFile;
            _errorMessageEmptyFileContent = errorMsgForEmptyFileContent;
        }


        /// <summary>
        /// Validate that the size is valid.
        /// </summary>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results;
            bool useTarget = validationEvent.Target != null;
            
            HtmlInputFile file = useTarget ? (HtmlInputFile)target : _file;

            // Validate.
            if (!ValidationUtils.Validate(file == null, results, string.Empty, _errorMessageNoFile, null)) { return false; }
            if (!ValidationUtils.Validate(file.PostedFile == null, results, string.Empty, _errorMessageNoFile, null)) { return false; }
            if (!ValidationUtils.Validate(file.PostedFile.ContentLength == 0, results, string.Empty, _errorMessageEmptyFileContent, null)) { return false; }

            int initialErrorCount = results.Count;

            if (_restrictSize)
            {
                int fileSizeInKiloBytes = file.PostedFile.ContentLength / 1000;

                if (fileSizeInKiloBytes > _restrictedFileSizeInKiloBytes)
                {
                    results.Add(string.Empty, "File exceeds " + _restrictedFileSizeInKiloBytes + "K.", null);
                }
            }
            return results.Count == initialErrorCount;
        }
    }
}
