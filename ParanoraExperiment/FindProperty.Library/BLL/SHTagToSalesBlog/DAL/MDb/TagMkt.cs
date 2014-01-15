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
    public class TagMkt:IDAL.ITagMkt
    {
        public IMongoQuery BuildSelectTagMktWhere(string scpMkt, string tag, string tagCategory)
        {
            IMongoQuery where = Query.EQ("$where", "1==1");
            if (!string.IsNullOrEmpty(scpMkt))
            {
                where = Query.And(where, Query.EQ("scpMkt", scpMkt));
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

        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top)
        {
            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            IMongoQuery where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMkt>())
            {
                var result = db["TagMkt"].Find(where);
                if (top > 0)
                {
                    result = result.SetLimit(top);
                }
                list = result.ToList<ViewModel.TagMkt>();
            }
            return list;
        }

        public List<ViewModel.TagMkt> SelectTagMktGroupByTag(string scpMkt, string tag, string tagCategory, int top)
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

            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            IMongoQuery where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<BsonDocument>())
            {
                var result = db["TagMkt"].MapReduce(where, mapJs, reduceJs);
                var resultList = result.InlineResults.ToList();
                var array = resultList.Select(x => x["value"].ToJson()).ToArray<string>();
                var json = "[" + string.Join(",", array) + "]";

                list = Json.JsonSerializer.DeserializeObject<List<ViewModel.TagMkt>>(json);
            }
            return list;
        }

        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagMkt> list = new List<ViewModel.TagMkt>();
            IMongoQuery where = BuildSelectTagMktWhere(scpMkt, tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMkt>())
            {            
                var result = db["TagMkt"].Find(where);

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
                list = result.ToList();
            }
            return list;
        }
    }
}
