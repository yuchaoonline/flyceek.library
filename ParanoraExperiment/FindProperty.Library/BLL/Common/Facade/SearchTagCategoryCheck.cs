using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SearchTagCategoryCheck
    {
        private readonly ISearchTagCategoryCheck dal = DataAccessFactoryCreator.Create().SearchTagCategoryCheck();

        public string SearchTagMainCategoryCheck(string keyWord)
        {
            return dal.SearchTagMainCategoryCheck(keyWord);
        }

        public string SearchTagMktCategoryCheck(string keyWord)
        {
            return dal.SearchTagMktCategoryCheck(keyWord);
        }

        public string SearchTagGscpCategoryCheck(string keyWord)
        {
            return dal.SearchTagGscpCategoryCheck(keyWord);
        }
    }
}
