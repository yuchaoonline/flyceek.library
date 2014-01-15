using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db
{
    public class TagAgent : ITagAgent
    {
        public List<ViewModel.TagAgent> SelectTagAgent(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            string sql = "SELECT * FROM dbo.TagAgent";
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(agentNo))
            {
                where += " and AgentNo='" + agentNo + "'";
            }
            if (!string.IsNullOrEmpty(agentName))
            {
                where += " and ( AgentCName = '" + agentName + "' or";
                where += " and ( AgentCNPYSX = '" + agentName.ToUpper() + "' or";
                where += " and ( AgentCNPY = '" + agentName.ToLower() + "' or";
                where += " and ( AgentCNT = '" + agentName + "' )";
            }
            if (!string.IsNullOrEmpty(agentMobile))
            {
                where += " and AgentMobile='" + agentMobile + "'";
            }
            string orderBy1 = " AboutNum ";
            string order1 = " desc ";
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                orderBy1 = orderBy;
                order1 = order;
            }
            sql += " order by " + orderBy1 + order1;

            List<ViewModel.TagAgent> list = new List<ViewModel.TagAgent>();
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagAgent>().Convert(ds.Tables[0]);
            }
            return list;
        }


        public List<ViewModel.TagAgent> SelectTagAgentLike(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            string sql = "SELECT * FROM dbo.TagAgent";
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(agentNo))
            {
                where += " and AgentNo='" + agentNo + "'";
            }
            if (!string.IsNullOrEmpty(agentName))
            {
                where += " and ( AgentCName like '%" + agentName + "%' or";
                where += " and ( AgentCNPYSX like '%" + agentName.ToUpper() + "%' or";
                where += " and ( AgentCNPY like '%" + agentName.ToLower() + "%' or";
                where += " and ( AgentCNT like '" + agentName + "' )";
            }
            if (!string.IsNullOrEmpty(agentMobile))
            {
                where += " and AgentMobile like '%" + agentMobile + "%'";
            }
            string orderBy1 = " AboutNum ";
            string order1 = " desc ";
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                orderBy1 = orderBy;
                order1 = order;
            }
            sql += " order by " + orderBy1 + order1;

            List<ViewModel.TagAgent> list = new List<ViewModel.TagAgent>();
            DataSet ds = DbContextFactory.SHTagToSalesBlog.ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                list = new DataTableToModeList<ViewModel.TagAgent>().Convert(ds.Tables[0]);
            }
            return list;
        }
    }
}
