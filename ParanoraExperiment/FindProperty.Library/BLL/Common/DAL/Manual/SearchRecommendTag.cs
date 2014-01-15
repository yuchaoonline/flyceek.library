using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    class SearchRecommendTag:IDAL.ISearchRecommendTag
    {
        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory)
        {
            List<ViewModel.SearchRecommendTag> list = new List<ViewModel.SearchRecommendTag>();

            if (!string.IsNullOrEmpty(tagCategory))
            {

                string[] mainRecommendTag = ConfigInfo.MainRecommendTag.Split(',');


                if (mainRecommendTag.Length > 0)
                {
                    for (var i = 0; i < mainRecommendTag.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mainRecommendTag[i]))
                        {
                            list.Add(new ViewModel.SearchRecommendTag() { Tag = mainRecommendTag[i] ,Type=1});
                        }
                    }
                }
            }

            return list;
        }

        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt)
        {
            throw new NotImplementedException();
        }

        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt, string gscpId)
        {
            throw new NotImplementedException();
        }
    }
}
