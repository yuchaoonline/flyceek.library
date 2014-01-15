using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.CentalineEst.DAL.Db
{
    public class EstHeader:IDAL.IEstHeader
    {
        public List<ViewModel.EstHeader> SelectEstHeader(string scpmkt,int top)
        {
            List<ViewModel.EstHeader> result = new List<ViewModel.EstHeader>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.EstHeader WHERE Price>0";
            if (!string.IsNullOrEmpty(scpmkt))
            {
                sql += " and scpmkt ='" + scpmkt + "'";
            }

            DataSet ds = DbContextFactory.CentalineEst.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.EstHeader>().Convert(ds.Tables[0]);
            }
            return result;
        }
    }
}
