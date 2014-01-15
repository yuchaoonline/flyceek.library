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
    public class EstScore:IEstScore
    {
        public List<ViewModel.EstScore> SelectEstScore(int raidus, double lat, double lng,int pageindex,string posttype,int sort)
        {
            int rows = 15;
            int skiprows = pageindex * rows;
            List<ViewModel.EstScore> list = new List<ViewModel.EstScore>();

            var where = Query.Near("locs", lng, lat, raidus); 
            double[] range = getAround(lat, lng, raidus);
            where = Query.And(where, Query.And(Query.GTE("lat", range[0]), Query.LTE("lat", range[2])));
            where = Query.And(where, Query.And(Query.GTE("lng", range[1]), Query.LTE("lng", range[3])));
           
            if (posttype.ToLower() == "s")
            {
                where = Query.And(where, Query.And(Query.GT("scounter", 0)));
            }
            else if (posttype.ToLower() == "r")
            {
                where = Query.And(where, Query.And(Query.GT("rcounter", 0)));
               
            }
            
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.EstScore>())
            {
                var result = db["estscore"].Find(where);
                SortByBuilder orderBy = null;
                switch (sort)
                { 
                   
                    case 1:
                        orderBy = SortBy.Descending("score");
                        break;
                    case 2:
                        if (posttype.ToLower()=="r")
                            orderBy = SortBy.Ascending("avgrentprice");
                        else
                            orderBy = SortBy.Ascending("avgprice");
                        break;
                    default:
                        break;
                }
                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderBy);
                }
                //list=result.Skip(skiprows).Take(rows).ToList<ViewModel.EstScore>();
                list = result.SetSkip(skiprows).SetLimit(rows).ToList<ViewModel.EstScore>();

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


        public ViewModel.EstScore GetPosition(string EstCode, string BigEstCode)
        {
            var returnObject = new ViewModel.EstScore();
            var result = new List<ViewModel.EstScore>();
            var where = Query.EQ("code", EstCode);
            if (!string.IsNullOrEmpty(BigEstCode))
            {
                where = Query.Or(where, Query.EQ("code", BigEstCode));
            }
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.EstScore>())
            {
                result = db["estscore"].Find(where).Take(1).ToList();
            }
            if (result.Count > 0)
            {
                returnObject = result[0];
            }
            return returnObject;
        }
    }
}
