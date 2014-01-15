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
    /// Delegate that can build the url for the pager.
    /// This is useful when used with URL rewriting.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="currentPage"></param>
    /// <param name="selectedPage"></param>
    /// <param name="totalPages"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public delegate string PagerUrlBuilder(string url, int currentPage);
    


    /// <summary>
    /// Pager url mode builder interface.
    /// </summary>
    public interface IPagerUrlModeBuilder
    {
        /// <summary>
        /// Build the entire html for the pager.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        string Build(PagerActionContext ctx, ComLib.Paging.PagerSettings settings);
    }



    /// <summary>
    /// Buider class that builds the pager in Url mode.
    /// </summary>
    public class PagerUrlModeBuilder : IPagerUrlModeBuilder
    {
        private static IPagerUrlModeBuilder _instance;
        private static readonly object _syncRoot = new object();


        /// <summary>
        /// Get singleton instance.
        /// </summary>
        /// <returns></returns>
        public static IPagerUrlModeBuilder Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new PagerUrlModeBuilder();
                        }
                    }
                }
                return _instance;
            }
        }
        

        /// <summary>
        /// Build the page number links as url links.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public string Build(PagerActionContext ctx, PagerSettings settings)
        {
            PagerData pagerData = ctx.PageData;

            // Get reference to the default or custom url builder for this pager.
            // A custom url builder may be used for SEO.
            PagerUrlBuilder urlBuilder = null;
            if (ctx.UrlBuilder == null)
                urlBuilder = new PagerUrlBuilder(PagerHelper.BuildUrlWithParams);
            else
                urlBuilder = ctx.UrlBuilder;

            StringBuilder buffer = new StringBuilder();
            string cssClass = string.Empty;
            string urlParams = string.Empty;
            string url = ctx.Url;

            buffer.Append("<table><tr>");

            // Build the previous page link.
            if (pagerData.CanShowPrevious)
            {
                // Previous
                url = urlBuilder(ctx.Url, pagerData.CurrentPage - 1);
                buffer.Append("<td><span class=\"" + settings.CssClass + "\">");
                buffer.Append("<a href=\"" + url + "\">" + "&#171;" + "</a>");
                buffer.Append("</span></td>");
            }

            // Build the starting page link.            
            if (pagerData.CanShowFirst)
            {
                // First
                url = urlBuilder(ctx.Url, 1);
                buffer.Append("<td><span class=\"" + settings.CssClass + "\">");
                buffer.Append("<a href=\"" + url + "\">" + 1 + "</a>");
                buffer.Append("</span></td>");

                // This is to avoid putting ".." between 1 and 2 for example.
                // If 1 is the starting page and we want to display 2 as starting page.
                if (pagerData.CanShowPrevious)
                {
                    buffer.Append("<td> .. </td>");
                }
            }

            // Each page number.
            for (int ndx = pagerData.StartingPage; ndx <= pagerData.EndingPage; ndx++)
            {
                cssClass = (ndx == pagerData.CurrentPage) ? settings.CssCurrentPage : string.Empty;
                url = urlBuilder(ctx.Url, ndx);

                // Build page number link. <a href="<%=Url %>" class="<%=cssClass %>" ><%=ndx %></a>                
                buffer.Append("<td><span class=\"" + settings.CssClass + "\">");
                buffer.Append("<a href=\"" + url + "\" class=\"" + cssClass + "\">" + ndx + "</a>");
                buffer.Append("</span></td>");
            }

            // Build the  ending page link.
            if (pagerData.CanShowLast)
            {
                url = urlBuilder(ctx.Url, pagerData.TotalPages);

                // This is to avoid putting ".." between 7 and 8 for example.
                // If 7 is the ending page and we want to display 8 as total pages.
                if (pagerData.CanShowNext)
                {
                    buffer.Append("<td> .. </td>");
                }
                buffer.Append("<td><span class=\"" + settings.CssClass + "\">");
                buffer.Append("<a href=\"" + url + "\">" + pagerData.TotalPages + "</a>");
                buffer.Append("</span></td>");
            }

            // Build the next page link.
            if (pagerData.CanShowNext)
            {
                // Previous
                url = urlBuilder(ctx.Url, pagerData.CurrentPage + 1);
                buffer.Append("<td><span class=\"" + settings.CssClass + "\">");
                buffer.Append("<a href=\"" + url + "\">" + "&#187;" + "</a>");
                buffer.Append("</span></td>");
            }

            buffer.Append("</tr></table>");
            string content = buffer.ToString();
            return content;
        }
    }
}