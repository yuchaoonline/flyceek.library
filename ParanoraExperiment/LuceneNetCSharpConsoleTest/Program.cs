using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Diagnostics;
using PanGu;
using System.IO;
using Lucene.Net.Analysis.PanGu;
using LuceneNetCSharpConsoleTest.Lib;
using System.Data;
using PanGu.Match;


namespace LuceneNetCSharpConsoleTest
{
    public class Article 
    {
        public string Id { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Score { get; set; }
        public DateTime CreateTime { get; set; }
    }


    class Program
    {
        static string INDEX_STORE_PATH = "SearchIndex";

        static string FindPropert_Post_IndexStorePath = "FindPropert_Post_Index";

        static string FindPropertyDbConnectionString = "Data Source=10.4.99.11;Initial Catalog=findproperty;User ID=ecFindpropertyR;Password=94djghrungvgl((UUd";

        static void Main(string[] args)
        {
            AppStart();


            CreateFindPropertyPostIndex(FindPropert_Post_IndexStorePath);

            //PanGuParticiple("公交：639路、797内圈、573路、61路、施崂专线、新川专线 幼儿园：上海市浦东新区、东园幼儿园（商城路）陆家嘴桃林幼儿园等 综合商场：正大广场、八佰伴 医院：上海华美女子医院、明德五官科医院等 邮局：中国邮政招商银行邮政服务处（浦东区局）中国邮政洋泾邮政所（浦东区局） 招商银行上海分行民生支行、中国银行上海分行国际肮运大厦支行、中国工商银行上海分行浦东大道支行 其他：平安信用卡便利还款点 小区内部配套：商业设施其全。");

            //SearchFindPropertyPosts(FindPropert_Post_IndexStorePath, "唐镇");
            
            Console.ReadKey();
        }
        public static void AppStart()
        {
            PanGu.Segment.Init();
        }

        public static void PanGuParticiple(string text)
        {
            Segment segment = new Segment();
            MatchOptions options = new MatchOptions();
            MatchParameter parameters = new MatchParameter();
            //ICollection<WordInfo> words = segment.DoSegment(text, options, parameters);


            ICollection<WordInfo> words = new PanGuTokenizer().SegmentToWordInfos(text);

            foreach (WordInfo wi in words)
            {
                Console.WriteLine(wi.Word);
            }
        }

        public static int GetTotalPostsCont()
        {
            int count = 0;
            count = (int)SqlHelper.ExecuteScalar(FindPropertyDbConnectionString, 
                System.Data.CommandType.Text,
                "SELECT count(1) FROM pub.post",
                null);
            return count;
        }

        public static DataTable SelectPagedPosts(int pageIndex, int pageSize)
        {
            DataTable dt = null;
            DataSet ds=SqlHelper.ExecuteDataset(FindPropertyDbConnectionString,
                CommandType.Text,
                string.Format("SELECT TOP {0} rowId,refNo,Characteristic FROM (SELECT ROW_NUMBER() over(order BY CreateDate  DESC) rowId,* FROM pub.post a) D WHERE rowId >{0} *{1} ORDER BY CreateDate DESC", pageSize, pageIndex - 1),
                null);

            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public static void SearchFindPropertyPosts(string indexStorePath,string k)
        {
            Stopwatch sw = new Stopwatch();
            var luceneVer = Lucene.Net.Util.Version.LUCENE_29;
            Analyzer analyzer = new PanGuAnalyzer(true);
            DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(indexStorePath);
            Lucene.Net.Store.Directory indexDirectory = FSDirectory.Open(dirInfo);
            IndexSearcher search = new IndexSearcher(indexDirectory,true);
            QueryParser parser = new QueryParser(luceneVer, "info", analyzer);
            Query query = parser.Parse(k);

            sw.Start();
            Sort sort = new Sort(new SortField("index", SortField.INT, false));
            //Hits hits = search.Search(query,sort);

            TopDocs docs = search.Search(query, (Filter)null, 10000);
            ScoreDoc[] hits = docs.ScoreDocs;
            int count = hits.Length;
            sw.Stop();

            Console.WriteLine("搜索完成，找到{0}条记录,共耗时{1},", count, sw.Elapsed.TotalMilliseconds);

            for (var i = 0; i < count; i++)
            {
                //PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color='red'>", "</font>");
                ////创建高亮，输入HTML代码和 盘古对象Semgent    
                //PanGu.HighLight.Highlighter highter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());

                //Document doc = hits.Doc(i);
                //String title = highter.GetBestFragment(keyWord, doc.Get("PTitle"));
                //String context = highter.GetBestFragment(keyWord, doc.Get("PContext"));
                //sb.Append("标题：" + title + "<br/>" + context + doc.Get("PContext") + "<br/>");

                //Document doc = hits.Doc(i);//search.Doc(i);
                Document doc = search.Doc(hits[i].Doc);
                //Document doc = search.Doc(i);
                Console.WriteLine("\r\n第{0}条记录,refNo:{1}", i + 1, doc.Get("id"));
            }  

            search.Close(); 
        }

        public static void CreateFindPropertyPostIndex(string indexStorePath)
        {
            var ver = Lucene.Net.Util.Version.LUCENE_30;
            DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(indexStorePath);
            Lucene.Net.Store.Directory directory = FSDirectory.Open(dirInfo);
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.UNLIMITED);

            PagerInfo pageInfo=new PagerInfo();
            DataTable posts;
            Stopwatch sw = new Stopwatch();

            pageInfo.TotalItemCount = GetTotalPostsCont();
            pageInfo.PageSize = 5000;
            pageInfo.CurrentPageIndex = 1;

            Console.WriteLine("源数据共有{0}条，分为{1}页(每页{2}条数据)处理。", pageInfo.TotalItemCount, pageInfo.TotalPageCount, pageInfo.PageSize);
            try
            {
                for (int pageIndex = 1; pageIndex <= pageInfo.TotalPageCount; pageIndex++)
                {
                    Console.WriteLine("当前正在处理{0}/{1}页数据。", pageIndex,pageInfo.TotalPageCount);
                    sw.Start();
                    Stopwatch curSw = new Stopwatch();
                    curSw.Start();
                    posts = SelectPagedPosts(pageIndex, pageInfo.PageSize);
                    if (posts != null)
                    {
                        foreach (DataRow post in posts.Rows)
                        {
                            Document doc = new Document();
                            doc.Add(new Field("rowIndex", post["rowId"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("id", post["refNo"].ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("info", post["Characteristic"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                            writer.AddDocument(doc);
                        }
                    }
                    sw.Stop();
                    curSw.Stop();
                    Console.WriteLine("当前{0}/{1}页数据处理完毕，该页耗时{2},当前共耗时{3}。", pageIndex, pageInfo.TotalPageCount, curSw.Elapsed.TotalMilliseconds, sw.Elapsed.TotalMilliseconds);
                }
                Console.WriteLine("所有数据处理完毕，当前共耗时{0}。", sw.Elapsed.TotalMilliseconds);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (writer != null)
                {
                    writer.Commit();
                    writer.Optimize();
                    writer.Close();
                }
                if (directory != null)
                {
                    directory.Close();
                }
            }

        }


        

    }
}
