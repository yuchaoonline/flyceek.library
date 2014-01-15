using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db
{
    public class TagMkt:IDAL.ITagMkt
    {
        public string BuildSelectTagMktWhere(string scpMkt, string tag, string tagCategory)
        {
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and scpMkt='" + scpMkt + "'";
            }
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

        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top)
        {
            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.TagSearchMkt";
            string where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            sql += where;
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagMkt>().Convert(ds.Tables[0]);
            }
            return list;
        }

        public List<ViewModel.TagMkt> SelectTagMktGroupByTag(string scpMkt, string tag, string tagCategory, int top)
        {
            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = " SELECT " + topSql + " MAX(scpMkt) scpMkt,Tag,SUM(TagCount) TagCount,SUM(seq) seq FROM (";
            sql += "SELECT * FROM dbo.TagSearchMkt";
            string where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            sql += where;
            sql += " ) a GROUP BY a.tag";

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagMkt>().Convert(ds.Tables[0]);
            }

            return list;
        }


        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.TagSearchMkt";
            string where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            sql += where;
            if(!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy + " " + order;
            }
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagMkt>().Convert(ds.Tables[0]);
            }
            return list;
        }
    }
}
