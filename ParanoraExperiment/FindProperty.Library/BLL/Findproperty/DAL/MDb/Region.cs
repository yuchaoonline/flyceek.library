using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Region:IDAL.IRegion
    {
        public List<ViewModel.Region> GetAllRegion()
        {
            List<ViewModel.Region> list = new List<ViewModel.Region>();
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.Region>())
            {
                list = db["Region"].FindAll().SetSortOrder(SortBy.Ascending("scp_mkt")).ToList<ViewModel.Region>();
            }

            return list;
        }
    }
}
