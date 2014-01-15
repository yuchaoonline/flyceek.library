using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IHouseSearchCriteria
    {
        HouseSearchCriteria GetSearchCriteria(string keyWord,
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
            );
    }
}
