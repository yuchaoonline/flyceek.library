using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneNetMvcAppTest.Lib.Common;
using LuceneNetMvcAppTest.Lib.DataSource;
using LuceneNetMvcAppTest.Lib.Index;
using LuceneNetMvcAppTest.Models;
using LuceneNetMvcAppTest.ViewModels;
using PanGu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LuceneNetMvcAppTest.Controllers
{
    public class PropertyController : Controller
    {
        //
        // GET: /Property/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            string k = Request["k"];
            string pageIndexStr = Request["p"];

            double indexSearchTime = 0;
            double dbSearchTime = 0;
            int totalItemCount = 0;
            int pageIndex = 1;
            int pageSize = ConfigInfo.FindProperty_LuceneSearch_PageSize;
            if (!int.TryParse(pageIndexStr, out pageIndex))
            {
                pageIndex = 1;
            }

            PropertySearch result = new PropertySearch();
            result.Propertys = new List<Models.Property>();
            result.PagerInfo = new PagerInfo
            {
                PageSize = pageSize,
                CurrentPageIndex = pageIndex
            };

            if (!string.IsNullOrEmpty(k))
            {
                result.KeyWord = HttpUtility.UrlDecode(k, System.Text.Encoding.UTF8);

                PanGuProvider.PanGuInit(Server.MapPath(ConfigInfo.PanGuConfigPath));                

                PropertyIndexProvider propertyIndex = new PropertyIndexProvider(ConfigInfo.FindProperty_LuceneIndex_StorePath);

                List<PropertyLuceneIndexDocument> indexDocs = propertyIndex.SearchSplitKeyWords(result.KeyWord,
                    pageIndex,
                    pageSize,
                    out totalItemCount,
                    out indexSearchTime);

                List<string> refNos = indexDocs.Select(x => x.Id).ToList<string>();

                result.Propertys = new PropertySqlDbProvider().Search(refNos);

                result.PagerInfo.TotalItemCount = totalItemCount;
                result.SearchTimeConsuming = indexSearchTime+dbSearchTime;
            }

            return View(result);
        }

        public ActionResult LuceneSearch()
        {
            string k = Request["k"];
            string pageIndexStr = Request["p"];

            double searchTime=0;
            int totalItemCount=0;
            int pageIndex = 1;
            int pageSize = ConfigInfo.FindProperty_LuceneSearch_PageSize;
            if (!int.TryParse(pageIndexStr, out pageIndex))
            {
                pageIndex = 1;
            }
            
            PropertyLuceneSearch result = new PropertyLuceneSearch();
            result.PropertyLuceneIndexDocuments = new List<Models.PropertyLuceneIndexDocument>();
            result.PagerInfo = new PagerInfo
            {
                PageSize = pageSize,
                CurrentPageIndex = pageIndex
            };

            if (!string.IsNullOrEmpty(k))
            {
                PanGuProvider.PanGuInit(Server.MapPath(ConfigInfo.PanGuConfigPath));

                result.KeyWord = HttpUtility.UrlDecode(k, System.Text.Encoding.UTF8);

                PropertyIndexProvider propertyIndex = new PropertyIndexProvider(ConfigInfo.FindProperty_LuceneIndex_StorePath);

                result.PropertyLuceneIndexDocuments = propertyIndex.SearchSplitKeyWords(result.KeyWord,
                    pageIndex, 
                    pageSize, 
                    out totalItemCount, 
                    out searchTime);

                List<string> refNos = result.PropertyLuceneIndexDocuments.Select(x => x.Id).ToList<string>();



                result.PagerInfo.TotalItemCount = totalItemCount;
                result.SearchTimeConsuming = searchTime;
            }
            return View(result);
        }

    }
}
