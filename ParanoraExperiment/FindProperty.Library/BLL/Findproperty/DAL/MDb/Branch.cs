using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Findproperty.IDAL;
using FindProperty.Lib.Factory;
using MongoDB.Driver.Builders;

namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Branch :IBranch
    {
        public List<ViewModel.Branch> SelectBranch(string scpid, double lat, double lng, int pageSize, int pageindex, int sort)
        {

            int rows = 15;
            int skiprows = pageindex * rows;
            List<ViewModel.Branch> list = new List<ViewModel.Branch>();

            var where =Query.Near("locs", lng, lat);

            if (scpid.ToLower()!=("0"))
            {
                where = Query.And(where, Query.EQ("scp_id", scpid.ToUpper()));  
            }

            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Branch>())
            {
                var result = db["branch"].Find(where);
                SortByBuilder orderBy = null;
                switch (sort)
                {
                    case 1:
                        orderBy = SortBy.Descending("orgcount");
                        break;
                    default:
                        break;
                }
                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderBy);
                }

                list = result.SetSkip(skiprows).SetLimit(rows).ToList<ViewModel.Branch>();

                foreach (var item in list)
                {
                    item.dis = getDistance(lat, lng, item.lat, item.lng);
                }
            }
            return list;
        }

        private readonly double EARTH_RADIUS = 6378.137;
        private double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public int getDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            s *= 1000;
            int dis = (int)Math.Round(s);

            return dis;
        }


        public double[] getAround(double lat, double lon, int raidus)
        {

            Double latitude = lat;

            Double longitude = lon;
            Double degree = (24901 * 1609) / 360.0;

            double raidusMile = raidus;



            Double dpmLat = 1 / degree;

            Double radiusLat = dpmLat * raidusMile;

            Double minLat = latitude - radiusLat;

            Double maxLat = latitude + radiusLat;



            Double mpdLng = degree * Math.Cos(latitude * (Math.PI / 180));

            Double dpmLng = 1 / mpdLng;

            Double radiusLng = dpmLng * raidusMile;

            Double minLng = longitude - radiusLng;

            Double maxLng = longitude + radiusLng;

            return new double[] { minLat, minLng, maxLat, maxLng };

        }
    }
}
