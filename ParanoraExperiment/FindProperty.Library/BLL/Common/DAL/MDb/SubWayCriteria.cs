using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using SHTagToSalesBlog=FindProperty.Lib.BLL.SHTagToSalesBlog;

namespace FindProperty.Lib.BLL.Common.DAL.MDb
{
    public class SubWayCriteria:IDAL.ISubWayCriteria
    {
        public List<ViewModel.SubWayCriteria> GetSubwayCriteria()
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagMain> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagMain, SHTagToSalesBlog.IDAL.ITagMain>().SelectTagMain(@"^\d+号线$", "地铁");

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2, WParam = x.AboutNum });
            });
            return subWayList;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt)
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagMkt> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagMkt, SHTagToSalesBlog.IDAL.ITagMkt>().SelectTagMkt(scpMkt, @"^\d+号线$", "地铁", 0);

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                if (int.Parse(subWay) < 13)
                {
                    subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2, WParam = x.TagCount });
                }
            });

            return subWayList;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt, string gscpId)
        {
            List<ViewModel.SubWayCriteria> subWayList = new List<ViewModel.SubWayCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagGscp> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagGscp, SHTagToSalesBlog.IDAL.ITagGscp>().SelectTagGscp(scpMkt, gscpId, @"^\d+号线$", "地铁", 0);

            list.OrderBy((x) => { return int.Parse(Regex.Match(x.Tag, @"^\d+").Value); }).ToList().ForEach(x =>
            {
                string subWay = Regex.Match(x.Tag, @"^\d+").Value;
                if (int.Parse(subWay) < 13)
                {
                    subWayList.Add(new ViewModel.SubWayCriteria() { ID = subWay, Value = subWay, DisplayString = subWay + "号线", Type = 2, WParam = x.TagCount });
                }
            });

            return subWayList;
        }
    }
}
