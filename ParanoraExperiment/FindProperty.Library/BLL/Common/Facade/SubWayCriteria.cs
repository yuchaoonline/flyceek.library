using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SubWayCriteria
    {
        private readonly ISubWayCriteria dal = DataAccessFactoryCreator.Create().SubWayCriteria();

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria()
        {
            return dal.GetSubwayCriteria();
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt)
        {
            List<ViewModel.SubWayCriteria> list = new List<ViewModel.SubWayCriteria>();
            list = dal.GetSubwayCriteria(scpMkt);
            return list;
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt,string gscpId)
        {
            return dal.GetSubwayCriteria(scpMkt,gscpId);
        }

        public List<ViewModel.SubWayCriteria> GetSubwayCriteriaEx(string scpMkt, string gscpId)
        {
            List<ViewModel.SubWayCriteria> list = new List<ViewModel.SubWayCriteria>();

            if (!string.IsNullOrEmpty(gscpId))
            {
                list = GetSubwayCriteria(scpMkt,gscpId);
            }
            else
            {
                if (!string.IsNullOrEmpty(scpMkt))
                {
                    list = GetSubwayCriteria(scpMkt);
                }
                else
                {
                    list = GetSubwayCriteria();
                }
            }
            return list;
        }
    }
}
