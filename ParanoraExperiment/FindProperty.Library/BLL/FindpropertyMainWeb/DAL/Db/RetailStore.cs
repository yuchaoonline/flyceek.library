using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.DAL.Db
{
    public class RetailStore:IDAL.IRetailStore
    {
        public List<ViewModel.RetailStore> GetTopRetailStoreByArea(int top, string scpMkt, string gscp)
        {
            System.Text.StringBuilder strSql = new System.Text.StringBuilder();

            if (string.IsNullOrEmpty(scpMkt) && string.IsNullOrEmpty(gscp))
            {
                strSql.Append("select top " + top + " * FROM RetailStore order by newid()");
            }
            else
            {
                if (!string.IsNullOrEmpty(gscp))
                {
                    strSql.Append("select top " + top + " * FROM RetailStore where Sector='" + gscp + "'");
                }
                else
                {
                    strSql.Append("select top " + top + " * FROM RetailStore where Region='" + scpMkt + "'");
                }
                strSql.Append(" order by newid()");      
            }
            SqlCommand dbc = new SqlCommand(strSql.ToString());
            dbc.CommandType = CommandType.Text;
            DataSet ds = DbContextFactory.FIndpropertyMainWeb.ExecuteDataSet(dbc);
            List<ViewModel.RetailStore> lst = new List<ViewModel.RetailStore>();
            if (ds != null && ds.Tables.Count > 0)
            {
                lst = new DataTableToModeList<ViewModel.RetailStore>().Convert(ds.Tables[0]);
            }
            return lst;
        }
    }
}
