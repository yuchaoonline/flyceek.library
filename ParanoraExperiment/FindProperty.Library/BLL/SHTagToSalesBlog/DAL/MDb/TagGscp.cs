using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.MDb
{
    class TagGscp:IDAL.ITagGscp
    {
        private IMongoQuery BuildSelectTagGscpWhere(string scpMkt, string gscpId, string tag, string tagCategory)
        {
            IMongoQuery where = Query.EQ("$where", "1==1");
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", scpMkt));
            }
            if (!string.IsNullOrEmpty(gscpId))
            {
                where = Query.And(where, Query.EQ("gscpID", gscpId));
            }
            if (!string.IsNullOrEmpty(tag))
            {
                where = Query.And(where, Query.Matches("Tag", tag));
            }
            if (!string.IsNullOrEmpty(tagCategory))
            {
                where = Query.And(where, Query.EQ("TagCategory", tagCategory));
            }
            return where;
        }

        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory,int top)
        {
            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            var where = BuildSelectTagGscpWhere(scpMkt, gscpId, tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagGscp>())
            {
                var result = db["TagGscp"].Find(where);
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.TagGscp>();
            }
            return list;
        }


        public List<ViewModel.TagGscp> SelectTagGscpGroupByTag(string scpMkt, string gscpId, string tag, string tagCategory, int top)
        {
            string mapJs = @"function(){
                emit(this.Tag ,{TagCount:this.TagCount,Tag:this.Tag});
            }";
            string reduceJs = @"function(key,value){
                var sum=0;
                var count=value.length;
                for(var i=0;i<count;i++)
                    sum+=value[i].TagCount;
                }
                return {TagCount:sum,Tag:value[count-1].Tag};
            }";

            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            var where = BuildSelectTagGscpWhere(scpMkt, gscpId, tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<BsonDocument>())
            {
            DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagGscp>();
            
                var result =  db["TagGscp"].MapReduce(where, mapJs, reduceJs);
                var resultList = result.InlineResults.ToList();
                var array = resultList.Select(x => x["value"].ToJson()).ToArray<string>();
                var json = "[" + string.Join(",", array) + "]";

                list = Json.JsonSerializer.DeserializeObject<List<ViewModel.TagGscp>>(json);
            }
            return list;
        }


        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagGscp> list = new List<ViewModel.TagGscp>();
            var where = BuildSelectTagGscpWhere(scpMkt, gscpId, tag, tagCategory);

            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagGscp>())
            {
                var result = db["TagGscp"].Find(where);
                if (top > 0)
                {
                    result = result.SetLimit(top);
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

                if (orderWhere != null)
                {
                    result = result.SetSortOrder(orderWhere);
                }
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.TagGscp>();
            }
            return list;
        }
    }
}
