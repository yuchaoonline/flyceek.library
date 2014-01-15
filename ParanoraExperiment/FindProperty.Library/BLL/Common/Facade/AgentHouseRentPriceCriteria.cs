using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentHouseRentPriceCriteria
    {
        private readonly IAgentHouseRentPriceCriteria dal = DataAccessFactoryCreator.Create().AgentHouseRentPriceCriteria();
        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            return dal.GetPriceCriteria();
        }
    }
}
