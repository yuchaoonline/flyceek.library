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

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db
{
    public class TagMain:IDAL.ITagMain
    {
        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory)
        {
            List<ViewModel.TagMain> list = new List<ViewModel.TagMain>();

            string sql = "SELECT * FROM dbo.TagMain";
            string where = BuildSelectTagMainWhere(tag, tagCategory);
            sql += where;
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagMain>().Convert(ds.Tables[0]);
            }
            return list;
        }


        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagMain> list = new List<ViewModel.TagMain>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.TagMain";
            string where = BuildSelectTagMainWhere(tag, tagCategory);
            sql += where;
            if(!string.IsNullOrEmpty(orderBy)&&!string.IsNullOrEmpty(order))
            {
                sql+=" order by "+orderBy+" "+order;
            }
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagMain>().Convert(ds.Tables[0]);
            }
            return list;
        }

        public string BuildSelectTagMainWhere(string tag, string tagCategory)
        {
            string where = " where 1=1";

            if (!string.IsNullOrEmpty(tag))
            {
                where += " and tag like '" + tag + "'";
            }
            if (!string.IsNullOrEmpty(tagCategory))
            {
                where += " and tagCategory = '" + tagCategory + "'";
            }
            return where;
        }


        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int pageSize, int pageIndex, string orderBy, string order)
        {
            return new DAL.MDb.TagMain().SelectTagMain(tag,tagCategory,pageSize,pageIndex,orderBy,order);
        }


        public long GetTagCategoryCount(string tagCategory)
        {
            return new DAL.MDb.TagMain().GetTagCategoryCount(tagCategory);
        }
    }
}
