using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class SearchTagCategoryCheck:IDAL.ISearchTagCategoryCheck
    {
        private Regex _regex = new Regex("((学区)|(xuequ))+", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public string SearchTagMainCategoryCheck(string keyWord)
        {
            string tagCategory = "楼盘";
            if (!string.IsNullOrEmpty(keyWord) && _regex.Match(keyWord).Success)
            {
                tagCategory = "学校";
            }
            return tagCategory;
        }

        public string SearchTagMktCategoryCheck(string keyWord)
        {
            string tagCategory = "房源";
            if (!string.IsNullOrEmpty(keyWord) && _regex.Match(keyWord).Success)
            {
                tagCategory = "学校";
            }
            return tagCategory;
        }

        public string SearchTagGscpCategoryCheck(string keyWord)
        {
            string tagCategory = "房源";
            if (!string.IsNullOrEmpty(keyWord) && _regex.Match(keyWord).Success)
            {
                tagCategory = "学校";
            }
            return tagCategory;
        }
    }
}
