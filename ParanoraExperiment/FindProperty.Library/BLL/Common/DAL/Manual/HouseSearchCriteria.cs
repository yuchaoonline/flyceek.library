using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HouseSearchCriteria:IDAL.IHouseSearchCriteria
    {
        public ViewModel.HouseSearchCriteria GetSearchCriteria(string keyWord,
            string ip,
            string session,
            string scpMkt,
            string gscpId,
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
            int? subWay,
            string subStation,
            string tag,
            int? pageSize,
            int? pageIndex,
            bool init,
            int? listStyleId,
            int? houseType,
            string scpc,
            string gscpc,
            string ddlHouseType,
            string ddlHousYear,
            string ddlOrientations,
            string ddlRenovation
            )
        {
            ViewModel.HouseSearchCriteria searchCriteria = new ViewModel.HouseSearchCriteria();

            searchCriteria.KeyWord = keyWord;
            searchCriteria.Ip = ip;
            searchCriteria.Session = session;
            searchCriteria.ScpMkt = scpMkt;
            searchCriteria.GscpId = gscpId;
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

            searchCriteria.SubWay = subWay;
            searchCriteria.SubStation = subStation;
            searchCriteria.Tag = tag;

            searchCriteria.PageSize = pageSize;
            searchCriteria.PageIndex = pageIndex;

            searchCriteria.PriceId = priceTypeId;
            searchCriteria.BedRoomId = bedRoomCountTypeId;
            searchCriteria.SizeId = sizeTypeId;

            searchCriteria.IsInit = init;

            searchCriteria.ListStyleId = listStyleId;

            searchCriteria.HouseType = houseType;

            searchCriteria.Gscpc = gscpc;
            searchCriteria.Scpc = scpc;

            searchCriteria.ddlHouseType = ddlHouseType;
            searchCriteria.ddlHousYear = ddlHousYear;
            searchCriteria.ddlOrientations = ddlOrientations;
            searchCriteria.ddlRenovation = ddlRenovation;


            return searchCriteria;
        }
    }
}
