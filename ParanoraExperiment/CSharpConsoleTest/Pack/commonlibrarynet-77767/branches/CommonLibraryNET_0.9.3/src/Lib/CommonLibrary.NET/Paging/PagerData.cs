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
    /// Holds the paging data.
    /// </summary>
    public class PagerData : ICloneable
    {
        private int _currentPage;
        private int _totalPages;
        private int _previousPage;
        private int _startingPage;
        private int _endingPage;
        private int _nextPage;
        private PagerSettings _pagerSettings;


        /// <summary>
        /// Constructor.
        /// </summary>
        public PagerData() : this(1, 1, PagerSettings.Default)
        {
        }


        /// <summary>
        /// Constructor to set properties.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="totalPages"></param>
        /// <param name="numPagesInRange"></param>
        public PagerData(int currentPage, int totalPages) : this(currentPage, totalPages, PagerSettings.Default)
        {
        }


        /// <summary>
        /// Constructor to set properties.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="totalPages"></param>
        /// <param name="numPagesInRange"></param>
        public PagerData(int currentPage, int totalPages, PagerSettings settings)
        {
            _pagerSettings = settings;
            SetCurrentPage(currentPage, totalPages);
        }


        #region Data Members
        /// <summary>
        /// Set the current page and calculate the rest of the pages.
        /// </summary>
        /// <param name="currentPage"></param>
        public void SetCurrentPage(int currentPage)
        {
            SetCurrentPage(currentPage, _totalPages);
        }


        /// <summary>
        /// Set the current page and calculate the rest of the pages.
        /// </summary>
        /// <param name="currentPage"></param>
        public void SetCurrentPage(int currentPage, int totalPages)
        {
            if (totalPages < 0) totalPages = 1;
            if (currentPage < 0 || currentPage > totalPages) currentPage = 1;

            _currentPage = currentPage;
            _totalPages = totalPages;
            Calculate();
        }


        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }           
        }


        /// <summary>
        /// Total pages available
        /// </summary>
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }


        /// <summary>
        /// Always 1.
        /// </summary>
        public int FirstPage { get { return 1; } }


        /// <summary>
        /// What is the previous page number if applicable.
        /// </summary>
        public int PreviousPage
        {
            get { return _previousPage; }
            set { _previousPage = value; }
        }


        /// <summary>
        /// Starting page.
        /// e.g.
        /// can be 1 as in                    1, 2, 3, 4, 5   next, last
        /// can be 6 as in   first, previous, 6, 7, 8, 9, 10  next, last
        /// </summary>
        public int StartingPage
        {
            get { return _startingPage; }
            set { _startingPage = value; }
        }


        /// <summary>
        /// Starting page.
        /// e.g.
        /// can be 5 as in                     1, 2, 3, 4, 5   next, last
        /// can be 10 as in   first, previous, 6, 7, 8, 9, 10  next, last
        /// </summary>
        public int EndingPage
        {
            get { return _endingPage; }
            set { _endingPage = value; }
        }


        /// <summary>
        /// What is the next page number if applicable.
        /// </summary>
        public int NextPage
        {
            get { return _nextPage; }
            set { _nextPage = value; }
        }


        /// <summary>
        /// Last page number is always the Total pages.
        /// </summary>
        public int LastPage { get { return _totalPages; } }


        /// <summary>
        /// Whether or not there are more than 1 page.
        /// </summary>
        public bool IsMultiplePages
        {
            get { return _totalPages > 1; }
        }
        #endregion


        #region Navigation Checks
        /// <summary>
        /// Can show First page link?
        /// </summary>
        public bool CanShowFirst
        {
            get { return (_startingPage != 1); }
        }
        

        /// <summary>
        /// Can show previous link?
        /// </summary>
        public bool CanShowPrevious
        {
            get { return (_startingPage > 2); }
        }


        /// <summary>
        /// Can show Next page link?
        /// </summary>
        public bool CanShowNext
        {
            get { return (_endingPage < (_totalPages - 1)); }
        }

        
        /// <summary>
        /// Can show Last page link?
        /// </summary>
        public bool CanShowLast
        {
            get { return (_endingPage != _totalPages); }
        }
        #endregion                


        #region Navigation
        /// <summary>
        /// Move to the fist page.
        /// </summary>
        public void MoveFirst()
        {
            _currentPage = 1;
            Calculate();
        }


        /// <summary>
        /// Move to the previous page.
        /// </summary>
        public void MovePrevious()
        {
            _currentPage = _previousPage;
            Calculate();
        }


        /// <summary>
        /// Move to the next page.
        /// </summary>
        public void MoveNext()
        {
            _currentPage = _nextPage;
            Calculate();
        }


        /// <summary>
        /// Move to the last page.
        /// </summary>
        public void MoveLast()
        {
            _currentPage = _totalPages;
            Calculate();
        }


        /// <summary>
        /// Move to a specific page.
        /// </summary>
        /// <param name="selectedPage"></param>
        public void MoveToPage(int selectedPage)
        {
            _currentPage = selectedPage;
            Calculate();
        }
        #endregion


        #region Calculation
        /// <summary>
        /// Calcuate pages.
        /// </summary>
        public void Calculate()
        {
            PagerCalculator.Calculate(this, _pagerSettings);
        }
        #endregion


        #region ICloneable Members
        /// <summary>
        /// Clones the object.
        /// Good as long as properties are not objects.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }




    /// <summary>
    /// Page number to store page and css class for it.
    /// </summary>
    public class PageNumber
    {
        private int _pageNumber;
        private string _cssClass;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="cssClass"></param>
        public PageNumber(int number, string cssClass)
        {
            _pageNumber = number;
            _cssClass = cssClass;
        }


        /// <summary>
        /// page number
        /// </summary>
        public int Page
        {
            get { return _pageNumber; }
        }


        /// <summary>
        /// Css class associated with this page.
        /// </summary>
        public string CssClass
        {
            get { return _cssClass; }
        }
    }



    /// <summary>
    /// Contextual information when querying records page by page.
    /// </summary>
    public class PageContext
    {
        public int PageIndex;
        public int PageSize;
        public int TotalRecords;
    }

}