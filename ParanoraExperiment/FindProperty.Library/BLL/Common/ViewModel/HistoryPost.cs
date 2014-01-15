using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    public class HistoryPost
    {
        public string RefNo { get; set; }
        public string CestName { get; set; }
        public decimal Size { get; set; }
        public string BedRoom { get; set; }
        public string SittingRoom { get; set; }
        public string PostType { get; set; }
        public decimal Price { get; set; }

        public string PicUrl { get; set; }
        public string Des { get; set; }

        public override string ToString()
        {
            return RefNo + "|" + CestName + "|" + Size + "|" + BedRoom + "|" + SittingRoom + "|" + PostType + "|" + Price + "|" + PicUrl + "|" + Des;
        }

        public bool FromString(string value)
        {
            bool result = false;

            string[] strAry = value.Split('|');
            if (strAry.Length > 0 && strAry.Length>7)
            {
                result = FromStringArray(strAry);
            }
            return result;
        }
        public bool FromStringArray(string[] strAry)
        {
            bool result = false;
            if (strAry.Length > 0 && strAry.Length > 7)
            {
                RefNo = strAry[0];
                CestName = strAry[1];
                Size = decimal.Parse(strAry[2]);
                BedRoom = strAry[3];
                SittingRoom = strAry[4];
                PostType = strAry[5];
                Price = decimal.Parse(strAry[6]);
                PicUrl = strAry[7];
                Des = strAry[8];
            }
            return result;
        }
        public HistoryPost()
        {
        }
        public HistoryPost(string value)
        {
            FromString(value);
        }
        public HistoryPost(string[] strAry)
        {
            FromStringArray(strAry);
        }
    }
}
