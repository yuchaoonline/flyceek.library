using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentListOrderCriteria
    {
        private readonly IAgentListOrderCriteria dal = DataAccessFactoryCreator.Create().AgentListOrderCriteria();

        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            return dal.GetOrderCriteria();
        }
    }
}
