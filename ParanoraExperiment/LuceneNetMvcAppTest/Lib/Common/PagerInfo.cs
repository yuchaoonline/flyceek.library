using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneNetMvcAppTest.Lib.Common
{
    public class PagerInfo
    {
        public int CurrentPageIndex { get; set; }
        public int TotalItemCount { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }

        public PagerInfo()
        {
            CurrentPageIndex = 1;
        }
    }
}