using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Cms.DAL.Db
{
    public class IcmArticle:IDAL.IIcmArticle
    {
        public List<ViewModel.IcmArticle> SelectIcmArticleByPropValue(string propValue,int top,string orderBy,string order)
        {
            string topSql = string.Empty;
            if (top > -1)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " b.* FROM dbo.icmArticleProp a JOIN dbo.icmArticle b ON a.articleID=b.articleID";

            if (!string.IsNullOrEmpty(propValue))
            {
                sql += " where a.propValue LIKE '%" + propValue + "%'";
            }
            if (!string.IsNullOrEmpty(orderBy)&&!string.IsNullOrEmpty(order))
            {
                sql += " order by " + orderBy + " " + order;
            }

            List<ViewModel.IcmArticle> result = new List<ViewModel.IcmArticle>();

            DataSet ds = DbContextFactory.Cms.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.IcmArticle>().Convert(ds.Tables[0]);
            }

            return result;
        }
    }
}
