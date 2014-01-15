using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentListSearchCriteria:IDAL.IAgentListSearchCriteria
    {
        public ViewModel.AgentListSearchCriteria GetSearchCriteria(string ip, 
            string session, string scpMkt, string gscpId, 
            int? orderById, string orderBy,
            string order, int? pageSize, int? pageIndex, string scpc,
            string gscpc, bool init, string k)
        {
            ViewModel.AgentListSearchCriteria searchCriteria = new ViewModel.AgentListSearchCriteria();
            searchCriteria.ScpMkt = scpMkt;
            searchCriteria.GscpId = gscpId;
            searchCriteria.PageIndex = pageIndex;
            searchCriteria.PageSize = pageSize;
            searchCriteria.OrderById = orderById;
            searchCriteria.Ip = ip;
            searchCriteria.Session = session;

            searchCriteria.OrderBy = orderBy;
            searchCriteria.Order = order;

            searchCriteria.IsInit = init;
            searchCriteria.Scpc = scpc;
            searchCriteria.Gscpc = gscpc;
            searchCriteria.KeyWord = k;

            return searchCriteria;
        }
    }
}
