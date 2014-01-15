using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SearchRecommendTag
    {
        private readonly ISearchRecommendTag dal = DataAccessFactoryCreator.Create().SearchRecommendTag();

        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string keyWord, string scpMkt, string gscpId)
        {
            var tagCategory=new SearchTagCategoryCheck();

            string mainSelectTagCategory = tagCategory.SearchTagMainCategoryCheck(keyWord);
            string scpMktSelectTagCategory = tagCategory.SearchTagMktCategoryCheck(keyWord);
            string gscpSelectTagCategory = tagCategory.SearchTagGscpCategoryCheck(keyWord);

            List<ViewModel.SearchRecommendTag> list = new List<ViewModel.SearchRecommendTag>();

            if (!string.IsNullOrEmpty(gscpId))
            {
                list = dal.SelectSearchRecommendTag(gscpSelectTagCategory, scpMkt, gscpId);
            }
            else
            {
                if (!string.IsNullOrEmpty(scpMkt))
                {
                    list = dal.SelectSearchRecommendTag(scpMktSelectTagCategory, scpMkt);
                }
                else
                {
                    if (mainSelectTagCategory == "学校")
                    {
                        list = dal.SelectSearchRecommendTag(mainSelectTagCategory);
                    }
                    else
                    {
                        list = new DAL.Manual.SearchRecommendTag().SelectSearchRecommendTag(mainSelectTagCategory);
                    }
                }
            }

            return list;
        }
    }
}
