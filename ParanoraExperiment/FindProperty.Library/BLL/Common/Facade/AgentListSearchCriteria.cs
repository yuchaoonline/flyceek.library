using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.Common;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentListSearchCriteria
    {
        private readonly IAgentListSearchCriteria dal = DataAccessFactoryCreator.Create().AgentListSearchCriteria();

        public ViewModel.AgentListSearchCriteria GetSearchCriteria(
            string ip,
            string session,
            string scpMkt,
            string gscpId,           
            string orderByTypeId,
            string pageSize,
            string pageIndex,
            string k)
        {
            ViewModel.AgentListSearchCriteria searchCriteria = new ViewModel.AgentListSearchCriteria();
            var stringToNullableDecimal = new StringToNullableDecimal();
            var stringToNummableInt = new StringToNullableInt();

            int? orderByTypeIdInt = null;
            int? pageSizeInt = null;
            int? pageIndexInt = null;

            bool init = false;

            string orderBy = string.Empty;
            string order = string.Empty;

            if (!stringToNummableInt.Convert(pageSize, out pageSizeInt))
            {
                pageSizeInt = ConstValue.AgentListPageSize;
            }
            if (!stringToNummableInt.Convert(pageIndex, out pageIndexInt))
            {
                pageIndexInt = 1;
            }

            string scpc = string.Empty;
            string gscpc = string.Empty;

            ViewModel.OrderCriteria item = null;

            if (!string.IsNullOrEmpty(orderByTypeId))
            {
                item = new AgentListOrderCriteria().GetOrderCriteria().Where(x => x.ID == orderByTypeId.Trim()).FirstOrDefault();
            }

            SearchCriteriaValidate SearchCriteriaValidate = new Facade.SearchCriteriaValidate();
            SearchCriteriaValidate.Order(out orderBy, out order, out orderByTypeIdInt,item);

            var gscpItem = SearchCriteriaValidate.GscpItem(gscpId);

            if (gscpItem != null)
            {
                init = true;
                scpMkt = (string.IsNullOrEmpty(scpMkt)?gscpItem.gscp_mkt:scpMkt);
                gscpId = gscpItem.gscp_id;
                gscpc = gscpItem.gscp_c;
                var scpItem = SearchCriteriaValidate.ScpMktItem(scpMkt);
                if (scpItem != null)
                {
                    scpc = scpItem.c_distname;
                }
            }
            else
            {
                var scpItem = SearchCriteriaValidate.ScpMktItem(scpMkt);
                if (scpItem != null)
                {
                    scpMkt = scpItem.scp_mkt;
                    scpc = scpItem.c_distname;
                    init = true;
                }
                else
                {
                    scpMkt = string.Empty;
                }
                gscpId = string.Empty;
            }

            if (!string.IsNullOrEmpty(k))
            {
                init = true;
            }

            return dal.GetSearchCriteria(ip,
                session,
                scpMkt,
                gscpId,
                orderByTypeIdInt,
                orderBy,
                order,
                pageSizeInt,
                pageIndexInt,
                scpc,
                gscpc,
                init,
                k);
        }
    }
}
