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
    public class TagGscp:IDAL.ITagGscp
    {
        private string BuildSelectTagGscpWhere(string scpMkt, string gscpId, string tag, string tagCategory)
        {
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and scpMkt='" + scpMkt + "'";
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where += " and gscpId='" + gscpId + "'";
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

        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory,int top)
        {
            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql=" top "+top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.TagSearchGscp";
            string where= BuildSelectTagGscpWhere(scpMkt,gscpId,tag,tagCategory);
            sql += where;
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagGscp>().Convert(ds.Tables[0]);
            }
            return list;
        }

        public List<ViewModel.TagGscp> SelectTagGscpGroupByTag(string scpMkt, string gscpId, string tag, string tagCategory, int top)
        {
            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = " SELECT "+topSql+" MAX(scpMkt) scpMkt,MAX(gscpID) gscpID,Tag,SUM(TagCount) TagCount,SUM(seq) seq FROM (";
            sql += "SELECT * FROM dbo.TagSearchGscp";
            string where = BuildSelectTagGscpWhere(scpMkt, gscpId, tag, tagCategory);
            sql += where;
            sql += " ) a GROUP BY a.tag";

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagGscp>().Convert(ds.Tables[0]);
            }

            return list;
        }

        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " * FROM dbo.TagSearchGscp";
            string where = BuildSelectTagGscpWhere(scpMkt, gscpId, tag, tagCategory);
            sql += where;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy + " " + order;
            }
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagGscp>().Convert(ds.Tables[0]);
            }
            return list;
        }
    }
}
