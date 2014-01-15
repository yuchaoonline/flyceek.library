using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.Factory;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Gscp:IDAL.IGscp
    {
        public List<ViewModel.Gscp> GetAllGscp()
        {
            List<ViewModel.Gscp> list = new List<ViewModel.Gscp>();

            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.Gscp>())
            {
                var result = db["Gscp"].FindAll();
                list = result.ToList<ViewModel.Gscp>();
            }
            return list;
        }

        public List<ViewModel.Gscp> GetGscpByScpMkt(string scpMkt)
        {
            return PolicyInjectionFactory.Create().Wrap<IDAL.IGscp>(this).GetAllGscp().Where(x => x.gscp_mkt == scpMkt).OrderBy(x=>x.gscp_id).ToList();
        }
    }
}
