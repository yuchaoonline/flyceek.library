using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.Aop.Componet;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IHouseRentOrderCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetHouseRentOrderCriteria")]
        List<ViewModel.OrderCriteria> GetOrderCriteria();
    }
}
