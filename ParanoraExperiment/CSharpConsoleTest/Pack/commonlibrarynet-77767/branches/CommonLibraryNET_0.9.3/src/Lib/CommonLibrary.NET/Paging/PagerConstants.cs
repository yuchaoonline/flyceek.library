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
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;



namespace ComLib.Paging
{

    /// <summary>
    /// Actions that can be done on the pager.
    /// These are clickable links.
    /// </summary>
    public enum PagerAction { First = 0, Last, Previous, Next, SelectedPage };



    /// <summary>
    /// How the pager operates, either Url based on forms/post back based.
    /// Url based is used for making the paging SEO friendly.
    /// </summary>
    public enum PagerOperatingMode { Url, Forms };



    /// <summary>
    /// Pager constants.
    /// Keeping the names as small as possible.
    /// </summary>
    public class PagerConstants
    {
        public const string Action = "pgrAct";
        public const string CurrentPage = "pagenum";
        public const string TotalPages = "totalpages";
        public const string SelectedPage = "pgrS";
    }
}