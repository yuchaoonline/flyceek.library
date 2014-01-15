using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Findproperty.Facade;
using FindProperty.Lib.Common;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentInfoSearchCriteria
    {
        private readonly IAgentInfoSearchCriteria dal = DataAccessFactoryCreator.Create().AgentInfoSearchCriteria();

        public ViewModel.AgentInfoSearchCriteria GetSearchCriteria(string keyWord,
            string ip,
            string session,
            string postType,
            string priceTypeId,
            string minPrice,
            string maxPrice,
            string bedRoomCountTypeId,
            string minBedRoomCount,
            string maxBedRoomCount,
            string sizeTypeId,
            string minSize,
            string maxSize,
            string orderByTypeId,
            string pageSize,
            string pageIndex,
            string cestCode,
            string agentName,
            string houseType,
            string gscpId,
            string scpMkt)
        {
            ViewModel.AgentInfoSearchCriteria searchCriteria = new ViewModel.AgentInfoSearchCriteria();

            string estName = string.Empty;
            string gscpc = string.Empty;
            string scpc = string.Empty;

            int? priceTypeIdInt = null;

            decimal? minPriceDecimal = null;
            decimal? maxPriceDecimal = null;

            int? bedRoomCountTypeIdInt = null;
            int? minBedRoomCountInt = null;
            int? maxBedRoomCountInt = null;
            int? orderByTypeIdInt = null;
            int? sizeTypeIdInt = null;
            int? minSizeInt = null;
            int? maxSizeInt = null;
            string orderBy = string.Empty;
            string order = string.Empty;
            int? pageSizeInt = null;
            int? pageIndexInt = null;

            int? houseTypeInt = null;

            bool init = false;
            if (postType != null)
            {
                postType = postType.ToUpper().Trim();
            }
            if (postType == null || (postType != "S" && postType != "R"))
            {
                postType = "S";
            }

            var stringToNullableDecimal = new StringToNullableDecimal();
            var stringToNummableInt = new StringToNullableInt();

            if (!stringToNummableInt.Convert(houseType, out houseTypeInt))
            {
                houseTypeInt = null;
            }

            if (!stringToNummableInt.Convert(pageSize, out pageSizeInt))
            {
                pageSizeInt = ConstValue.AgentInfoPostListPageSize;
            }
            if (!stringToNummableInt.Convert(pageIndex, out pageIndexInt))
            {
                pageIndexInt = 1;
            }

            SearchCriteriaValidate SearchCriteriaValidate = new Facade.SearchCriteriaValidate();

            if (SearchCriteriaValidate.MinMaxPrice(minPrice, maxPrice, out minPriceDecimal, out maxPriceDecimal, postType,
                new HousePriceCriteria().GetAgentShopHousePriceCriteria(postType, priceTypeId),
                out priceTypeIdInt)) init = true;

            if (SearchCriteriaValidate.MinMaxBedRoom(minBedRoomCount, maxBedRoomCount,
                out minBedRoomCountInt, out maxBedRoomCountInt,
                postType,
                new HouseBedRoomCriteria().GetBedRoomCriteria(bedRoomCountTypeId),
                out bedRoomCountTypeIdInt)) init = true;

            if (SearchCriteriaValidate.MinMaxSize(minSize, maxSize, out minSizeInt, out maxSizeInt,
                new HouseSizeCriteria().GetSizeCriteria(sizeTypeId),
                out sizeTypeIdInt)) init = true;

            SearchCriteriaValidate.Order(out orderBy, out order, out orderByTypeIdInt, new HouseOrderCriteria().GetOrderCriteria(postType, orderByTypeId));

            var gscpItem = SearchCriteriaValidate.GscpItem(gscpId);

            if (gscpItem != null)
            {
                init = true;
                scpMkt = (string.IsNullOrEmpty(scpMkt) ? gscpItem.gscp_mkt : scpMkt);
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

            if (!string.IsNullOrEmpty(keyWord))
            {
                init = true;
            }
            else
            {
                keyWord = string.Empty;
            }

            if (!string.IsNullOrEmpty(cestCode) &&
                new Regex("^[a-z]+$", RegexOptions.IgnoreCase).Match(cestCode).Success)
            {
                var est = new Cenest().GetCenest(cestCode);
                if (est != null)
                {
                    estName = est.c_estate;
                    init = true;
                }
            }

            searchCriteria = dal.GetSearchCriteria(keyWord,
                ip,
                session,
                postType,
                priceTypeIdInt,
                minPriceDecimal,
                maxPriceDecimal,
                bedRoomCountTypeIdInt,
                minBedRoomCountInt,
                maxBedRoomCountInt,
                sizeTypeIdInt,
                minSizeInt,
                maxSizeInt,
                orderByTypeIdInt,
                orderBy,
                order,
                pageSizeInt,
                pageIndexInt,
                cestCode,
                agentName,
                init,
                estName,
                houseTypeInt,
                gscpId,
                gscpc,
                scpMkt,
                scpc);

            return searchCriteria;
        }
    }
}
