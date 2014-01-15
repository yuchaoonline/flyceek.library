using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.BLL.Findproperty.ViewModel;
using FindProperty.Lib.Common.ValueConvert;
using FindProperty.Lib.Factory;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace FindProperty.Lib.BLL.Findproperty.DAL.Db
{
    public class Post:IDAL.IPost
    {
        private SqlCommand BuildSelectPostWhere(HouseSearchCriteria searchCriteria)
        {
            #region command

            SqlCommand dbc = new SqlCommand();

            #endregion

            #region where

            string where = " RotatedIn=1 ";

            //if (!string.IsNullOrEmpty(searchCriteria.Code))
            //{
            //    switch (searchCriteria.Type)
            //    {
            //        case "0":
            //            break;
            //        case "1":
            //            where += " and cblgcode='" + searchCriteria.Code + "' and cblgCName <>''";
            //            break;
            //        case "2":
            //            where += " and cestcode='" + searchCriteria.Code + "' and (cestCName<>'' or cblgCName<>'')";
            //            break;
            //        case "3":
            //            where += " and bigestcode='" + searchCriteria.Code + "' and bigestCName<>''";
            //            break;
            //        case "22":
            //            if (!string.IsNullOrEmpty(searchCriteria.ScopeID))
            //            {
            //                where += " and gscpID='" + searchCriteria.ScopeID + "'";
            //            }
            //            if (!string.IsNullOrEmpty(searchCriteria.ScopeMkt))
            //            {
            //                where += " and scpMkt='" + searchCriteria.ScopeMkt + "'";
            //            }
            //            break;
            //    }
            //}

            if (string.IsNullOrEmpty(searchCriteria.PostType))
            {
                where += " and (PostType='S' or PostType='B')";
            }
            else
            {
                where += " and (PostType='" + searchCriteria.PostType + "' or PostType='B')";
            }

            if (!string.IsNullOrEmpty(searchCriteria.Tag))
            {
                where += " and id in (SELECT PostID FROM dbo.FullTagPost a JOIN tagmain b ON a.TagCommonCode=b.tagcode WHERE tag LIKE '%'+@Tag+'%')";
                dbc.Parameters.Add(new SqlParameter("@Tag", searchCriteria.Tag));
            }

            if (!string.IsNullOrEmpty(searchCriteria.SubStation))
            {
                where += " and id in (SELECT PostID FROM dbo.FullTagPost a JOIN tagmain b ON a.TagCommonCode=b.tagcode WHERE TagCategory='地铁' AND tag LIKE '%'+ @SubStation +'%')";
                dbc.Parameters.Add(new SqlParameter("@SubStation", searchCriteria.SubStation));
            }
            else
            {
                if (searchCriteria.SubWay.HasValue)
                {
                    where += " and id in (SELECT PostID FROM dbo.FullTagPost a JOIN tagmain b ON a.TagCommonCode=b.tagcode WHERE TagCategory='地铁' AND tag ='" + searchCriteria.SubWay.Value.ToString() + "号线')";
                }
            }

            List<string> tagCommonCodeslist = searchCriteria.TagCommonCodes;
            if (tagCommonCodeslist != null && tagCommonCodeslist.Count > 0)
            {
                where += " and EXISTS(SELECT 1 FROM [dbo].[FullTagPost] AS [t1] WHERE ([t1].[PostID] = id) AND ((CONVERT(NVarChar(MAX),[t1].[TagCommonCode])) IN ('" + string.Join("','", tagCommonCodeslist.ToArray()) + "')))";
            }
            else
            {
                if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
                {
                    where += " and (CDisplay like '%'+@KeyWord+'%' or Display like '%'+@KeyWord+'%')";
                    dbc.Parameters.Add(new SqlParameter("@KeyWord", searchCriteria.KeyWord));
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
            {
                where += " and scpMkt='" + searchCriteria.ScpMkt + "'";
            }

            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                where += " and gscpID='" + searchCriteria.GscpId + "'";
            }

            if (searchCriteria.MinSize.HasValue && searchCriteria.MinSize.Value != 0)
            {
                where += " and Size>=" + searchCriteria.MinSize.Value;
            }

            if (searchCriteria.MaxSize.HasValue && searchCriteria.MaxSize.Value != 0)
            {
                where += " and Size<=" + searchCriteria.MaxSize.Value;
            }

            switch (searchCriteria.PostType)
            {
                case "S":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where += " and Price>=" + searchCriteria.MinPrice.Value;
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where += " and Price<=" + searchCriteria.MaxPrice.Value;
                    }
                    break;
                case "R":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where += " and Rental>=" + searchCriteria.MinPrice.Value;
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where += " and Rental<=" + searchCriteria.MaxPrice.Value;
                    }
                    break;
            }

            if (searchCriteria.MinBedRoomCount.HasValue && searchCriteria.MinBedRoomCount.Value != 0)
            {
                where += " and BedroomCount>=" + searchCriteria.MinBedRoomCount.Value;
            }

            if (searchCriteria.MaxBedRoomCount.HasValue && searchCriteria.MaxBedRoomCount.Value != 0)
            {
                where += " and BedroomCount<=" + searchCriteria.MaxBedRoomCount.Value;
            }

            if (searchCriteria.HouseType.HasValue)
            {
                switch (searchCriteria.HouseType.Value)
                {
                    case 1:
                        where += " and CreateDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        break;
                    case 2:
                        where += " and Selection=1";
                        break;
                }
            }

            //住宅类型
            if (!string.IsNullOrEmpty(searchCriteria.ddlHouseType))
            {
                where += " and CPropertyType='" + searchCriteria.ddlHouseType + "'";
            }
            //年限
            if (!string.IsNullOrEmpty(searchCriteria.ddlHousYear))
            {
                switch (searchCriteria.ddlHousYear)
                {
                    case "5":
                        where += " and cblgOpDate>='" + DateTime.Now.AddYears(-5) + "'";
                        where += " and cblgOpDate<='" + DateTime.Now + "'";
                        break;
                    case "10":
                        where += " and cblgOpDate>='" + DateTime.Now.AddYears(-10) + "'";
                        where += " and cblgOpDate<='" + DateTime.Now + "'";
                        break;
                    case "20":
                        where += " and cblgOpDate>='" + DateTime.Now.AddYears(-20) + "'";
                        where += " and cblgOpDate<='" + DateTime.Now + "'";
                        break;
                    case "30":
                        where += " and cblgOpDate>='" + DateTime.Now.AddYears(-30) + "'";
                        where += " and cblgOpDate<='" + DateTime.Now + "'";
                        break;
                    case "100":
                        where += " and cblgOpDate>='" + DateTime.Now.AddYears(-100) + "'";
                        where += " and cblgOpDate<='" + DateTime.Now + "'";
                        break;
                }
            }
            //朝向
            if (!string.IsNullOrEmpty(searchCriteria.ddlOrientations))
            {
                where += "and CDirection='" + searchCriteria.ddlOrientations + "'";
            }

            //装修
            //if (!string.IsNullOrEmpty(searchCriteria.ddlRenovation))
            //{
            //    where += "and TagCommand='" + searchCriteria.ddlRenovation + "'";
            //}
            #endregion

            dbc.CommandText = where;
            dbc.CommandType = CommandType.Text;

            return dbc;
        }

        private SqlCommand BuildSelectPostWhere(AgentInfoSearchCriteria searchCriteria)
        {
            SqlCommand dbc = new SqlCommand();
            #region where
            //string where = " RotatedIn=1 ";
            string where = " 1=1 ";
            if (string.IsNullOrEmpty(searchCriteria.PostType))
            {
                where += " and (PostType='S' or PostType='B')";
            }
            else
            {
                where += " and (PostType='" + searchCriteria.PostType + "' or PostType='B')";
            }

            if (searchCriteria.MinSize.HasValue && searchCriteria.MinSize.Value != 0)
            {
                where += " and Size>=" + searchCriteria.MinSize.Value;
            }

            if (searchCriteria.MaxSize.HasValue && searchCriteria.MaxSize.Value != 0)
            {
                where += " and Size>=" + searchCriteria.MinSize.Value;
            }
            if (!string.IsNullOrEmpty(searchCriteria.AgentNo))
            {
                where += " and AgentNo=@AgentNo";
                dbc.Parameters.Add(new SqlParameter("@AgentNo", searchCriteria.AgentNo));
            }

            switch (searchCriteria.PostType)
            {
                case "S":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where += " and Price>=" + searchCriteria.MinPrice.Value;
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where += " and Price<=" + searchCriteria.MaxPrice.Value;
                    }
                    break;
                case "R":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where += " and Rental>=" + searchCriteria.MinPrice.Value;
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where += " and Rental<=" + searchCriteria.MaxPrice.Value;
                    }
                    break;
            }

            if (searchCriteria.MinBedRoomCount.HasValue && searchCriteria.MinBedRoomCount.Value != 0)
            {
                where += " and BedroomCount>=" + searchCriteria.MinBedRoomCount.Value;
            }

            if (searchCriteria.MaxBedRoomCount.HasValue && searchCriteria.MaxBedRoomCount.Value != 0)
            {
                where += " and BedroomCount<=" + searchCriteria.MaxBedRoomCount.Value;
            }

            if (!string.IsNullOrEmpty(searchCriteria.CestCode))
            {
                where += " and (cestcode=@CestCode or bigestcode=@CestCode)";
                dbc.Parameters.Add(new SqlParameter("@CestCode", searchCriteria.CestCode));
            }


            if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
            {
                where += " and (CDisplay like '%'+@KeyWord+'%' or Display like '%'+@KeyWord+'%')";
                dbc.Parameters.Add(new SqlParameter("@KeyWord", searchCriteria.KeyWord));
            }

            if (searchCriteria.HouseType.HasValue)
            {
                switch (searchCriteria.HouseType.Value)
                {
                    case 1:
                        where += " and CreateDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                        break;
                    case 2:
                        where += " and Selection=1";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                where += " and gscpID='" + searchCriteria.GscpId + "'";
            }
            if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
            {
                where += " and scpMkt='" + searchCriteria.ScpMkt + "'";
            }
            #endregion

            dbc.CommandText = where;
            dbc.CommandType = CommandType.Text;

            return dbc;
        }

        private List<ViewModel.Post> SelectPost(SqlCommand dbc,string orderBy, string order, int? pageSize, int? pageIndex)
        {
            #region sql
            string topSql = string.Empty;

            if (pageSize.HasValue)
            {
                topSql = " top " + pageSize.ToString();
            }

            string orderByV = " score";
            string orderV = " desc";
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                orderByV = orderBy;
                orderV = order;
            }
            string sql = "SELECT " + topSql + " * FROM (SELECT ROW_NUMBER() over(order BY " + orderByV + " " + orderV + ") rowId,(SELECT  TOP 1 c_distname FROM Region WHERE scp_mkt=scpmkt) scpMktc,(SELECT TOP 1 value FROM pub.PostItem WHERE id=a.id AND ItemType='FLOOR TOTAL' ) FloorTotal,* FROM pub.post a";
            if (!string.IsNullOrEmpty(dbc.CommandText))
            {
                sql += " where " + dbc.CommandText;
            }
            if (pageSize.HasValue && pageIndex.HasValue)
            {
                sql += string.Format(" ) D WHERE rowId > {1}*{0}", pageSize, pageIndex.Value - 1);
            }
            else
            {
                sql += ") d";
            }
            dbc.CommandText = sql;
            #endregion

            List<ViewModel.Post> result = new List<ViewModel.Post>();
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Post>().Convert(ds.Tables[0]);
            }

            return result;
        }

        private List<ViewModel.PostDetail> SelectPostDetail(SqlCommand dbc, int top)
        {
            string topSql = string.Empty;
            if (top>0)
            {
                topSql = "top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " (SELECT  TOP 1 c_distname FROM Region WHERE scp_mkt=scpmkt) scpMktc,(SELECT TOP 1 value FROM pub.PostItem WHERE id=a.id AND ItemType='FLOOR TOTAL' ) FloorTotal,* FROM pub.post a";
            if (!string.IsNullOrEmpty(dbc.CommandText))
            {
                sql += " where " + dbc.CommandText;
            }
            dbc.CommandText = sql;
            dbc.CommandType = CommandType.Text;

            List<ViewModel.PostDetail> result = new List<ViewModel.PostDetail>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.PostDetail>().Convert(ds.Tables[0]);

                //填充PostImage和PostItem
                result[0].Images = new PostImage().SelectPostImage(result[0].ID.ToString());
                result[0].Items = new PostItem().SelectPostItem(result[0].ID.ToString());
                result[0].PosId = result[0].ID;
            }
 

            return result;
        }

        private List<ViewModel.Post> SelectPost(SqlCommand dbc, int top)
        {
            string topSql = string.Empty;
            if (top > 0)
            {
                topSql = "top " + top.ToString();
            }
            string sql = "SELECT " + topSql + " (SELECT  TOP 1 c_distname FROM Region WHERE scp_mkt=scpmkt) scpMktc,(SELECT TOP 1 value FROM pub.PostItem WHERE id=a.id AND ItemType='FLOOR TOTAL' ) FloorTotal,* FROM pub.post a";
            if (!string.IsNullOrEmpty(dbc.CommandText))
            {
                sql += " where " + dbc.CommandText;
            }
            dbc.CommandText = sql;
            dbc.CommandType = CommandType.Text;

            List<ViewModel.Post> result = new List<ViewModel.Post>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Post>().Convert(ds.Tables[0]);
            }

            return result;
        }
        
        public List<ViewModel.Post> SelectPost(HouseSearchCriteria searchCriteria)
        {
            SqlCommand dbc = BuildSelectPostWhere(searchCriteria);

            return SelectPost(dbc, searchCriteria.OrderBy, searchCriteria.Order, searchCriteria.PageSize, searchCriteria.PageIndex);
        }

        public List<ViewModel.Post> SelectSimPostByPrice(string refNo, int top, double minPrice, double maxPrice, string scpMkt, string gscpId, string postType)
        {
            string where = string.Empty;
            where = " 1=1";
            switch (postType.ToUpper())
            {
                case "S":
                    where += " and price >=" + minPrice.ToString();
                    where += " and price <=" + maxPrice.ToString();
                    break;
                case "R":
                    where += " and Rental >=" + minPrice.ToString();
                    where += " and Rental <=" + maxPrice.ToString();
                    break;
            }
            if (!string.IsNullOrEmpty(refNo))
            {
                where += " and refNo!='" + refNo + "'";
            }
            if (!string.IsNullOrEmpty(postType))
            {
                where += " and (PostType='" + postType + "' or posttype='B')";
            }
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and scpMkt='" + scpMkt + "'";
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where += " and gscpId='" + gscpId + "'";
            }
            where += " and RotatedIn=1";
            where += " ORDER BY score DESC";
            return SelectPost(new SqlCommand(where),top);
        }

        public List<ViewModel.Post> SelectPost(string refNo)
        {
            string where = string.Empty;
            where = " refno =@refNo";
            SqlCommand dbc = new SqlCommand(where);
            dbc.Parameters.AddWithValue("@refNo", refNo);

            return SelectPost(dbc, 1);
        }

        public List<ViewModel.PostDetail> SelectPostDetail(string refNo)
        {
            string where = string.Empty;
            where = " refno =@refNo";
            SqlCommand dbc = new SqlCommand(where);
            dbc.Parameters.AddWithValue("@refNo", refNo);

            return SelectPostDetail(dbc, 1);
        }

        //add 2013.09.10
        public List<ViewModel.Post> SelectTempPost(string refNo)
        {
            //To do
            return null;
        }

        public int SelectPostCount(HouseSearchCriteria searchCriteria)
        {
            int count = 0;
            string sql = "SELECT count(1) FROM pub.post";
            SqlCommand dbc = BuildSelectPostWhere(searchCriteria);
            if (!string.IsNullOrEmpty(dbc.CommandText))
            {
                sql += " where " + dbc.CommandText;
            }
            dbc.CommandText = sql;

            count = (int)DbContextFactory.Findproperty.ExecuteScalar(dbc);
            
            return count;
        }

        public List<ViewModel.Post> SelectRecommendPost(string refNo, string agentNo, string bigestcode, string cestCode, string postType, int top)
        {
            string where = "1=1";

            if (!string.IsNullOrEmpty(refNo))
            {
                where += " and refNo!='" + refNo + "'";
            }
            //if (!string.IsNullOrEmpty(agentNo))
            //{
            //    where += " and AgentNo=@AgentNo";
            //}
            if (!string.IsNullOrEmpty(cestCode))
            {
                where += " and cestcode='" + cestCode + "'";
            }
            if (!string.IsNullOrEmpty(bigestcode))
            {
                where += " and bigestcode='" + bigestcode + "'";
            }
            if (!string.IsNullOrEmpty(postType))
            {
                where += " and (postType='" + postType + "' or postType='B')";
            }
            where += " and cestCName<>''";
            where += " and RotatedIn=1";
            where += " ORDER BY score DESC";

            SqlCommand dbc = new SqlCommand(where);
            dbc.Parameters.AddWithValue("@AgentNo", agentNo);

            return SelectPost(dbc, top);
        }

        public List<ViewModel.Post> SelectSimPostByHouseType(string refNo, string scpMkt, string gscpId, int top, int sittingRoomCount, int bedroomCount, int toiletCount, int balconyCount, int ensuiteCount, string postType)
        {
            string where = string.Empty;
            where = " 1=1";

            if (!string.IsNullOrEmpty(refNo))
            {
                where += " and refNo!='" + refNo + "'";
            }
            if (!string.IsNullOrEmpty(postType))
            {
                where += " and (PostType='" + postType + "' or posttype='B')";
            }
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and scpMkt='" + scpMkt + "'";
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where += " and gscpId='" + gscpId + "'";
            }
            if (sittingRoomCount>-1)
            {
                where += " and sittingRoomCount='" + sittingRoomCount.ToString() + "'";
            }
            if (bedroomCount > -1)
            {
                where += " and bedroomCount='" + bedroomCount.ToString() + "'";
            }
            if (toiletCount > -1)
            {
                where += " and toiletCount='" + toiletCount.ToString() + "'";
            }
            if (balconyCount > -1)
            {
                where += " and balconyCount='" + balconyCount.ToString() + "'";
            }

            where += " and RotatedIn=1";
            where += " ORDER BY score DESC";

            SqlCommand dbc = new SqlCommand(where);

            return SelectPost(dbc, top);
        }

        public List<ViewModel.Post> SelectSimPostBySize(string refNo, int top, double minSize, double maxSize, string scpMkt, string gscpId, string postType)
        {
            string where = string.Empty;
            where = " 1=1";
            where += " and Size >=" + minSize.ToString();
            where += " and Size <=" + maxSize.ToString();
            if (!string.IsNullOrEmpty(refNo))
            {
                where += " and refNo!='" + refNo + "'";
            }
            if (!string.IsNullOrEmpty(postType))
            {
                where += " and (PostType='" + postType + "' or posttype='B')";
            }
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where += " and scpMkt='" + scpMkt + "'";
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where += " and gscpId='" + gscpId + "'";
            }
            where += " and RotatedIn=1";
            where += " ORDER BY score DESC";
            return SelectPost(new SqlCommand(where), top);
        }

        public int SelectPostCount(AgentInfoSearchCriteria searchCriteria)
        {
            string sql = "SELECT count(1) FROM pub.post";
            SqlCommand dbc = BuildSelectPostWhere(searchCriteria);
            if (!string.IsNullOrEmpty(dbc.CommandText))
            {
                sql += " where " + dbc.CommandText;
            }
            dbc.CommandText = sql;
            int count = (int)DbContextFactory.Findproperty.ExecuteScalar(dbc);
            return count;
        }

        public List<ViewModel.Post> SelectPost(AgentInfoSearchCriteria searchCriteria)
        {
            List<ViewModel.Post> result = new List<ViewModel.Post>();

            SqlCommand dbc = BuildSelectPostWhere(searchCriteria);

            #region select

            result = SelectPost(dbc, searchCriteria.OrderBy, searchCriteria.Order, searchCriteria.PageSize.Value, searchCriteria.PageIndex.Value);

            #endregion

            return result;
        }

        public List<ViewModel.Post> SelectPostByAgentNo(string agentNo)
        {
            string where =" agentNo =@AgentNo";

            SqlCommand dbc = new SqlCommand(where);
            dbc.Parameters.AddWithValue("@AgentNo", agentNo);

            return SelectPost(dbc, 0);
        }

        public List<ViewModel.Post> SelectRecommendPostByAgentNo(string agentNo,string postType,int top)
        {
            string where = " agentNo=@AgentNo and ( posttype=@PostType or posttype='B')  and Selection=1";

            SqlCommand dbc = new SqlCommand(where);
            dbc.Parameters.AddWithValue("@AgentNo", agentNo);
            dbc.Parameters.AddWithValue("@PostType", postType);

            return SelectPost(dbc, top);
        }

        public List<ViewModel.Post> SelectPkPostByRefNos(string refNos)
        {
            string topSql = string.Empty;
            SqlCommand dbc = new SqlCommand();
            string sql = "SELECT  (SELECT  TOP 1 c_distname FROM Region WHERE scp_mkt=scpmkt) scpMktc,(SELECT TOP 1 VALUE FROM pub.PostItem WHERE id=a.id) DECORATION,(SELECT TOP 1 value FROM pub.PostItem WHERE id=a.id AND ItemType='FLOOR TOTAL' ) FloorTotal,(SELECT Path FROM pub.PostImage WHERE RefType='LAYOUT' and id=a.id) LAYOUT,* FROM pub.post a";
            sql += " where ";

            string[] refNoAry = refNos.Split(',');

            for(var i=0;i<refNoAry.Length;i++)
            {
                sql += " refno = @refNo" + i.ToString();
                if ((i+1) < refNoAry.Length) { sql += " or "; }
                dbc.Parameters.AddWithValue("@refNo" + i.ToString(), refNoAry[i]);
            }

            dbc.CommandText = sql;
            dbc.CommandType = CommandType.Text;

            List<ViewModel.Post> result = new List<ViewModel.Post>();

            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Post>().Convert(ds.Tables[0]);
            }

            return result;
        }

        public List<ViewModel.Post> SelectTopPost(string orderBy, string order, int top)
        {
            #region sql
            string topSql = string.Empty;

            if (top>0)
            {
                topSql = " top " + top.ToString();
            }

            string orderByV = " score";
            string orderV = " desc";
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(order))
            {
                orderByV = orderBy;
                orderV = order;
            }
            string sql = "SELECT " + topSql + " * FROM pub.post ";
            sql += orderByV + orderV;
            #endregion

            List<ViewModel.Post> result = new List<ViewModel.Post>();
            DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(CommandType.Text,sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                result = new DataTableToModeList<ViewModel.Post>().Convert(ds.Tables[0]);
            }

            return result;
        }

        public PostState GetPostState()
        {
            PostState ps = new PostState();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select (select count(*) from pub.post where posttype in ('s','b')  and  RotatedIn=1) SaleCount,(select count(*) from pub.post where posttype='r'  and  RotatedIn=1) RentCount");
                SqlCommand dbc = new SqlCommand();
                dbc.CommandText = sql.ToString();
                dbc.CommandType = CommandType.Text;
                DataSet ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
                if (ds != null && ds.Tables.Count > 0)
                {
                    ps = new DataTableToModeList<ViewModel.PostState>().Convert(ds.Tables[0])[0];
                }
            }
            catch
            {
                throw;
            }
            return ps;
        }

        public int getPunishCout()
        {
            int returnNum = 0;

            System.Text.StringBuilder strsql = new System.Text.StringBuilder();
            strsql = new System.Text.StringBuilder();
            strsql.Append(" SELECT COUNT(*) as postnewcount FROM");
            strsql.AppendFormat(" pub.Post WHERE  CreateDate  BETWEEN '{0}' AND  '{1}' and RotatedIn=1",
                DateTime.Today.ToString(),
                DateTime.Today.AddDays(1).ToString());
            System.Data.DataSet ds = new DataSet();
            SqlCommand dbc = new SqlCommand();
            dbc.CommandText = strsql.ToString();
            dbc.CommandType = CommandType.Text;
            ds = DbContextFactory.Findproperty.ExecuteDataSet(dbc);
            if (ds.Tables.Count > 0)
            {
                returnNum = Convert.ToInt32(ds.Tables[0].Rows[0]["postnewcount"]);
            }
            return returnNum;
        }  
    }
}
