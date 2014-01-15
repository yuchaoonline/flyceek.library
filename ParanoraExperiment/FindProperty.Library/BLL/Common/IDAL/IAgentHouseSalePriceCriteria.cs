using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentHouseSalePriceCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetAgentHouseSalePriceCriteria")]
        List<ViewModel.PriceCriteria> GetPriceCriteria();
    }
}
