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
    /// Rule for supported text formats for import files.
    /// </summary>
    public class ImageSupportedFileTypesRule : SupportFileTypesRule
    {
        private static DictionaryReadOnly<string, string> _readOnlySupportedTypes;
        private const string _errorMessage = "Image must be one of the supported types (gif, jpeg, tiff, png).";


        static ImageSupportedFileTypesRule()
        {
            IDictionary<string, string> supportedTypes = new Dictionary<string, string>();
            supportedTypes.Add("gif", "gif");
            supportedTypes.Add("jpeg", "jpeg");
            supportedTypes.Add("tiff", "tiff");
            supportedTypes.Add("png", "png");
            supportedTypes.Add("jpg", "jpg");
            _readOnlySupportedTypes = new DictionaryReadOnly<string, string>(supportedTypes);
        }


        /// <summary>
        /// Support file types for import file.
        /// </summary>
        /// <param name="fileName"></param>
        public ImageSupportedFileTypesRule(string fileName)
            : base(fileName, _errorMessage, _readOnlySupportedTypes)
        {
        }
    }
}
