using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SearchCriteriaValidate
    {
        public bool RecommendTag(string tag, string scpMkt, string gscpId)
        {
            bool init = false;
            var item = new SearchRecommendTag().SelectSearchRecommendTag(tag, scpMkt, gscpId).Where(x => x.Tag == tag).Select(x => x.Tag).FirstOrDefault();

            if (item != null)
            {
                init = true;
            }

            return init;
        }

        public bool MinMaxPrice(string minPrice, 
            string maxPrice,
            out decimal? minPriceDecimal, 
            out decimal? maxPriceDecimal,
            string postType,
            ViewModel.PriceCriteria price,
            out int? priceTypeIdInt)
        {
            bool init = false;

            priceTypeIdInt = null;
            minPriceDecimal = null;
            maxPriceDecimal = null;

            new StringToNullableDecimal().Convert(minPrice, out minPriceDecimal);
            new StringToNullableDecimal().Convert(maxPrice, out maxPriceDecimal);

            if (minPriceDecimal.HasValue && maxPriceDecimal.HasValue)
            {
                var item = new ViewModel.PriceCriteria();
                switch (postType)
                {
                    case "S":
                        minPriceDecimal *= 10000;
                        maxPriceDecimal *= 10000;
                        break;
                }

                if (minPriceDecimal > (decimal)5500000000 || maxPriceDecimal > (decimal)5500000000 || minPriceDecimal > maxPriceDecimal)
                {
                    minPriceDecimal = null;
                    maxPriceDecimal = null;
                }
            }
            else
            {
                if (!minPriceDecimal.HasValue && !maxPriceDecimal.HasValue)
                {
                    if (price != null)
                    {
                        minPriceDecimal = price.Min;
                        maxPriceDecimal = price.Max;
                        priceTypeIdInt = int.Parse(price.ID);
                    }
                }
                else
                {
                    minPriceDecimal = null;
                    maxPriceDecimal = null;
                }
            }            

            if (minPriceDecimal.HasValue && maxPriceDecimal.HasValue)
            {
                init = true;
            }
            return init;
        }


        public bool MinMaxBedRoom(string minBed,
            string maxBed,
            out int? minBedRoomCountInt,
            out int? maxBedRoomCountInt,
            string postType,
            ViewModel.BedRoomCriteria bedRoom,
            out int? bedRoomCountTypeIdInt)
        {
            bool init = false;

            minBedRoomCountInt = null;
            maxBedRoomCountInt = null;
            bedRoomCountTypeIdInt = null;

            new StringToNullableInt().Convert(minBed, out minBedRoomCountInt);
            new StringToNullableInt().Convert(maxBed, out maxBedRoomCountInt);

            if (!minBedRoomCountInt.HasValue && !maxBedRoomCountInt.HasValue)
            {
                if (bedRoom != null)
                {
                    minBedRoomCountInt = (int)bedRoom.Min;
                    maxBedRoomCountInt = (int)bedRoom.Max;
                    bedRoomCountTypeIdInt = int.Parse(bedRoom.ID);
                }
            }
            else
            {
                if (minBedRoomCountInt.HasValue && maxBedRoomCountInt.HasValue)
                {
                    if (minBedRoomCountInt > 99 || maxBedRoomCountInt > 99 || minBedRoomCountInt > maxBedRoomCountInt)
                    {
                        minBedRoomCountInt = null;
                        maxBedRoomCountInt = null;
                    }
                }
                else
                {
                    minBedRoomCountInt = null;
                    maxBedRoomCountInt = null;
                }
            }
            if (minBedRoomCountInt.HasValue && maxBedRoomCountInt.HasValue)
            {
                init = true;
            }
            return init;
        }

        public bool MinMaxSize(string minSize,
            string maxSize,
            out int? minSizeInt,
            out int? maxSizeInt,
            ViewModel.SizeCriteria size,
            out int? sizeTypeIdInt)
        {
            bool init = false;

            minSizeInt = null;
            maxSizeInt = null;
            sizeTypeIdInt = null;

            new StringToNullableInt().Convert(minSize, out minSizeInt);
            new StringToNullableInt().Convert(maxSize, out maxSizeInt);
            if (!minSizeInt.HasValue && !maxSizeInt.HasValue)
            {
                if (size != null)
                {
                    minSizeInt = (int)size.Min;
                    maxSizeInt = (int)size.Max;
                    sizeTypeIdInt = int.Parse(size.ID);
                }
            }
            else
            {
                if (minSizeInt.HasValue && maxSizeInt.HasValue)
                {
                    if (minSizeInt > 99999 || maxSizeInt > 99999 || minSizeInt > maxSizeInt)
                    {
                        minSizeInt = null;
                        maxSizeInt = null;
                    }
                }
                else
                {
                    minSizeInt = null;
                    maxSizeInt = null;
                }
            }
            if (minSizeInt.HasValue && maxSizeInt.HasValue)
            {
                init = true;
            }


            return init;
        }

        public bool SubStation(string subStation,int subWay,string scpMkt,string gscpId, out string subStationReturn)
        {
            subStationReturn = null;
            bool init = false;

            if (new Facade.SubStationCriteria().GetSubStationCriteriaEx(subWay, gscpId, scpMkt).Where(x => x.Value == subStation).Count() > 0)
            {
                subStationReturn = subStation;
                init = true;
            }
            return init;
        }

        public bool SubWay(string subWay, string scpMkt,string gscpId,out int? subWayInt)
        {
            bool init = false;
            subWayInt = null;
            if (new StringToNullableInt().Convert(subWay, out subWayInt))
            {
                var item = new SubWayCriteria().GetSubwayCriteriaEx(scpMkt,gscpId).Where(x => x.ID == subWay.Trim()).FirstOrDefault();
                if (item != null)
                {
                    subWayInt = int.Parse(item.ID);
                    init = true;
                }
            }

            return init;
        }

        public bool Order(out string orderBy, out string order, out int? orderById, ViewModel.OrderCriteria orderCriteria)
        {
            orderById = null;
            order = string.Empty;
            orderBy = string.Empty;

            bool init = false;
            if (orderCriteria != null)
            {
                orderBy = orderCriteria.OrderBy.ToString();
                order = orderCriteria.Order.ToString();
                orderById = int.Parse(orderCriteria.ID);
            }
            return init;
        }

        public string ScpMkt(string scpMkt)
        {
            if (string.IsNullOrEmpty(scpMkt) ||
                new Findproperty.Facade.Region().GetAllRegion().Where(x => x.scp_mkt.ToUpper() == scpMkt.Trim().ToUpper()).Count() < 1)
            {
                scpMkt = string.Empty;
            }

            return scpMkt;
        }

        public Findproperty.ViewModel.Region ScpMktItem(string scpMkt)
        {
            Findproperty.ViewModel.Region item = null;

            if (!string.IsNullOrEmpty(scpMkt))
            {
                item = new Findproperty.Facade.Region().GetAllRegion().Where(x => x.scp_mkt.ToUpper() == scpMkt.Trim().ToUpper()).FirstOrDefault();
            }

            return item;
        }

        public Findproperty.ViewModel.Gscp GscpItem(string gscpId)
        {
            Findproperty.ViewModel.Gscp item = null;

            if (!string.IsNullOrEmpty(gscpId))
            {
                item = new Findproperty.Facade.Gscp().GetAllGscp().Where(x => x.gscp_id.ToUpper().Trim() == gscpId.Trim().ToUpper()).FirstOrDefault();
            }
            return item;
        }

        public string Gscp(string gscpId)
        {
            if (string.IsNullOrEmpty(gscpId) ||
                new Findproperty.Facade.Gscp().GetAllGscp().Where(x => x.gscp_id.ToUpper().Trim() == gscpId.Trim().ToUpper()).Count() < 1)
            {
                gscpId = string.Empty;
            }
            return gscpId;
        }

        public bool HouseListStyle(string listStyleId, out int? listStyleIdInt)
        {
            bool init = false;
            listStyleIdInt = null;

            if (!string.IsNullOrEmpty(listStyleId) &&new StringToNullableInt().Convert(listStyleId, out listStyleIdInt))
            {
                var item = new HouseListStyle().GetHouseListStyle().Where(x => x.ID == listStyleId).FirstOrDefault();
                if (item != null)
                {
                    listStyleIdInt = int.Parse(item.ID);
                }
            }

            return init;
        }
    }
}
