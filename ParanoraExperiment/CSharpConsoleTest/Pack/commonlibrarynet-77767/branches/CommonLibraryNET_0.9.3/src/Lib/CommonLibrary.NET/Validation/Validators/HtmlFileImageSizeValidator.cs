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
    public class ImageFileSizeRule : HtmlFileSizeRule
    {
        private const string _errorNoFile = "Please upload an image file of type ( gif, jpeg, tiff, png ).";
        private const string _errorEmptyFile = "Image file is empty.";


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="restrictSize"></param>
        /// <param name="restrictedFileSizeInKiloBytes"></param>
        public ImageFileSizeRule(HtmlInputFile inputFile, bool restrictSize, int restrictedFileSizeInKiloBytes)
            : base(inputFile, restrictSize, restrictedFileSizeInKiloBytes, _errorNoFile, _errorEmptyFile)
        {
        }
    }
}
