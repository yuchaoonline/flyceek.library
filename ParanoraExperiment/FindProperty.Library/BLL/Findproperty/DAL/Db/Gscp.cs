using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Gscp:IDAL.IGscp
    {
        public List<ViewModel.Gscp> GetAllGscp()
        {
            return GetGscp("");
        }

        public List<ViewModel.Gscp> GetGscpByScpMkt(string scpMkt)
        {
            return PolicyInjectionFactory.Create().Wrap<IDAL.IGscp>(this).GetAllGscp().Where(x => x.gscp_mkt == scpMkt).ToList();
        }

        public List<ViewModel.Gscp> GetGscp(string where)
        {
            List<ViewModel.Gscp> result = new List<ViewModel.Gscp>();

            string sql = "SELECT * FROM dbo.Gscp_id";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }
            sql += " ORDER BY gscp_id";
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Gscp>().Convert(ds.Tables[0]);
            }
            return result;
        }
    }
}
