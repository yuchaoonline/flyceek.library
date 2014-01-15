using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Scp:IDAL.IScp
    {
        public List<Model.Scp> GetAllScp()
        {
            return  GetScp("");
        }

        public List<Model.Scp> GetScpByScpMkt(string scpMkt)
        {
            return PolicyInjectionFactory.Create().Wrap<IDAL.IScp>(this).GetAllScp().Where(x => x.scp_mkt == scpMkt).ToList();
        }

        public List<Model.Scp> GetScp(string where)
        {
            List<Model.Scp> result = new List<Model.Scp>();
            string sql = "SELECT * FROM dbo.Scp_id";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<Model.Scp>().Convert(ds.Tables[0]);
            }
            return result;
        }
    }
}
