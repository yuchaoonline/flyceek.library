using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;

namespace FindProperty.Lib.BLL.Common.DAL.MDb
{
    public class SubStationCriteria:IDAL.ISubStationCriteria
    {

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay)
        {
            List<ViewModel.SubStationCriteria> subStationList = new List<ViewModel.SubStationCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagMain> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagMain, SHTagToSalesBlog.IDAL.ITagMain>().SelectTagMain(@"^"+subWay.ToString()+@"号线(\S)", "地铁");
            int id = 0;
            list.ForEach(x =>
            {
                string subStation = Regex.Match(x.Tag, @"\(\S+\)").Value;
                subStation = subStation.TrimStart('(').TrimEnd(')');
                subStationList.Add(new ViewModel.SubStationCriteria() { ID = (id++).ToString(), Value = x.Tag, DisplayString = subStation, Type = 2, WParam = x.AboutNum });
            });

            return subStationList;
        }

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay, string scpMkt)
        {
            List<ViewModel.SubStationCriteria> subStationList = new List<ViewModel.SubStationCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagMkt> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagMkt, SHTagToSalesBlog.IDAL.ITagMkt>().SelectTagMkt(scpMkt, @"^" + subWay.ToString() + @"号线(\S+)$", "地铁", 0);
            
            int id = 0;
            list.ForEach(x =>
            {
                string subStation = Regex.Match(x.Tag, @"\(\S+\)").Value;
                subStation = subStation.TrimStart('(').TrimEnd(')');
                subStationList.Add(new ViewModel.SubStationCriteria() { ID = (id++).ToString(), Value = x.Tag, DisplayString = subStation, Type = 2, WParam = x.TagCount });
            });
            return subStationList;
        }

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay,string scpMkt, string gscpId)
        {
            List<ViewModel.SubStationCriteria> subStationList = new List<ViewModel.SubStationCriteria>();

            List<SHTagToSalesBlog.ViewModel.TagGscp> list = PolicyInjectionFactory.Create().Create<SHTagToSalesBlog.DAL.MDb.TagGscp, SHTagToSalesBlog.IDAL.ITagGscp>().SelectTagGscp(scpMkt, gscpId, @"^" + subWay.ToString() + @"号线(\S+)$", "地铁", 0);

            int id = 0;
            list.ForEach(x =>
            {
                string subStation = Regex.Match(x.Tag, @"\(\S+\)").Value;
                subStation = subStation.TrimStart('(').TrimEnd(')');
                subStationList.Add(new ViewModel.SubStationCriteria() { ID = (id++).ToString(), Value = x.Tag, DisplayString = subStation, Type = 2, WParam = x.TagCount });
            });
            return subStationList;
        }
    }
}
