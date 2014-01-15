using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.Aop.Componet;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ISubWayCriteria
    {
        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubWayCriteria> GetSubwayCriteria();

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt);

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.SubWayCriteria> GetSubwayCriteria(string scpMkt,string gscpId);
    }
}
