using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Agent:IDAL.IAgent
    {
        public List<ViewModel.Agent> SelectAgent(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            SqlCommand dbc = BuildSelectAgentWhere(searchCriteria);
            string orderBy = " agentscore ";
            string order = " desc";
            if (!string.IsNullOrEmpty(searchCriteria.OrderBy) && !string.IsNullOrEmpty(searchCriteria.Order))
            {
                orderBy = searchCriteria.OrderBy;
                order = searchCriteria.Order;
            }
            return SelectAgent(dbc, orderBy,order, searchCriteria.PageSize.Value, searchCriteria.PageIndex.Value);
        }

        private List<ViewModel.Agent> SelectAgent(SqlCommand dbc, string orderBy,string order, int pageSize, int pageIndex)
        {
            List<ViewModel.Agent> result = new List<ViewModel.Agent>();
            string where = dbc.CommandText;
            string sql = "SELECT top " + pageSize.ToString() + " * FROM(SELECT row_number() over (order by " + orderBy + " " + order + ") AS rowId,(SELECT sum(post_counter) FROM dbo.agent_score WHERE agentno=b.agentno) post_counter,(SELECT TOP 1 EagleMemberType FROM CentaEagle_awards WHERE StaffNo=b.agentno ORDER BY CompetitionYear DESC) EagleMemberType,* FROM(SELECT * FROM (select row_number() over (PARTITION by a1.AgentName order by post_counter DESC) as rid,a1.AgentName,a1.AgentNo,AgentCName,AgentEmail,AgentMobile,BranchCName,agentscore,agentscore1,agentscore2,agentscore3,agentscore4,agentscore5,b1.scpid mainscpid,(SELECT gscp_mkt FROM dbo.Gscp_id WHERE gscp_id=b1.scpid) mainscpmkt FROM dbo.agent_score a1 join dbo.agent_scope b1 on a1.AgentNo=b1.AgentNo) a WHERE rid=1 and " + where + ") b ) b1";
            sql += string.Format(" WHERE rowId > {1}*{0}", pageSize, pageIndex - 1);

            dbc.CommandText = sql;

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Agent>().Convert(ds.Tables[0]);
             
                string agentNos=string.Join("','",result.Select(x=>x.AgentNo.Trim()).ToArray());

                string agentMainBusinessSql = " SELECT AgentName,AgentNo,scp_c,scpid,scpMkt,(SELECT TOP 1 c_distname FROM dbo.Region WHERE scpMkt=a.scpMkt) c_distname FROM dbo.agent_score a where AgentNo in ('" + agentNos + "') ORDER BY a.post_counter desc";

                agentMainBusinessSql += " SELECT (SELECT COUNT(1) FROM pub.post WHERE agentno=a.agentno AND (PostType='S' OR PostType='B') AND (bigestcode=Code OR cestcode=Code) ) SCount,(SELECT COUNT(1) FROM pub.post WHERE agentno=a.agentno AND (PostType='R' OR PostType='B') AND (bigestcode=Code OR cestcode=Code)) RCount,* FROM(select ROW_NUMBER() OVER (PARTITION by AgentName,CASE when bigestcode='' OR bigestcode IS null THEN cestcode ELSE bigestcode end order BY  UpdateDate DESC) rowId,AgentName,AgentNo,CnEstate EstName,CASE when bigestcode='' OR bigestcode IS null THEN cestcode ELSE bigestcode END Code FROM pub.Post WHERE AgentNo in ('" + agentNos + "') ) a where a.rowid=1";


                DataSet agentMainBusiness = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, agentMainBusinessSql);

                if (agentMainBusiness != null && agentMainBusiness.Tables.Count == 2)
                {
                    List<ViewModel.AgentMainBusinessGscp> agentMainBusinessGscpList = new List<ViewModel.AgentMainBusinessGscp>();
                    List<ViewModel.AgentMainBusinessEstate> agentMainBusinessEstateList = new List<ViewModel.AgentMainBusinessEstate>();
                    
                    if (agentMainBusiness.Tables[0].Rows.Count > 0)
                    {
                        agentMainBusinessGscpList = new DataTableToModeList<ViewModel.AgentMainBusinessGscp>().Convert(agentMainBusiness.Tables[0]);
                    }

                    if (agentMainBusiness.Tables[1].Rows.Count > 0)
                    {
                        agentMainBusinessEstateList = new DataTableToModeList<ViewModel.AgentMainBusinessEstate>().Convert(agentMainBusiness.Tables[1]);
                    }                    

                    result.ForEach(x =>
                    {
                        x.AgentEsts = agentMainBusinessEstateList.Where(d => d.AgentNo == x.AgentNo).Take(3).ToList();
                        x.Scps = agentMainBusinessGscpList.Where(d => d.AgentNo == x.AgentNo).Take(3).ToList();
                    });
                }
            }

            return result;
        }

        private SqlCommand BuildSelectAgentWhere(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            SqlCommand dbc = new SqlCommand();

            string where = "1=1";

            if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
            {
                where += " and mainscpmkt ='" + searchCriteria.ScpMkt + "'";
            }
            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                where += " and mainscpid ='" + searchCriteria.GscpId + "'";
            }

            if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
            {
                where += " and (AgentCName like '%'+@KeyWord+'%' or AgentName like '%'+@KeyWord+'%' or AgentMobile like '%'+@KeyWord+'%')";
                dbc.Parameters.Add(new SqlParameter("@KeyWord", searchCriteria.KeyWord));
            }

            dbc.CommandText = where;
            dbc.CommandType = CommandType.Text;

            return dbc;
        }

        public int SelectAgentCount(Common.ViewModel.AgentListSearchCriteria searchCriteria)
        {
            SqlCommand dbc = BuildSelectAgentWhere(searchCriteria);

            string sql = "SELECT COUNT(1) FROM(SELECT a.scpid mainscpid,(SELECT gscp_mkt FROM dbo.Gscp_id WHERE gscp_id=a.scpid) mainscpmkt,AgentName,AgentCName FROM dbo.agent_scope a JOIN dbo.agent_score b ON a.agentno=b.agentno) a1";
        
            sql+=" where "+dbc.CommandText;

            dbc.CommandText = sql;

            return (int)DbContextFactory.Findproperty.ExecuteScalar(dbc);
        }

        public ViewModel.Agent GetAgentByAgentName(string agentName)
        {
            string sql = "SELECT TOP 1 *,(SELECT SUM(post_counter) FROM dbo.agent_score WHERE agentno=a.agentno) post_counter,(SELECT TOP 1 EagleMemberType FROM CentaEagle_awards WHERE StaffNo=a.agentno ORDER BY CompetitionYear DESC) EagleMemberType,(SELECT TOP 1 managerno FROM pub.Post WHERE agentno=a.agentno) ManagerNo FROM dbo.agent_score a";
            sql += " where agentName=@AgentName";

            SqlCommand dbc= new SqlCommand(sql);
            dbc.Parameters.AddWithValue("@AgentName", agentName);
            dbc.CommandType = CommandType.Text;

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);

            ViewModel.Agent agent = null;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    agent = new DataTableToModeList<ViewModel.Agent>().Convert(ds.Tables[0])[0];

                    if (agent!=null)
                    {
                        sql = "SELECT * FROM dbo.awards WHERE staff_no ='" + agent.AgentNo + "'";

                        DataSet ds1 = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text,sql);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            agent.Awards = new DataTableToModeList<ViewModel.Awards>().Convert(ds1.Tables[0]);
                        }
                    }
                }
            }
            return agent;
        }

        public List<ViewModel.Agent> SelectAgentByManagerNo(string managerNo,int top)
        {
            string topSql = string.Empty;

            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }

            string sql = "SELECT " + topSql + " *,(SELECT SUM(post_counter) FROM dbo.agent_score WHERE AgentNo=a.AgentNo) post_counter  FROM(SELECT ROW_NUMBER() OVER (PARTITION BY AgentNo ORDER BY UpdateDate desc) rowId,AgentCName,AgentName,AgentNo,BranchCName FROM pub.Post WHERE ManagerNo =@ManagerNo ) a WHERE a.rowId=1 ORDER BY post_counter desc";

            SqlCommand dbc = new SqlCommand(sql);
            dbc.Parameters.AddWithValue("@ManagerNo", managerNo);
            dbc.CommandType = CommandType.Text;

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);

            List<ViewModel.Agent> agents = new List<ViewModel.Agent>();

            if (ds != null && ds.Tables.Count > 0)
            {
                agents = new DataTableToModeList<ViewModel.Agent>().Convert(ds.Tables[0]);
            }
            return agents;
        }

        public List<ViewModel.Agent> SelectAgent(string scpMkt, string gscpId, int top, string orderBy, string order)
        {
            //string topSql = string.Empty;

            //if (top > 0)
            //{
            //    topSql = " top " + top.ToString();
            //}
            //string sql = "SELECT " + topSql + " * FROM(SELECT a.scpid mainscpid,(SELECT gscp_mkt FROM dbo.Gscp_id WHERE gscp_id=a.scpid) mainscpmkt,(SELECT SUM(post_counter) FROM dbo.agent_score WHERE AgentNo=a.AgentNo) post_counter,AgentCName,a.AgentNo,BranchCName,b.agentscore FROM dbo.agent_scope a JOIN dbo.agent_score b ON a.AgentNo=b.AgentNo and a.scpid=b.scpid) a1";
            //string where = " where 1=1 ";
            //if (!string.IsNullOrEmpty(scpMkt))
            //{
            //    where += " and mainscpmkt ='" + scpMkt + "'";
            //}
            //if (!string.IsNullOrEmpty(gscpId))
            //{
            //    where += " and mainscpid ='" +gscpId + "'";
            //}
            //where += " order by " + orderBy + " " + order;


            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = " top " + top.ToString();
            }
            string sql = "SELECT "+topSql+" * FROM(SELECT   a.scpid mainscpid,(SELECT gscp_mkt FROM dbo.Gscp_id WHERE gscp_id=a.scpid) mainscpmkt,(SELECT SUM(post_counter) FROM dbo.agent_score WHERE AgentNo=a.AgentNo) post_counter,AgentCName,a.AgentNo,BranchCName,b.agentscore  FROM dbo.agent_scope a JOIN dbo.agent_score b ON a.AgentNo=b.AgentNo and a.scpid=b.scpid ) aa";
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and mainscpmkt ='" + scpMkt + "'";
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where += " and mainscpid ='" + gscpId + "'";
            }
            where += " order by " + orderBy + " " + order;
            sql += where;

            SqlCommand dbc = new SqlCommand(sql);
            dbc.CommandType = CommandType.Text;

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);

            List<ViewModel.Agent> agents = new List<ViewModel.Agent>();

            if (ds != null && ds.Tables.Count > 0)
            {
                agents = new DataTableToModeList<ViewModel.Agent>().Convert(ds.Tables[0]);
            }
            return agents;
        }

        public ViewModel.Agent GetAgentByAgentNo(string AgentNo)
        {
            string sql = "SELECT TOP 1 *,(SELECT SUM(post_counter) FROM dbo.agent_score WHERE agentno=a.agentno) post_counter,(SELECT TOP 1 EagleMemberType FROM CentaEagle_awards WHERE StaffNo=a.agentno ORDER BY CompetitionYear DESC) EagleMemberType,(SELECT TOP 1 managerno FROM pub.Post WHERE agentno=a.agentno) ManagerNo FROM dbo.agent_score a";
            sql += " where AgentNo=@AgentNo";

            SqlCommand dbc = new SqlCommand(sql);
            dbc.Parameters.AddWithValue("@AgentNo", AgentNo);
            dbc.CommandType = CommandType.Text;

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);

            ViewModel.Agent agent = null;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    agent = new DataTableToModeList<ViewModel.Agent>().Convert(ds.Tables[0])[0];

                    if (agent != null)
                    {
                        sql = "SELECT * FROM dbo.awards WHERE staff_no ='" + agent.AgentNo + "'";

                        DataSet ds1 = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text, sql);
                        if (ds1 != null && ds1.Tables.Count > 0)
                        {
                            agent.Awards = new DataTableToModeList<ViewModel.Awards>().Convert(ds1.Tables[0]);
                        }
                    }
                }
            }
            return agent;
        }
    }
}
