using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentPostSummary
    {
        private readonly IAgentPostSummary dal = DataAccessFactoryCreator.Create().AgentPostSummary();

        public ViewModel.AgentPostSummary GetAgentPostSummary(string agentNo)
        {
            return dal.GetAgentPostSummary(agentNo);
        }
    }
}
