using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.Common;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseSearchCriteria
    {
        private readonly IHouseSearchCriteria dal = DataAccessFactoryCreator.Create().HouseSearchCriteria();

        public ViewModel.HouseSearchCriteria GetSearchCriteria(string keyWord,
            string ip,
            string session,
            string scpMkt,
            string gscpId,
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
            string subWay,
            string subStation,
            string tag,
            string pageSize,
            string pageIndex,
            string listStyleId,
            string houseType,
            string ddlHouseType,
            string ddlHousYear,
            string ddlOrientations,
            string ddlRenovation
            )
        {
            ViewModel.HouseSearchCriteria searchCriteria = new ViewModel.HouseSearchCriteria();

            int? priceTypeIdInt = null;

            decimal? minPriceDecimal = null;
            decimal? maxPriceDecimal = null;

            int? bedRoomCountTypeIdInt= null;
            int? minBedRoomCountInt= null;
            int? maxBedRoomCountInt = null;
            int? orderByTypeIdInt=null;
            int? sizeTypeIdInt = null;
            int? minSizeInt=null;
            int? maxSizeInt = null;
            string orderBy = string.Empty;
            string order = string.Empty;
            int? pageSizeInt=null;
            int? pageIndexInt = null;
            int? subWayInt = null;
            int? listStyleIdInt = null;

            int? houseTypeInt = null;

            if (!new StringToNullableInt().Convert(houseType, out houseTypeInt))
            {
                houseTypeInt = null;
            }

            bool init = false;
            if (postType != null)
            {
                postType = postType.ToUpper().Trim();
            }
            if (postType == null || (postType != "S" && postType != "R"))
            {
                postType = "S";
            }
            
            if (!new StringToNullableInt().Convert(pageIndex, out pageIndexInt))
            {
                pageIndexInt = 1;
            }
            
            SearchCriteriaValidate SearchCriteriaValidate=new Facade.SearchCriteriaValidate();

            string priceDisplay=string.Empty;
            string sizeDisplay=string.Empty;
            string bedRoolDisplay = string.Empty;

            if(SearchCriteriaValidate.MinMaxPrice(minPrice, 
                maxPrice, 
                out minPriceDecimal,
                out maxPriceDecimal, 
                postType,
                new HousePriceCriteria().GetPriceCriteria(postType, priceTypeId),
                out priceTypeIdInt))
            {
                init=true;
            }

            if (SearchCriteriaValidate.MinMaxBedRoom(minBedRoomCount, maxBedRoomCount,
                out minBedRoomCountInt, out maxBedRoomCountInt,
                postType,
                new HouseBedRoomCriteria().GetBedRoomCriteria(bedRoomCountTypeId),
                out bedRoomCountTypeIdInt))
                init = true;

            if (SearchCriteriaValidate.MinMaxSize(minSize, maxSize, out minSizeInt, out maxSizeInt,
                new HouseSizeCriteria().GetSizeCriteria(sizeTypeId),
                out sizeTypeIdInt))
                init = true;

            SearchCriteriaValidate.Order(out orderBy, out order, out orderByTypeIdInt,new HouseOrderCriteria().GetOrderCriteria(postType, orderByTypeId));

            string scpc = string.Empty;
            string gscpc = string.Empty;

            var gscpItem = SearchCriteriaValidate.GscpItem(gscpId);

            if (gscpItem != null)
            {
                init = true;
                scpMkt = gscpItem.gscp_mkt;
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

            //if (!string.IsNullOrEmpty(scpMkt) || !string.IsNullOrEmpty(gscpId))
            //{
            //    if (SearchCriteriaValidate.RecommendTag(tag, scpMkt, gscpId))
            //    {
            //        init = true;
            //    }
            //    else
            //    {
            //        tag = string.Empty;
            //    }
            //}

            if (!string.IsNullOrEmpty(tag))
            {
                init = true;
            }
            else
            {
                tag = string.Empty;
            }

            if (SearchCriteriaValidate.SubWay(subWay, scpMkt, gscpId, out subWayInt))
            {
                init = true;
                string subStationReValue = string.Empty;
                if (SearchCriteriaValidate.SubStation(subStation, subWayInt.Value, scpMkt, gscpId, out subStationReValue))
                {
                    init = true;
                }

                subStation = subStationReValue;
            }
            

            SearchCriteriaValidate.HouseListStyle(listStyleId, out listStyleIdInt);

            if (!new StringToNullableInt().Convert(pageSize, out pageSizeInt))
            {
                pageSizeInt = ConstValue.PostListPageSize2;
            }

            if (!string.IsNullOrEmpty(keyWord))
            {
                init = true;
            }
            else
            {
                keyWord = string.Empty;
            }

            if (!string.IsNullOrEmpty(tag))
            {
                init = true;
            }
            else
            {
                tag = string.Empty;
            }

            if (!string.IsNullOrEmpty(ddlHouseType))
                init = true;
            else
                ddlHouseType = string.Empty;

            if (!string.IsNullOrEmpty(ddlHousYear))
                init = true;
            else
                ddlHousYear = string.Empty;

            if (!string.IsNullOrEmpty(ddlOrientations))
                init = true;
            else
                ddlOrientations = string.Empty;

            if (!string.IsNullOrEmpty(ddlRenovation))
                init = true;
            else
                ddlRenovation = string.Empty;


            searchCriteria = dal.GetSearchCriteria(keyWord,
                ip,
                session,
                scpMkt,
                gscpId,
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
                subWayInt,
                subStation,
                tag,
                pageSizeInt,
                pageIndexInt,
                init,
                listStyleIdInt,
                houseTypeInt,
                scpc,
                gscpc,
                ddlHouseType,
                ddlHousYear,
                ddlOrientations,
                ddlRenovation
                );

            return searchCriteria;
        }
    }
}
