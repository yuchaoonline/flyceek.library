using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.Aop.Componet;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IHouseBedRoomCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetHouseBedRoomCriteria")]
        List<ViewModel.BedRoomCriteria> GetBedRoomCriteria();
   }
}
