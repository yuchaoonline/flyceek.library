using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using ShTag = FindProperty.Lib.BLL.SHTagToSalesBlog;

namespace FindProperty.Lib.BLL.Common.DAL.Db
{
    public class SearchRecommendTag:IDAL.ISearchRecommendTag
    {
        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory)
        {
            List<ShTag.ViewModel.TagMain> tagMains = PolicyInjectionFactory.Create().Create<ShTag.DAL.Db.TagMain, ShTag.IDAL.ITagMain>().SelectTagMain("", tagCategory, 30, "SNum", "desc");

            List<ViewModel.SearchRecommendTag> list = new List<ViewModel.SearchRecommendTag>();

            tagMains.ForEach((x) =>
            {
                list.Add(new ViewModel.SearchRecommendTag() { Tag = x.Tag, TagCategory = x.TagCategory, Num = x.SNum.Value });
            });
            return list;
        }

        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt)
        {
            List<ShTag.ViewModel.TagMkt> tagMkts = PolicyInjectionFactory.Create().Create<ShTag.DAL.Db.TagMkt, ShTag.IDAL.ITagMkt>().SelectTagMkt(scpMkt, "", tagCategory, 30, "TagCount", "desc");

            List<ViewModel.SearchRecommendTag> list = new List<ViewModel.SearchRecommendTag>();

            tagMkts.ForEach((x) =>
            {
                list.Add(new ViewModel.SearchRecommendTag() { Tag = x.Tag, TagCategory = x.TagCategory, Num = (int)x.TagCount.Value });
            });

            return list;
        }

        public List<ViewModel.SearchRecommendTag> SelectSearchRecommendTag(string tagCategory, string scpMkt, string gscpId)
        {
            List<ShTag.ViewModel.TagGscp> tagGscps = PolicyInjectionFactory.Create().Create<ShTag.DAL.Db.TagGscp, ShTag.IDAL.ITagGscp>().SelectTagGscp(scpMkt, gscpId, "", tagCategory, 30, "TagCount", "desc");

            List<ViewModel.SearchRecommendTag> list = new List<ViewModel.SearchRecommendTag>();

            tagGscps.ForEach((x) =>
            {
                list.Add(new ViewModel.SearchRecommendTag() { Tag = x.Tag, TagCategory = x.TagCategory, Num = (int)x.TagCount.Value });
            });

            return list;
        }
    }
}
