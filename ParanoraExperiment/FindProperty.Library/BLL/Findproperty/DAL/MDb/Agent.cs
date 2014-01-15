using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Agent:IDAL.IAgent
    {
        private IMongoQuery BuilderSelectAgentWhere(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            IMongoQuery where = null;

            if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
            {
                if (where == null)
                {
                    where = Query.EQ("mainscpmkt", searchCriteria.ScpMkt.ToUpper());
                }
                else
                {
                    where = Query.And(where, Query.EQ("mainscpmkt", searchCriteria.ScpMkt.ToUpper()));
                }
            }
            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                if (where == null)
                {
                    where = Query.EQ("mainscpid", searchCriteria.GscpId.ToUpper());
                }
                else
                {
                    where = Query.And(where, Query.EQ("mainscpid", searchCriteria.GscpId.ToUpper()));
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
            {
                if (where == null)
                {
                    where = Query.Or(Query.Matches("AgentName", searchCriteria.KeyWord),
                        Query.Matches("AgentCName", searchCriteria.KeyWord), 
                        Query.Matches("AgentMobile", searchCriteria.KeyWord));
                }
                else
                {
                    where = Query.And(where, Query.Or(Query.Matches("AgentName", searchCriteria.KeyWord),
                        Query.Matches("AgentCName", searchCriteria.KeyWord),
                        Query.Matches("AgentMobile", searchCriteria.KeyWord)));
                }
            }

            return where;
        }

        public List<ViewModel.Agent> SelectAgent(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            List<ViewModel.Agent> list = new List<ViewModel.Agent>();
            IMongoQuery where = BuilderSelectAgentWhere(searchCriteria);

            #region order
            SortByBuilder orderBy = SortBy.Descending("agentscore");
            if (!string.IsNullOrEmpty(searchCriteria.OrderBy))
            {
                switch (searchCriteria.Order.ToString().ToUpper())
                {
                    case "ASC":
                        orderBy = SortBy.Ascending(searchCriteria.OrderBy);
                        break;
                    case "DESC":
                        orderBy = SortBy.Descending(searchCriteria.OrderBy);
                        break;
                    default:
                        orderBy = SortBy.Descending(searchCriteria.OrderBy);
                        break;
                }
            }
            #endregion

            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
                var result = db["Agent"].Find(where);

                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderBy);
                }
                if (searchCriteria.PageIndex.HasValue && searchCriteria.PageSize.HasValue)
                {
                    result = result.SetSkip(searchCriteria.PageSize.Value * (searchCriteria.PageIndex.Value - 1)).SetLimit(searchCriteria.PageSize.Value);
                }

                list = result.ToList<ViewModel.Agent>();
            }
            return list;
        }

        public int SelectAgentCount(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            IMongoQuery where = BuilderSelectAgentWhere(searchCriteria);
            int count = 0;
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
             DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>();
             count = (int)db["Agent"].Count(where);
            }            
            return count;
        }

        public ViewModel.Agent GetAgentByAgentName(string agentName)
        {
            ViewModel.Agent agent = null;
            IMongoQuery where = Query.EQ("AgentName", agentName);

            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
                var result = db["Agent"].Find(where);

                agent = result.FirstOrDefault();
            }

            //agent = new DAL.Db.Agent().GetAgentByAgentName(agentName);

            return agent;
        }

        public List<ViewModel.Agent> SelectAgentByManagerNo(string managerNo, int top)
        {
            List<ViewModel.Agent> agents = new List<ViewModel.Agent>();
            IMongoQuery where = Query.EQ("ManagerNo", managerNo);

            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
                var result = db["Agent"].Find(where);
                if (top > 0)
                {
                    result.SetLimit(top);
                }
                agents = result.ToList();
            }
            return agents;
        }

        public List<ViewModel.Agent> SelectAgent(string scpMkt, string gscpId, int top, string orderBy, string order)
        {
            IMongoQuery where = null;

            if (!string.IsNullOrEmpty(scpMkt))
            {
                if (where == null)
                {
                    where = Query.EQ("mainscpmkt", scpMkt.ToUpper());
                }
                else
                {
                    where = Query.And(where, Query.EQ("mainscpmkt", scpMkt.ToUpper()));
                }
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                if (where == null)
                {
                    where = Query.EQ("mainscpid", gscpId.ToUpper());
                }
                else
                {
                    where = Query.And(where, Query.EQ("mainscpid", gscpId.ToUpper()));
                }
            }

            #region order
            SortByBuilder orderByDefault = SortBy.Descending("agentscore");
            if (!string.IsNullOrEmpty(orderBy)&&!string.IsNullOrEmpty(order))
            {
                switch (order.ToUpper())
                {
                    case "ASC":
                        orderByDefault = SortBy.Ascending(orderBy);
                        break;
                    case "DESC":
                        orderByDefault = SortBy.Descending(orderBy);
                        break;
                    default:
                        orderByDefault = SortBy.Descending(orderBy);
                        break;
                }
            }
            #endregion

            List<ViewModel.Agent> list = new List<ViewModel.Agent>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
                DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>();
                var result = db["Agent"].Find(where);
                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderByDefault);
                }
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList();
            }
            return list;
        }

        public ViewModel.Agent GetAgentByAgentNo(string AgentNo)
        {
            ViewModel.Agent agent = null;
            IMongoQuery where = Query.EQ("AgentNo", AgentNo);

            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Agent>())
            {
                var result = db["Agent"].Find(where);

                agent = result.FirstOrDefault();
            }
            return agent;
        }
    }
}
