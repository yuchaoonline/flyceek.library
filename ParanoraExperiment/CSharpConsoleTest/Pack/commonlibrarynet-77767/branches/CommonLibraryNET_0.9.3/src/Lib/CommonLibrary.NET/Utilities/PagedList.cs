using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComLib
{
    /// <summary>
    /// Paged list to represent the page index, size, total records, and total pages
    /// in addition to the items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }        
        

        /// <summary>
        /// Initialize w/ items, page index, size and total records.
        /// </summary>
        /// <param name="items">The items representing the list.</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        public PagedList(int pageIndex, int pageSize, int totalRecords, IList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalRecords;
            TotalPages = (int) Math.Ceiling(TotalCount / (double)PageSize);
            this.AddRange(items);
        }
    }
}
