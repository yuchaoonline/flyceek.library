using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.MDb
{
    public class TagAgent:IDAL.ITagAgent
    {
        public List<ViewModel.TagAgent> SelectTagAgent(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            List<ViewModel.TagAgent> list = new List<ViewModel.TagAgent>();
            var where = Query.EQ("$where", "1==1");
            if (!string.IsNullOrEmpty(agentNo))
            {
                where=Query.And(where,Query.EQ("AgentNo",agentNo));
            }
            if (!string.IsNullOrEmpty(agentName))
            {
                var where1 = Query.EQ("AgentCName", agentName);
                where1 = Query.Or(where1, Query.EQ("AgentCNPYSX", agentName.ToUpper()));
                where1 = Query.Or(where1, Query.EQ("AgentCNPY", agentName.ToLower()));
                where1 = Query.Or(where1, Query.EQ("AgentCNT", agentName));
                where = Query.And(where, where1);
            }
            if (!string.IsNullOrEmpty(agentMobile))
            {
                var where2 = Query.EQ("AgentMobile", agentMobile);
                where = Query.And(where, where2);
            }

            SortByBuilder orderWhere = null;
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                switch (order.ToString().ToUpper())
                {
                    case "ASC":
                        orderWhere = SortBy.Ascending(orderBy);
                        break;
                    case "DESC":
                        orderWhere = SortBy.Descending(orderBy);
                        break;
                    default:
                        orderWhere = SortBy.Descending(orderBy);
                        break;
                }
            }
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagAgent>())
            {
                var result = db["TagAgent"].Find(where);
                //var result = db["TagAgent"].Find(where);
                if (orderWhere != null)
                {
                    result = result.SetSortOrder(orderWhere);
                }
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList();
            }
            return list;
        }


        public List<ViewModel.TagAgent> SelectTagAgentLike(string agentNo, string agentName, string agentMobile, int top, string orderBy, string order)
        {
            List<ViewModel.TagAgent> list = new List<ViewModel.TagAgent>();
            var where = Query.EQ("$where", "1==1");
            if (!string.IsNullOrEmpty(agentNo))
            {
                where = Query.And(where, Query.EQ("AgentNo", agentNo));
            }
            if (!string.IsNullOrEmpty(agentName))
            {
                var where1 = Query.Matches("AgentCName", agentName);
                where1 = Query.Or(where1, Query.Matches("AgentCNPYSX", agentName.ToUpper()));
                where1 = Query.Or(where1, Query.Matches("AgentCNPY", agentName.ToLower()));
                where1 = Query.Or(where1, Query.Matches("AgentCNT", agentName));
                where = Query.And(where, where1);
            }
            if (!string.IsNullOrEmpty(agentMobile))
            {
                var where2 = Query.Matches("AgentMobile", agentMobile);
                where = Query.And(where, where2);
            }

            SortByBuilder orderWhere = null;
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                switch (order.ToString().ToUpper())
                {
                    case "ASC":
                        orderWhere = SortBy.Ascending(orderBy);
                        break;
                    case "DESC":
                        orderWhere = SortBy.Descending(orderBy);
                        break;
                    default:
                        orderWhere = SortBy.Descending(orderBy);
                        break;
                }
            }
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagAgent>())
            {
                var result = db["TagAgent"].Find(where);
                //var result = db["TagAgent"].Find(where);
                if (orderWhere != null)
                {
                    result = result.SetSortOrder(orderWhere);
                }
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList();
            }
            return list;
        }
    }
}
