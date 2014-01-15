using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentListSearchCriteria
    {
        ViewModel.AgentListSearchCriteria GetSearchCriteria(string ip,
            string session, 
            string scpMkt,
            string gscpId,
            int? orderById,
            string orderBy,
            string order,
            int? pageSize,
            int? pageIndex,
            string scpc,
            string gscpc,
            bool init,
            string k
            );
    }
}
