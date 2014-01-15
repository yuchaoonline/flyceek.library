using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentListOrderCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetAgentListOrderCriteria")]
        List<ViewModel.OrderCriteria> GetOrderCriteria();
    }
}
