using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneNetMvcAppTest.Models;
using PanGu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace LuceneNetMvcAppTest.Lib.Index
{
    public class PropertyIndexProvider
    {
        string _indexStroePath;

        public PropertyIndexProvider(string indexStorePath)
        {
            _indexStroePath = indexStorePath;
        }

        public List<PropertyLuceneIndexDocument> SearchSplitKeyWords(string k, int pageIndex, int pageSize, out int resultCount,out double searchTime)
        {
            resultCount = 0;
            searchTime = 0;
            return Search(k, PanGuProvider.GetKeyWordsSplitBySpace(k, new PanGuTokenizer()), pageIndex, pageSize, out resultCount, out searchTime);
        }

        public List<PropertyLuceneIndexDocument> Search(string k,string searchKeyWords, int pageIndex, int pageSize, out int resultCount, out double searchTime)
        {
            resultCount = 0;
            searchTime = 0;
            List<PropertyLuceneIndexDocument> list = new List<PropertyLuceneIndexDocument>();

            try
            {
                Stopwatch sw = new Stopwatch();

                DirectoryInfo directoryInfo = System.IO.Directory.CreateDirectory(_indexStroePath);
                IndexSearcher search = new IndexSearcher(FSDirectory.Open(directoryInfo), true);
                
                Sort sort = new Sort(new SortField("index", SortField.INT, false));
                QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,"info", new PanGuAnalyzer(true));
                Query query = parser.Parse(searchKeyWords);
                BooleanQuery bq = new BooleanQuery();
                bq.Add(query, Occur.SHOULD);

                sw.Start();

                //Hits hits = search.Search(bq);

                TopDocs docs = search.Search(bq, pageSize * pageIndex);

                sw.Stop();

                resultCount = docs.TotalHits;
                searchTime = sw.Elapsed.TotalMilliseconds;

                for (var i = pageSize * (pageIndex - 1); i < pageSize * pageIndex && i < resultCount; i++)
                {
                    list.Add(DocumentToPropertyLuceneIndexDocument(k, search.Doc(docs.ScoreDocs[i].Doc)));
                }

                search.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               
            }

            return list;
        }

        public PropertyLuceneIndexDocument DocumentToPropertyLuceneIndexDocument(string k,Document doc)
        {
            PropertyLuceneIndexDocument propertyLuceneIndexDocument = new PropertyLuceneIndexDocument();
            propertyLuceneIndexDocument.Index = int.Parse(doc.Get("rowIndex"));
            propertyLuceneIndexDocument.Id = doc.Get("id");
            propertyLuceneIndexDocument.Info = PanGuProvider.HighLightContent(k, doc.Get("info"),"<font color=\"red\">","</font>");
            return propertyLuceneIndexDocument;
        }

        
    }
}