using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Factory;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FindProperty.Lib.DBUtility.Mongo.Official;
namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.MDb
{
    public class TagMain:IDAL.ITagMain
    {
        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory)
        {
            List<ViewModel.TagMain> list = new List<ViewModel.TagMain>();
            var where = BuildSelectTagMainWhere(tag, tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMain>())
            {
                var result =db["TagMain"].Find(where);
                list = result.ToList();
            }
            return list;
        }

        private IMongoQuery BuildSelectTagMainWhere(string tag, string tagCategory)
        {
            var where = Query.Matches("TagCommon", tag);
            //where = Query.Or(where, Query.Matches("TagCommon", tag));
            where = Query.Or(where, Query.Matches("TagPYSX", tag.ToUpper()));
            where = Query.Or(where, Query.Matches("TagPY", tag.ToLower()));
            //where = Query.Or(where, Query.Matches("TChinese", tag));
            if (!string.IsNullOrEmpty(tagCategory))
            {
                where = Query.And(where, Query.EQ("TagCategory", tagCategory));
            }
            return where;
        }

        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int top, string orderBy, string order)
        {
            List<ViewModel.TagMain> list = new List<ViewModel.TagMain>();
            var where = BuildSelectTagMainWhere(tag, tagCategory);

            SortByBuilder orderWhere = null;
            if (!string.IsNullOrEmpty(orderBy)&&!string.IsNullOrEmpty(order))
            {
                switch(order.ToString().ToUpper())
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
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMain>())
            {
                var result = db["TagMain"].Find(where);
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


        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int pageSize, int pageIndex, string orderBy, string order)
        {
            List<ViewModel.TagMain> list = new List<ViewModel.TagMain>();
            var where = BuildSelectTagMainWhere(tag, tagCategory);

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
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMain>())
            {
                var result = db["TagMain"].Find(where);
                if (orderWhere != null)
                {
                    result = result.SetSortOrder(orderWhere);
                }
                if (pageSize > 0 && pageIndex > 0)
                {
                    result = result.SetSkip(pageSize * (pageIndex - 1)).SetLimit(pageSize);
                }
                
                list = result.ToList();
            }
            return list;
        }


        public long GetTagCategoryCount(string tagCategory)
        {
            long c = 0;
            var where = Query.EQ("TagCategory", tagCategory);
            using (var db = DbContextFactory.SHTagToSalesBlogMongoDatabase<ViewModel.TagMain>())
            {
                c = db["TagMain"].Find(where).Count();
            }
            return c;
        }
    }
}
