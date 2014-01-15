using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentInfoSearchCriteria:IDAL.IAgentInfoSearchCriteria
    {
        public ViewModel.AgentInfoSearchCriteria GetSearchCriteria(string keyWord,
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
            string scpc)
        {
            ViewModel.AgentInfoSearchCriteria searchCriteria = new ViewModel.AgentInfoSearchCriteria();

            searchCriteria.KeyWord = keyWord;
            searchCriteria.Ip = ip;
            searchCriteria.Session = session;
            searchCriteria.PostType = postType;

            searchCriteria.MinPrice = minPrice;
            searchCriteria.MaxPrice = maxPrice;

            searchCriteria.MinBedRoomCount = minBedRoomCount;
            searchCriteria.MaxBedRoomCount = maxBedRoomCount;

            searchCriteria.MinSize = minSize;
            searchCriteria.MaxSize = maxSize;

            searchCriteria.OrderBy = orderBy;
            searchCriteria.Order = order;

            searchCriteria.OrderById = orderByTypeId;


            searchCriteria.PageSize = pageSize;
            searchCriteria.PageIndex = pageIndex;

            searchCriteria.PriceId = priceTypeId;
            searchCriteria.BedRoomId = bedRoomCountTypeId;
            searchCriteria.SizeId = sizeTypeId;

            searchCriteria.IsInit = init;
            searchCriteria.CestCode = cestCode;
            searchCriteria.AgentName = agentName;

            searchCriteria.EstName = estName;

            searchCriteria.HouseType = houseType;

            searchCriteria.GscpId = gscpId;

            searchCriteria.Gscpc = gscpc;

            searchCriteria.ScpMkt = scpMkt;

            searchCriteria.Scpc = scpc;

            return searchCriteria;
        }
    }
}
