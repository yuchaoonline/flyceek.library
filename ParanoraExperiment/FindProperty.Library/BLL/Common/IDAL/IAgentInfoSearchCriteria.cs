using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IAgentInfoSearchCriteria
    {
        ViewModel.AgentInfoSearchCriteria GetSearchCriteria(string keyWord,
            string ip,
            string session,
            string postType,
            int? priceTypeId,
            decimal? minPrice,
            decimal? maxPrice,
            int? bedRoomCountTypeId,
            int? minBedRoomCount,
            int? maxBedRoomCount,
            int? sizeTypeId,
            int? minSize,
            int? maxSize,
            int? orderByTypeId,
            string orderBy,
            string order,
            int? pageSize,
            int? pageIndex,
            string cestCode,
            string agentName,
            bool init,
            string estName,
            int? houseType,
            string gscpId,
            string gscpc,
            string scpMkt,
            string scpc
            );
    }
}
