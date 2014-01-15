using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL;
using System.Text.RegularExpressions;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class SubWayCriteria:IDAL.ISubWayCriteria
    {
        public List<ViewModel.SubWayCriteria> GetSubwayCriteria()
        {
            List<ViewModel.SubWayCriteria> list = new List<ViewModel.SubWayCriteria>();

            for (var i = 1; i < 14; i++)
            {
                list.Add(new ViewModel.SubWayCriteria() { ID = i.ToString(),Value=i.ToString(), DisplayString = i.ToString()+"号线", Type = 2 });
            }
            return list;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt)
        {
            return PolicyInjectionFactory.Create().Wrap<ISubWayCriteria>(this).GetSubwayCriteria();
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt,string gscpId)
        {
            return PolicyInjectionFactory.Create().Wrap<ISubWayCriteria>(this).GetSubwayCriteria();
        }
    }
}
