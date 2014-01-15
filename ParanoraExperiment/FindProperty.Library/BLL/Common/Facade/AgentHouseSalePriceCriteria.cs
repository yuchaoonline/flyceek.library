using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentHouseSalePriceCriteria
    {
        private readonly IAgentHouseSalePriceCriteria dal = DataAccessFactoryCreator.Create().AgentHouseSalePriceCriteria();
        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            return dal.GetPriceCriteria();
        }
    }
}
