using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common
{
    public class MapCommon
    {
        private const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        /// <summary>
        /// 中国正常坐标系GCJ02协议的坐标，转到 百度地图对应的 BD09 协议坐标
        /// </summary>
        /// <param name="lat">维度</param>
        /// <param name="lng">经度</param>
        public static void Convert_GCJ02_To_BD09(ref double lat, ref double lng)
        {
            double x = lng, y = lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * x_pi);
            lng = z * Math.Cos(theta) + 0.0065;
            lat = z * Math.Sin(theta) + 0.006;
        }
        /// <summary>
        /// 百度地图对应的 BD09 协议坐标，转到 中国正常坐标系GCJ02协议的坐标
        /// </summary>
        /// <param name="lat">维度</param>
        /// <param name="lng">经度</param>
        public static void Convert_BD09_To_GCJ02(ref double lat, ref double lng)
        {
            double x = lng - 0.0065, y = lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            lng = z * Math.Cos(theta);
            lat = z * Math.Sin(theta);
        } 
    }
}
