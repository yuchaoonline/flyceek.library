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
    /// Utility methods for the pager.
    /// </summary>
    public class PagerHelper
    {

        /// <summary>
        /// Calculate the total pages given records, records per page.
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="recordsPerPage"></param>
        /// <returns></returns>
        public static int CalculatePages(int totalRecords, int recordsPerPage)
        {
            float totalRecordsFloat = totalRecords;
            double totalPages = (totalRecordsFloat / recordsPerPage);
            totalPages = Math.Ceiling(totalPages);
            return (int)totalPages;
        }


        /// <summary>
        /// Determines whether or not there are query string params containing pager data.
        /// </summary>
        /// <param name="queryStringParams"></param>
        /// <returns></returns>
        public static bool ContainsUrlParams(NameValueCollection queryStringParams)
        {
            string pagerAction = queryStringParams[PagerConstants.CurrentPage];
            return (!string.IsNullOrEmpty(pagerAction));
        }


        /// <summary>
        /// Build the url params.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="selectedPage"></param>
        /// <param name="totalPages"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string BuildUrlWithParams(string url, int currentPage)
        {
            string urlParams = PagerConstants.CurrentPage + "=" + currentPage;
            return url + urlParams;
        }


        /// <summary>
        /// Build page numbers list.
        /// </summary>
        /// <param name="ctx">Pager action context</param>
        /// <param name="pagerSettings"></param>
        public static void BuildPageNumbersList(IList<PageNumber> pageNumbersList, PagerData pagerData, PagerSettings pagerSettings)
        {
            // Clear existing to build them.
            pageNumbersList.Clear();

            // Build each page number.
            for (int ndx = pagerData.StartingPage; ndx <= pagerData.EndingPage; ndx++)
            {
                string cssClass = (ndx == pagerData.CurrentPage) ? pagerSettings.CssCurrentPage : string.Empty;
                pageNumbersList.Add(new PageNumber(ndx, cssClass));
            }
        }

        /*
        /// <summary>
        /// Loads the pager state from either the url or form data via postback.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="pagerSettings"></param>
        /// <param name="urlParams"></param>
        /// <param name="totalPagesFromFormState"></param>
        /// <param name="currentPageFromFormState"></param>
        /// <param name="isPostBack"></param>
        public static void LoadPagerState(PagerActionContext ctx, PagerSettings pagerSettings,
            NameValueCollection urlParams, string totalPagesFromFormState,
            string currentPageFromFormState, bool isPostBack)
        {
            // Default to forms mode.
            ctx.OperatingMode = PagerOperatingMode.Forms;

            if (isPostBack && !string.IsNullOrEmpty(totalPagesFromFormState))
            {
                ctx.PageData.TotalPages = Convert.ToInt32(totalPagesFromFormState);
                ctx.PageData.CurrentPage = Convert.ToInt32(currentPageFromFormState);
                ctx.NewSelectedPage = ctx.PageData.CurrentPage;
                PagerCalculator.Calculate(ctx.PageData, pagerSettings);
            }
            else if (PagerHelper.ContainsUrlParams(urlParams))
            {
                PagerHelper.LoadPagerStateFromUrl(urlParams, ctx);
                ctx.OperatingMode = PagerOperatingMode.Url;
                PagerCalculator.Calculate(ctx.PageData, pagerSettings);
            }
        }


        /// <summary>
        /// Load using existing context.
        /// </summary>
        /// <param name="paramsCollection"></param>
        /// <param name="ctx"></param>
        public static void LoadPagerStateFromUrl(NameValueCollection paramCollection, PagerActionContext ctx)
        {
            // Get the action
            string action = paramCollection[PagerConstants.Action] as string;
            string strCurrentPage = paramCollection[PagerConstants.CurrentPage] as string;
            string strTotalPages = paramCollection[PagerConstants.TotalPages] as string;
            string strNewSelectedPage = paramCollection[PagerConstants.SelectedPage] as string;

            // Validate
            if (string.IsNullOrEmpty(action)) { ctx.Default(); return; }
            if (string.IsNullOrEmpty(strCurrentPage)) { ctx.Default(); return; }
            if (string.IsNullOrEmpty(strTotalPages)) { ctx.Default(); return; }

            PagerAction pageAction = PagerAction.First;
            int newSelectedPage = 1;
            int currentPage = 1;
            int totalPages = 1;

            try
            {
                pageAction = (PagerAction)Enum.Parse(typeof(PagerAction), action, true);

                // Get the current page and total pages.
                int.TryParse(strCurrentPage, out currentPage);
                int.TryParse(strTotalPages, out totalPages);
                newSelectedPage = currentPage;

                // Selected page
                if (!string.IsNullOrEmpty(strNewSelectedPage))
                {
                    int.TryParse(strNewSelectedPage, out newSelectedPage);
                }
            }
            catch (Exception)
            {
                pageAction = PagerAction.First;
                newSelectedPage = 1;
                currentPage = 1;
                totalPages = 1;
            }

            ctx.ActionType = pageAction;
            ctx.NewSelectedPage = newSelectedPage;
            ctx.PageData.SetCurrentPage(currentPage, totalPages);
        } 
        */
    }
}