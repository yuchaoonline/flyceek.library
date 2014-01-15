using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.MDb
{
    public class TagArea:IDAL.ITagArea
    {
        public List<ViewModel.TagArea> SelectTagArea(string distname)
        {
            List<ViewModel.TagArea> list = new List<ViewModel.TagArea>();
            var where = Query.EQ("c_distname", distname);
            where = Query.Or(where, Query.EQ("RegionPY", distname.ToLower()));
            where = Query.Or(where, Query.EQ("RegionPYSX", distname.ToUpper()));
            where = Query.Or(where, Query.EQ("CNTradi", distname));
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagArea>())
            {
                var result = db["TagArea"].Find(where);
                list = result.ToList();
            }
            return list;
        }


        public List<ViewModel.TagArea> SelectTagAreaByDistname(string distname)
        {
            List<ViewModel.TagArea> list = new List<ViewModel.TagArea>();
            var where = Query.EQ("c_distname", distname);

            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagArea>())
            {
                var result = db["TagArea"].Find(where);
                list = result.ToList();
            }
            return list;
        }
    }
}
