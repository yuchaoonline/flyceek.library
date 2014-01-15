using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentHouseRentPriceCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetAgentHousePriceCriteria")]
        List<ViewModel.PriceCriteria> GetPriceCriteria();
    }
}
