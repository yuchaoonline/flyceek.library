using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Region:IDAL.IRegion
    {
        public List<ViewModel.Region> GetAllRegion()
        {
            return GetRegion("");
        }

        public List<ViewModel.Region> GetRegion(string where)
        {
            List<ViewModel.Region> result = new List<ViewModel.Region>();
            string sql = "SELECT * FROM dbo.Region";
            if (!string.IsNullOrEmpty(where))
            {
                sql += " where " + where;
            }
            sql += " ORDER BY scp_mkt";
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Region>().Convert(ds.Tables[0]);
            }
            return result;
        }
    }
}
