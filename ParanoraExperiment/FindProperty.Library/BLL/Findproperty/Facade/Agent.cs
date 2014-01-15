using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Agent
    {
        private IDAL.IAgent dal = DALFactory.DataAccessFactoryCreator.Create().Agent();

        public List<ViewModel.Agent> SelectAgent(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            return dal.SelectAgent(searchCriteria);
        }

        public int SelectAgentCount(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            return dal.SelectAgentCount(searchCriteria);
        }

        public ViewModel.Agent GetAgentByAgentName(string agentName)
        {
            return dal.GetAgentByAgentName(agentName);
        }

        public List<ViewModel.Agent> SelectAgentByManagerNo(string managerNo, int top)
        {
            return dal.SelectAgentByManagerNo(managerNo, top);
        }

        public List<ViewModel.Agent> SelectAgent(string scpMkt, string gscpId, int top, string orderBy, string order)
        {
            return dal.SelectAgent(scpMkt, gscpId, top, orderBy, order);
        }

        public List<ViewModel.Agent> SelectAgent(string scpMkt, string gscpId, int top)
        {
            return dal.SelectAgent(scpMkt, gscpId, top, "agentscore", "desc");
        }

        public ViewModel.Agent GetAgentByAgentNo(string AgentNo)
        {
            return dal.GetAgentByAgentNo(AgentNo);
        }
    }
}
