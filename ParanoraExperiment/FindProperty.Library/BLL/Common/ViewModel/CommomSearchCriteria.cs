using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class CommomSearchCriteria
    {
        public string KeyWord { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public int? MinBedRoomCount { get; set; }
        public int? MaxBedRoomCount { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public int? OrderById { get; set; }
        public string OrderBy { get; set; }
        public string Order { get; set; }

        public string Ip { get; set; }
        public string Session { get; set; }
        public string PostType { get; set; }

        public int? PriceId { get; set; }
        public int? SizeId { get; set; }
        public int? BedRoomId { get; set; }

        public bool IsInit { get; set; }

        public int? HouseType { get; set; }

        public string GscpId { get; set; }

        public string Gscpc { get; set; }

        public string ScpMkt { get; set; }
        public string Scpc { get; set; }

        public string ddlHouseType { get; set; }
        public string ddlHousYear { get; set; }
        public string ddlOrientations { get; set; }
        public string ddlRenovation { get; set; }

        public CommomSearchCriteria()
        {
            IsInit = false;
        }

        public override string ToString()
        {
            string str = string.Empty;
            str += (string.IsNullOrEmpty(KeyWord) ? "" : KeyWord) + "|";

            str += (MinPrice.HasValue ? MinPrice.Value.ToString() : "") + "|";
            str += (MaxPrice.HasValue ? MaxPrice.Value.ToString() : "") + "|";
            str += (MinSize.HasValue ? MinSize.Value.ToString() : "") + "|";
            str += (MaxSize.HasValue ? MaxSize.Value.ToString() : "") + "|";
            str += (MinBedRoomCount.HasValue ? MinBedRoomCount.Value.ToString() : "") + "|";
            str += (MaxBedRoomCount.HasValue ? MaxBedRoomCount.Value.ToString() : "") + "|";
            str += (PageIndex.HasValue ? PageIndex.Value.ToString() : "") + "|";
            str += (PageSize.HasValue ? PageSize.Value.ToString() : "") + "|";

            str += (OrderById.HasValue ? OrderById.Value.ToString() : "") + "|";
            str += (string.IsNullOrEmpty(PostType) ? "" : PostType) + "|";

            str += (string.IsNullOrEmpty(ScpMkt) ? "" : ScpMkt) + "|";
            str += (string.IsNullOrEmpty(GscpId) ? "" : GscpId) + "|";
            str += (HouseType.HasValue ? HouseType.Value.ToString() : "") + "|";

            return str;
        }
    }
}
