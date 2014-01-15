using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentHouseSizeCriteria
    {
        [CommonCallHandler(CacheSecond = 2592000, CacheKey = "GetAgentHouseSizeCriteria")]
        List<ViewModel.SizeCriteria> GetSizeCriteria();
    }
}
