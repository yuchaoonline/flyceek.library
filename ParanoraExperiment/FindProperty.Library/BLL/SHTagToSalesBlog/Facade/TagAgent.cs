using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.Facade
{
    public class TagAgent
    {
        private IDAL.ITagAgent dal = DALFactory.DataAccessFactoryCreator.Create().TagAgent();

        public List<ViewModel.TagAgent> SelectTagAgent(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            return dal.SelectTagAgent(agentNo, agentName, agentMobile, top, orderBy, order);
        }

        public List<ViewModel.TagAgent> SelectTagAgent(string agentNo, string agentName, string agentMobile, int top)
        {
            return dal.SelectTagAgent(agentNo, agentName, agentMobile, top, "SNum", "desc");
        }

        public List<ViewModel.TagAgent> SelectTagAgentLike(string agentNo, string agentName, string agentMobile, int top)
        {
            return dal.SelectTagAgentLike(agentNo, agentName, agentMobile, top, "SNum", "desc");
        }

        public List<ViewModel.TagAgent> SelectTagAgentLike(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            return dal.SelectTagAgentLike(agentNo, agentName, agentMobile, top, orderBy, order);
        }
    }
}
