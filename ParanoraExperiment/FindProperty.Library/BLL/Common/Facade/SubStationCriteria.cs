using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SubStationCriteria
    {
        private readonly ISubStationCriteria dal = DataAccessFactoryCreator.Create().SubStationCriteria();

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay)
        {
            return dal.GetSubStationCriteria(subWay);
        }

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay, string scpMkt)
        {
            return dal.GetSubStationCriteria(subWay, scpMkt);
        }

        public List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay,string scpMkt, string gscpId)
        {
            return dal.GetSubStationCriteria(subWay,scpMkt, gscpId);
        }

        public List<ViewModel.SubStationCriteria> GetSubStationCriteriaEx(int? subWay, string gscpId,string scpMkt)
        {
            List<ViewModel.SubStationCriteria> list = new List<ViewModel.SubStationCriteria>();
            if (subWay.HasValue)
            {
                if (!string.IsNullOrEmpty(gscpId))
                {
                    list = GetSubStationCriteria(subWay.Value, scpMkt,gscpId);
                }
                else
                {
                    if (!string.IsNullOrEmpty(scpMkt))
                    {
                        list = GetSubStationCriteria(subWay.Value, scpMkt);
                    }
                    else
                    {
                        list = GetSubStationCriteria(subWay.Value);
                    }
                }
            }
            return list;
        } 

    }
}
