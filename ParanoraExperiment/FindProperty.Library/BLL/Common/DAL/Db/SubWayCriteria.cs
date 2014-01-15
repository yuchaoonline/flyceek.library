using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;

namespace FindProperty.Lib.BLL.Common.DAL.Db
{
    public class SubWayCriteria:IDAL.ISubWayCriteria
    {
        public List<ViewModel.SubWayCriteria> GetSubwayCriteria()
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagMkt> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.Db.TagMkt, SHTagToSalesBlog.IDAL.ITagMkt>().SelectTagMktGroupByTag("", @"[0-9]%号线", "地铁", 0);

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2, WParam = x.TagCount });
            });

            return subWayList;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt)
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            //List<SHTagToSalesBlog.ViewModel.TagMkt> list = new SHTagToSalesBlog.Facade.TagMkt().SelectTagMkt(scpMkt, @"^\d+号线$", "地铁", 0);

            List<SHTagToSalesBlog.ViewModel.TagMkt> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.Db.TagMkt, SHTagToSalesBlog.IDAL.ITagMkt>().SelectTagMktGroupByTag(scpMkt, @"[0-9]%号线", "地铁", 0);

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2,WParam=x.TagCount });
            });

            return subWayList;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt, string gscpId)
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagGscp> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.Db.TagGscp, SHTagToSalesBlog.IDAL.ITagGscp>().SelectTagGscpGroupByTag(scpMkt, gscpId, @"[0-9]%号线", "地铁", 0);

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2, WParam = x.TagCount });
            });

            return subWayList;
        }
    }
}
