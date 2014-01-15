using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ISearchTagCategoryCheck
    {
        string SearchTagMainCategoryCheck(string keyWord);

        string SearchTagMktCategoryCheck(string keyWord);

        string SearchTagGscpCategoryCheck(string keyWord);
    }
}
