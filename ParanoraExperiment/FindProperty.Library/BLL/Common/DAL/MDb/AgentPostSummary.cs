using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FD = FindProperty.Lib.BLL.Findproperty;

namespace FindProperty.Lib.BLL.Common.DAL.MDb
{
    public class AgentPostSummary:IDAL.IAgentPostSummary
    {
        public ViewModel.AgentPostSummary GetAgentPostSummary(string agentNo)
        {
            List<FD.ViewModel.Post> agentPosts = PolicyInjectionFactory.Create().Create<FD.DAL.Db.Post, FD.IDAL.IPost>().SelectPostByAgentNo(agentNo);

            return new Facade.CriteriaSummary().Summary(agentPosts, new ViewModel.AgentPostSummary()) as ViewModel.AgentPostSummary;
        }
    }
}
