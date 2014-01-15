using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace FindProperty.Lib.BLL.Common.DAL.Db
{
    public class SearchAutoComplete:IDAL.ISearchAutoComplete
    {
        public List<ViewModel.SearchAutoComplete> GetSearchAutoComplete(string keyWord, string postType, int searchType)
        {
            List<ViewModel.SearchAutoComplete> list = new List<ViewModel.SearchAutoComplete>();
            Database db = DbContextFactory.SHTagToSalesBlog;
            DbCommand dbc = db.GetStoredProcCommand("dbo.USP_SearchTagAutoList");
            dbc.CommandType = CommandType.StoredProcedure;

            db.AddInParameter(dbc, "@Tag", DbType.String, keyWord);
            if (!string.IsNullOrEmpty(postType))
            {
                db.AddInParameter(dbc, "@PostType", DbType.String, postType);
            }
            
            DataSet ds = db.ExecuteDataSet(dbc);
            
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.SearchAutoComplete>().Convert(ds.Tables[0]);
            }
            return list;
        }
    }
}
