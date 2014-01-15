using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using FindProperty.Lib.Json;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class Post:IDAL.IPost
    {
        private IMongoQuery BuildSelectPostWhere(Common.ViewModel.HouseSearchCriteria searchCriteria)
        {
            #region where
            var where = Query.EQ("PostType", searchCriteria.PostType);                       

            if (!string.IsNullOrEmpty(searchCriteria.SubStation))
            {
                where = Query.And(where, Query.EQ("TagCommand", searchCriteria.SubStation));
            }
            else
            {
                if (searchCriteria.SubWay.HasValue)
                {
                    where = Query.And(where, Query.EQ("TagCommand",searchCriteria.SubWay.Value+"号线"));
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.Tag))
            {
                where = Query.And(where, Query.EQ("TagCommand", searchCriteria.Tag));
            }

            if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
            {
                where = Query.And(where, Query.EQ("TagCommand", searchCriteria.KeyWord));
                //where = Query.And(where, Query.Or(Query.EQ("TagCommand", searchCriteria.KeyWord), Query.Matches("CDisplay", searchCriteria.KeyWord)));
            }

            if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", searchCriteria.ScpMkt));
            }

            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", searchCriteria.GscpId));
            }

            if (searchCriteria.MinSize.HasValue && searchCriteria.MinSize.Value!=0)
            {
                where = Query.And(where, Query.GTE("Size", (int)searchCriteria.MinSize.Value));
            }

            if (searchCriteria.MaxSize.HasValue && searchCriteria.MaxSize.Value != 0)
            {
                where = Query.And(where, Query.LTE("Size", (int)searchCriteria.MaxSize.Value));
            }


            switch (searchCriteria.PostType)
            {
                case "S":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where = Query.And(where, Query.GTE("Price", (int)searchCriteria.MinPrice.Value));
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where = Query.And(where, Query.LTE("Price", (int)searchCriteria.MaxPrice.Value));
                    }
                    break;
                case "R":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where = Query.And(where, Query.GTE("Rental", (int)searchCriteria.MinPrice.Value));
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where = Query.And(where, Query.LTE("Rental", (int)searchCriteria.MaxPrice.Value));
                    }
                    break;
            }

            if (searchCriteria.MinBedRoomCount.HasValue && searchCriteria.MinBedRoomCount.Value != 0)
            {
                where = Query.And(where, Query.GTE("BedroomCount", (int)searchCriteria.MinBedRoomCount.Value));
            }

            if (searchCriteria.MaxBedRoomCount.HasValue && searchCriteria.MaxBedRoomCount.Value != 0)
            {
                where = Query.And(where, Query.LTE("BedroomCount", (int)searchCriteria.MaxBedRoomCount.Value));
            }

            if (searchCriteria.HouseType.HasValue)
            {
                switch (searchCriteria.HouseType.Value)
                {
                    case 1:
                        DateTime n = DateTime.Now;
                        where = Query.And(where, Query.GTE("CreateDate", new DateTime(n.Year,n.Month,n.Day) ));
                        break;
                    case 2:
                        where = Query.And(where, Query.EQ("Selection",true));
                        break;
                }
            }

            //add 2013.09.13

            //住宅类型
            if (!string.IsNullOrEmpty(searchCriteria.ddlHouseType))
            {
                where = Query.And(where, Query.EQ("CPropertyType", searchCriteria.ddlHouseType));
            }
            //年限
            if (!string.IsNullOrEmpty(searchCriteria.ddlHousYear))
            {
                switch (searchCriteria.ddlHousYear)
                {
                    case "5":
                        where = Query.And(where, Query.GTE("cblgOpDate", DateTime.Now.AddYears(-5)));
                        where = Query.And(where, Query.LTE("cblgOpDate", DateTime.Now));
                        break;
                    case "10":
                        where = Query.And(where, Query.GTE("cblgOpDate", DateTime.Now.AddYears(-10)));
                        where = Query.And(where, Query.LTE("cblgOpDate", DateTime.Now));
                        break;
                    case "20":
                        where = Query.And(where, Query.GTE("cblgOpDate", DateTime.Now.AddYears(-20)));
                        where = Query.And(where, Query.LTE("cblgOpDate", DateTime.Now));
                        break;
                    case "30":
                        where = Query.And(where, Query.GTE("cblgOpDate", DateTime.Now.AddYears(-30)));
                        where = Query.And(where, Query.LTE("cblgOpDate", DateTime.Now));
                        break;
                    case "100":
                        where = Query.And(where, Query.GTE("cblgOpDate", DateTime.Now.AddYears(-100)));
                        where = Query.And(where, Query.LTE("cblgOpDate", DateTime.Now));
                        break;
                }
            }
            //朝向
            if (!string.IsNullOrEmpty(searchCriteria.ddlOrientations))
            {
                where = Query.And(where, Query.EQ("CDirection", searchCriteria.ddlOrientations));
            }

            //装修
            if (!string.IsNullOrEmpty(searchCriteria.ddlRenovation))
            {
                where = Query.And(where, Query.EQ("TagCommand", searchCriteria.ddlRenovation));
            }
            #endregion

            return where;
        }

        public List<ViewModel.Post> SelectPost(Common.ViewModel.HouseSearchCriteria searchCriteria)
        {
            List<ViewModel.Post> list = new List<ViewModel.Post>();

            #region order
            SortByBuilder orderBy = SortBy.Descending("score");
            if (!string.IsNullOrEmpty(searchCriteria.OrderBy))
            {
                switch(searchCriteria.Order.ToString().ToUpper())
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

            var where = BuildSelectPostWhere(searchCriteria);

            #region select

            list = SelectPost(where, searchCriteria.PageSize, searchCriteria.PageIndex, orderBy);

            #endregion
            
            return list;
        }

        public List<ViewModel.Post> SelectPost(IMongoQuery where,int? pageSize,int? pageIndex,SortByBuilder orderBy)
        {
            #region select
            List<ViewModel.Post> list = new List<ViewModel.Post>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderBy);
                }
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    result = result.SetSkip(pageSize.Value * (pageIndex.Value - 1)).SetLimit(pageSize.Value);
                }

                list = result.ToList<ViewModel.Post>();
            }
            #endregion

            return list;
        }

        public List<ViewModel.Post> SelectPost(string refNo)
        {
            return new DAL.Db.Post().SelectPost(refNo);
        }

        public List<ViewModel.PostDetail> SelectPostDetail(string refNo)
        {
            var result = new List<ViewModel.PostDetail>();
            var where = Query.EQ("_id", refNo);
            using (var db = DbContextFactory.FindPropertyInfomationMongoDatabase<ViewModel.PostDetail>())
            {
                result = db["posts"].Find(where).Take(1).ToList();

                if (result != null && result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        if (item.PostType.ToUpper() == "S")
                        {
                            item.unit_price = Convert.ToInt32(item.Price / item.Size);
                        }
                    }
                }
            }
            return result;
        }
        //add 2013.09.10
        //public List<TempModel.Post> SelectTempPost(string refNo)
        //{
        //    var result = new List<TempModel.Post>();
        //    var where = Query.EQ("_id", refNo);
        //    using (var db = DbContextFactory.FindPropertyInfomationMongoDatabase<TempModel.Post>())
        //    {
        //        result = db["posts"].Find(where).Take(1).ToList();
        //    }
        //    return result;
        //}
        
        public List<ViewModel.Post> SelectTempPost(string refNo)
        {
            var result = new List<ViewModel.Post>();
            var where = Query.EQ("_id", refNo);
            using (var db = DbContextFactory.FindPropertyInfomationMongoDatabase<ViewModel.Post>())
            {
                result = db["posts"].Find(where).Take(1).ToList();
            }
            return result;
        }

        public int SelectPostCount(Common.ViewModel.HouseSearchCriteria searchCriteria)
        {
            int count = 0;
            var where = BuildSelectPostWhere(searchCriteria);
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                count = (int)db["Post"].Find(where).Count();
            }
            return count;
        }

        public List<ViewModel.Post> SelectRecommendPost(string refNo, string agentNo,string bigestcode, string cestCode, string postType, int top)
        {
            IMongoQuery where = Query.EQ("PostType", postType);

            if (!string.IsNullOrEmpty(agentNo))
            {
                where = Query.And(where, Query.EQ("AgentNo", agentNo));
            }

            if (!string.IsNullOrEmpty(refNo))
            {
                where = Query.And(where, Query.NE("_id", refNo));
            }

            if (!string.IsNullOrEmpty(bigestcode))
            {
                where = Query.And(where, Query.EQ("bigestcode", bigestcode));
            }
            if (!string.IsNullOrEmpty(cestCode))
            {
                where = Query.And(where, Query.EQ("cestcode", cestCode));
            }
            
            List<ViewModel.Post> list = new List<ViewModel.Post>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                SortByBuilder orderBy = SortBy.Descending("score");
                result = result.SetSortOrder(orderBy);

                if (top > 0)
                {
                    result = result.SetLimit(top);
                }

                list = result.ToList<ViewModel.Post>();
            }
            return list;
        }

        public List<ViewModel.Post> SelectSimPostByPrice(string refNo, int top, double minPrice, double maxPrice, string scpMkt, string gscpId, string postType)
        {
            var where = Query.EQ("PostType", postType);
            switch (postType.ToUpper())
            {
                case "S":
                    where = Query.And(where,Query.GTE("Price",minPrice));
                    where = Query.And(where, Query.LTE("Price", maxPrice));
                    break;
                case "R":
                    where = Query.And(where, Query.GTE("Rental", minPrice));
                    where = Query.And(where, Query.LTE("Rental", maxPrice));
                    break;
            }

            if (!string.IsNullOrEmpty(scpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", scpMkt));
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", gscpId));
            }
            List<ViewModel.Post> list = new List<ViewModel.Post>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                SortByBuilder orderBy = SortBy.Descending("score");
                result = result.SetSortOrder(orderBy);
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.Post>();
            }
            return list;
        }

        public List<ViewModel.Post> SelectSimPostBySize(string refNo, int top, double minSize, double maxSize, string scpMkt, string gscpId, string postType)
        {
            var where = Query.EQ("PostType", postType);
            where = Query.And(where, Query.GTE("Size", minSize));
            where = Query.And(where, Query.LTE("Size", maxSize));
            
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", scpMkt));
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", gscpId));
            }
            List<ViewModel.Post> list = new List<ViewModel.Post>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                SortByBuilder orderBy = SortBy.Descending("score");
                result = result.SetSortOrder(orderBy);
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.Post>();
            }
            return list;
        }

        public List<ViewModel.Post> SelectSimPostByHouseType(string refNo, string scpMkt, string gscpId, int top, int sittingRoomCount, int bedroomCount, int toiletCount, int balconyCount, int ensuiteCount, string postType)
        {
            var where = Query.EQ("PostType", postType);

            if (!string.IsNullOrEmpty(scpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", scpMkt));
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", gscpId));
            }
            //if (sittingRoomCount > -1)
            //{
            //    where = Query.And(where, Query.EQ("SittingRoomCount", sittingRoomCount));
            //}
            if (bedroomCount > -1)
            {
                where = Query.And(where, Query.EQ("BedroomCount", bedroomCount));
            }
            //if (toiletCount > -1)
            //{
            //    where = Query.And(where, Query.EQ("ToiletCount", toiletCount));
            //}
            //if (balconyCount > -1)
            //{
            //    where = Query.And(where, Query.EQ("BalconyCount", balconyCount));
            //}

            List<ViewModel.Post> list = new List<ViewModel.Post>();
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                SortByBuilder orderBy = SortBy.Descending("score");
                result = result.SetSortOrder(orderBy);
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.Post>();
            }
            return list;
        }

        public int SelectPostCount(Common.ViewModel.AgentInfoSearchCriteria searchCriteria)
        {
            //int count = 0;
            //var where = BuildSelectPostWhere(searchCriteria);
            //using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            //{
            //    count = (int)db["Post"].Find(where).Count();
            //}
            //return count;
            return new DAL.Db.Post().SelectPostCount(searchCriteria);
        }

        public List<ViewModel.Post> SelectPost(Common.ViewModel.AgentInfoSearchCriteria searchCriteria)
        {
            //List<ViewModel.Post> list = new List<ViewModel.Post>();

            //#region order
            //SortByBuilder orderBy = SortBy.Descending("score");
            //if (!string.IsNullOrEmpty(searchCriteria.OrderBy))
            //{
            //    switch (searchCriteria.Order.ToString().ToUpper())
            //    {
            //        case "ASC":
            //            orderBy = SortBy.Ascending(searchCriteria.OrderBy);
            //            break;
            //        case "DESC":
            //            orderBy = SortBy.Descending(searchCriteria.OrderBy);
            //            break;
            //        default:
            //            orderBy = SortBy.Descending(searchCriteria.OrderBy);
            //            break;
            //    }
            //}
            //#endregion

            //var where = BuildSelectPostWhere(searchCriteria);

            //#region select

            //list = SelectPost(where, searchCriteria.PageSize, searchCriteria.PageIndex, orderBy);

            //#endregion

            //return list;
            return new DAL.Db.Post().SelectPost(searchCriteria);
        }

        private IMongoQuery BuildSelectPostWhere(Common.ViewModel.AgentInfoSearchCriteria searchCriteria)
        {
            #region where
            var where = Query.EQ("PostType", searchCriteria.PostType);

            if (!string.IsNullOrEmpty(searchCriteria.KeyWord))
            {
                where = Query.And(where, Query.EQ("TagCommand", searchCriteria.KeyWord));
                //where = Query.And(where, Query.Or(Query.EQ("TagCommand", searchCriteria.KeyWord), Query.EQ("Keywords", searchCriteria.KeyWord)));
                //where = Query.And(where, Query.Or(Query.EQ("TagCommand", searchCriteria.KeyWord), Query.Matches("CDisplay", searchCriteria.KeyWord)));
            }

            if (searchCriteria.MinSize.HasValue && searchCriteria.MinSize.Value != 0)
            {
                where = Query.And(where, Query.GTE("Size", (int)searchCriteria.MinSize.Value));
            }

            if (searchCriteria.MaxSize.HasValue && searchCriteria.MaxSize.Value != 0)
            {
                where = Query.And(where, Query.LTE("Size", (int)searchCriteria.MaxSize.Value));
            }
            if (!string.IsNullOrEmpty(searchCriteria.AgentNo))
            {
                where = Query.And(where, Query.EQ("AgentNo", searchCriteria.AgentNo));
            }

            switch (searchCriteria.PostType)
            {
                case "S":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where = Query.And(where, Query.GTE("Price", (int)searchCriteria.MinPrice.Value));
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where = Query.And(where, Query.LTE("Price", (int)searchCriteria.MaxPrice.Value));
                    }
                    break;
                case "R":
                    if (searchCriteria.MinPrice.HasValue && searchCriteria.MinPrice.Value != 0)
                    {
                        where = Query.And(where, Query.GTE("Rental", (int)searchCriteria.MinPrice.Value));
                    }
                    if (searchCriteria.MaxPrice.HasValue && searchCriteria.MaxPrice.Value != 0)
                    {
                        where = Query.And(where, Query.LTE("Rental", (int)searchCriteria.MaxPrice.Value));
                    }
                    break;
            }

            if (searchCriteria.MinBedRoomCount.HasValue && searchCriteria.MinBedRoomCount.Value != 0)
            {
                where = Query.And(where, Query.GTE("BedroomCount", (int)searchCriteria.MinBedRoomCount.Value));
            }

            if (searchCriteria.MaxBedRoomCount.HasValue && searchCriteria.MaxBedRoomCount.Value != 0)
            {
                where = Query.And(where, Query.LTE("BedroomCount", (int)searchCriteria.MaxBedRoomCount.Value));
            }

            if (searchCriteria.HouseType.HasValue)
            {
                switch (searchCriteria.HouseType.Value)
                {
                    case 1:
                        //where = Query.And(where, Query.GTE("CreateDate", DateTime.Now.ToString("yyyy-MM-dd")));
                        DateTime n = DateTime.Now;
                        where = Query.And(where, Query.GTE("CreateDate", new DateTime(n.Year,n.Month,n.Day) ));
                        break;
                    case 2:
                        where = Query.And(where, Query.EQ("Selection", true));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.GscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", searchCriteria.GscpId));
            }
            else
            {
                if (!string.IsNullOrEmpty(searchCriteria.ScpMkt))
                {
                    where = Query.And(where, Query.EQ("scpMkt", searchCriteria.ScpMkt));
                }
            }

            if (!string.IsNullOrEmpty(searchCriteria.CestCode))
            {
                where = Query.And(where, Query.Or(Query.EQ("cestcode", searchCriteria.CestCode), Query.EQ("bigestcode", searchCriteria.CestCode)));
            }

            #endregion

            return where;
        }

        public List<ViewModel.Post> SelectPostByAgentNo(string agentNo)
        {
            List<ViewModel.Post> list = new List<ViewModel.Post>();
            
            #region where
            var where = Query.EQ("AgentNo", agentNo);

            #endregion

            #region select

            list = SelectPost(where, null, null, null);

            #endregion

            return list;
        }

        public List<ViewModel.Post> SelectRecommendPostByAgentNo(string agentNo, string postType, int top)
        {
            List<ViewModel.Post> list = new List<ViewModel.Post>();

            #region where
            var where = Query.EQ("AgentNo", agentNo);
            
            if (!string.IsNullOrEmpty(postType))
            {
                where = Query.And(where, Query.EQ("PostType", postType));
            }
            #endregion

            #region select

            #region select
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].Find(where);
                SortByBuilder orderBy = SortBy.Descending("score");
                result = result.SetSortOrder(orderBy);

                if (top > 0)
                {
                    result.SetLimit(top);
                }

                list = result.ToList<ViewModel.Post>();
            }
            #endregion

            #endregion

            return list;
        }

        public List<ViewModel.Post> SelectPkPostByRefNos(string refNos)
        {
            List<ViewModel.Post> list = new List<ViewModel.Post>();

            //#region where
            //var where = Query.EQ("RotatedIn", true);

            //var where1 = Query.EQ("$where", "1");
            //string[] refNoAry = refNos.Split(',');
            //for (var i = 0; i < refNoAry.Length; i++)
            //{
            //    where1 = Query.Or(where1, Query.EQ("RefNo", refNoAry[i]));
            //}
            //where = Query.And(where, where1);
            //#endregion

            //#region select
            //using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            //{
            //    var result = db["Post"].Find(where);
            //    SortByBuilder orderBy = SortBy.Descending("score");
            //    result = result.SetSortOrder(orderBy);

            //    list = result.ToList<ViewModel.Post>();
            //}
            //#endregion

            list = new DAL.Db.Post().SelectPkPostByRefNos(refNos);// Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Post, IDAL.IPost>().SelectPkPostByRefNos(refNos);

            return list;
        }

        public List<ViewModel.Post> SelectTopPost(string orderBy, string order, int top)
        {
            List<ViewModel.Post> list = new List<ViewModel.Post>();

            #region order
            SortByBuilder orderByDefault = SortBy.Descending("score");
            if (!string.IsNullOrEmpty(orderBy)&&!string.IsNullOrEmpty(order))
            {
                switch (order.ToString().ToUpper())
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

            #region select
            using (var db = DbContextFactory.FindpropertyMongoDatabase<ViewModel.Post>())
            {
                var result = db["Post"].FindAll();
                if (orderBy != null)
                {
                    result = result.SetSortOrder(orderBy);
                }
                if (top>0)
                {
                    result = result.SetLimit(top);
                }

                list = result.ToList<ViewModel.Post>();
            }
            #endregion

            return list;
        }

        public ViewModel.PostState GetPostState()
        {
            return new DAL.Db.Post().GetPostState();
        }

        public int getPunishCout()
        {
            return new Db.Post().getPunishCout();
        }
    }
}
