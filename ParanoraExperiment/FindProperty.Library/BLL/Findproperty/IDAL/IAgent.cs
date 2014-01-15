using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.Aop.Componet;
using FindProperty.Lib.Cache.Component.ResultExaminer;

namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IAgent
    {
        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer), CacheSecond = 600)]
        List<ViewModel.Agent> SelectAgent(AgentListSearchCriteria searchCriteria);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer), CacheSecond = 600)]
        int SelectAgentCount(AgentListSearchCriteria searchCriteria);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer), CacheSecond = 600)]
        ViewModel.Agent GetAgentByAgentName(string agentName);

        List<ViewModel.Agent> SelectAgentByManagerNo(string managerNo, int top);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer), CacheSecond = 600)]
        List<ViewModel.Agent> SelectAgent(string scpMkt, string gscpId, int top, string orderBy, string order);

        //add 2013.09.10
        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer), CacheSecond = 600)]
        ViewModel.Agent GetAgentByAgentNo(string AgentNo);
    }
}
