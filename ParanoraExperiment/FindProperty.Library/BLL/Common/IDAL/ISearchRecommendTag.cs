using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ISearchRecommendTag
    {
        [CommonCallHandler]
        List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory);

        [CommonCallHandler]
        List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt);

        [CommonCallHandler]
        List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt, string gscpId);
    }
}
