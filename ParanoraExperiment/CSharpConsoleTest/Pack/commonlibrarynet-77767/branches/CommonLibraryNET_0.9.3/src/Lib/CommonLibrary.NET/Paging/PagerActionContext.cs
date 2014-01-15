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
    /// Class to present the specific action on the pager
    /// and the selected page number
    /// </summary>
    public class PagerActionContext
    {
        /// <summary>
        /// Any of the possible actions.
        /// </summary>
        public PagerAction ActionType;


        /// <summary>
        /// Operating mode URL (SEO friendly) or forms/postback.
        /// </summary>
        public PagerOperatingMode OperatingMode = PagerOperatingMode.Url;


        /// <summary>
        /// Represent the page number a user clicks on.
        /// </summary>
        public int NewSelectedPage = 1;


        /// <summary>
        /// Url use for url mode.
        /// </summary>
        public string Url;


        /// <summary>
        /// Delegate to call for building url.
        /// Optional. can be null, in which case the Url property
        /// needs to be set.
        /// </summary>
        public PagerUrlBuilder UrlBuilder;


        /// <summary>
        /// Existing pager data.
        /// </summary>
        public PagerData PageData;


        /// <summary>
        /// Create a default page action context.
        /// Represents the first page.
        /// </summary>
        /// <returns></returns>
        public static PagerActionContext CreateDefault()
        {
            PagerActionContext ctx = new PagerActionContext();
            ctx.ActionType = PagerAction.First;
            ctx.OperatingMode = PagerOperatingMode.Forms;
            ctx.Url = string.Empty;
            ctx.PageData = new PagerData();
            ctx.NewSelectedPage = 1;
            return ctx;
        }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public PagerActionContext()
        {
        }


        /// <summary>
        /// Constructor to initialize properties.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="pagerData"></param>
        public PagerActionContext(PagerAction action, PagerData pagerData, int newSelectedPage)
        {
            ActionType = action;
            PageData = pagerData;
            NewSelectedPage = newSelectedPage;
        }


        /// <summary>
        /// Default the properties.
        /// </summary>
        public void Default()
        {
            ActionType = PagerAction.First;
            OperatingMode = PagerOperatingMode.Forms;
            Url = string.Empty;
            PageData.TotalPages = 1;
            PageData.CurrentPage = 1;
            NewSelectedPage = 1;
        }
    }
}