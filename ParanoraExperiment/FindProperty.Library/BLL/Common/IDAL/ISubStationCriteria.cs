using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ISubStationCriteria
    {
        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay);

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay, string scpMkt);

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubStationCriteria> GetSubStationCriteria(int subWay,string scpMkt, string gscpId);
    }
}
