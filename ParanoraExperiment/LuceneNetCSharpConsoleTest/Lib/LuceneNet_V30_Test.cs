using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneNetCSharpConsoleTest.Lib
{
    //public class LuceneNet_V30_Test
    //{
    //    static void TestFun()
    //    {
    //        Directory indexDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(INDEX_STORE_PATH));
    //        bool isCreate = !Lucene.Net.Index.IndexReader.IndexExists(indexDirectory);
    //        Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
    //        IndexWriter writer = new IndexWriter(indexDirectory, analyzer, isCreate, IndexWriter.MaxFieldLength.UNLIMITED);

    //        try
    //        {
    //            Document doc = new Document();
    //            doc.Add(new Field("id", "leng0", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO));
    //            doc.Add(new Field("info", "i love you every day! and you love me?", Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.NO));
    //            writer.AddDocument(doc);

    //            doc = new Document();
    //            doc.Add(new Field("id", "leng1", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
    //            doc.Add(new Field("info", "i love you every day", Field.Store.YES, Field.Index.ANALYZED));
    //            writer.AddDocument(doc);
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //            if (analyzer != null)
    //            {
    //                analyzer.Close();
    //            }
    //            if (writer != null)
    //            {
    //                writer.Close();
    //            }
    //            if (indexDirectory != null)
    //            {
    //                indexDirectory.Dispose();
    //            }
    //        }


    //        RAMDirectory ramDir = new RAMDirectory();
    //        IndexWriter writerRam = new IndexWriter(ramDir, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), true, IndexWriter.MaxFieldLength.LIMITED);
    //        writerRam.AddIndexes(IndexReader.Open(indexDirectory, false));
    //        writerRam.AddIndexes(indexDirectory);

    //        IndexSearcher isearcher = new IndexSearcher(ramDir, true);
    //        QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "info", analyzer);
    //        Query query = parser.Parse("love");

    //        ScoreDoc[] hits = isearcher.Search(query, null, 1000).ScoreDocs;

    //        for (int i = 0; i < hits.Length; i++)
    //        {
    //            Document hitDoc = isearcher.Doc(hits[i].Doc);
    //            Console.WriteLine(hitDoc.Get("info") + " : This is the text to be indexed.");
    //        }
    //        isearcher.Dispose();
    //        indexDirectory.Dispose();
    //        if (analyzer != null)
    //        {
    //            analyzer.Close();
    //        }
    //    }

    //    public List<Article> Search(string k, string cid)
    //    {
    //        Stopwatch st = new Stopwatch();
    //        st.Start();//计时开始  
    //        为索引存储目录

    //        var ver = Lucene.Net.Util.Version.LUCENE_29;
    //        Directory indexDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(INDEX_STORE_PATH));
    //        Analyzer analyzer = new StandardAnalyzer(ver);
    //        IndexSearcher searcher = null;
    //        List<Article> list;
    //        int recCount = 0;
    //        try
    //        {
    //            searcher = new IndexSearcher(indexDirectory, true);
    //            string[] fields = { "title", "summary" };
    //            BooleanQuery booleanQuery = new BooleanQuery();
    //            多字段查询同时搜索title和summary
    //            MultiFieldQueryParser parser = new MultiFieldQueryParser(ver, fields, analyzer);
    //            Query query = parser.Parse(k);
    //            Query query1 = new QueryParser(ver, "classid", analyzer).Parse("1");

    //            TermQuery只能查询不分词的索引(Field.Index.NOT_ANALYZED)
    //            Query query1 = new TermQuery(new Term("id", "1"));
    //            当classname为ANALYZED时搜不到
    //             Query query2 = new TermQuery(new Term("classname", "体育新闻"));
    //            只有当classname为NOT_ANALYZED才可以搜得到，
    //            由此得出TermQuery只能查询不分词的索引(Field.Index.NOT_ANALYZED)的结论
    //            但当id为ANALYZED时TermQuery却可以收的到,
    //            当搜classname中包含“体”时即Query query2 = new TermQuery(new Term("classname", "体"));
    //            当搜classname中包含“育”时即Query query2 = new TermQuery(new Term("classname", "育"));
    //            可以搜得到。因此，由此得出，TermQuery搜的是最小单位，由此又得出Lucene是把“体育新闻”拆分成了"体/育/新/闻"四部分
    //            听说Lucene分词是按空格分的，那么把“体育新闻”,改成“体育 新闻”后再重新生成索引是不是可以搜的到呢？
    //            Query query2 = new TermQuery(new Term("classname", "体育"));
    //            但是结果却是搜不到，纳闷...难道Lucene的分词不是这么分而是更复杂？
    //            StandardAnalyzer看来是对中文分词不怎么好,当ClassName = "sports news"可以搜sports和news
    //            StandardAnalyzer只支持英文的空格分词
    //            Query query2 = new TermQuery(new Term("classname", k));
    //            关于QueryParser的搜索当k为Empty或null时会报错注意处理
    //            Query query3 = new QueryParser(ver, "title", analyzer).Parse(k);
    //            Query query3 = new QueryParser(ver, "title", analyzer).Parse(k);

    //            Query query4 = new PrefixQuery(new Term("classname", k));
    //            Query query5 = new QueryParser(ver, "title", analyzer).Parse(k);
    //            TermRangeQuery query6 = new TermRangeQuery("createtime", "2012-1-3", "2012-5-3", true, true);


    //            booleanQuery.Add(query1, BooleanClause.Occur.MUST);
    //            booleanQuery.Add(query2, BooleanClause.Occur.MUST);
    //            booleanQuery.Add(query3, BooleanClause.Occur.MUST);
    //            booleanQuery.Add(query4, BooleanClause.Occur.MUST);
    //            booleanQuery.Add(query5, BooleanClause.Occur.MUST);
    //            booleanQuery.Add(query6, BooleanClause.Occur.MUST);
    //            TopDocs ts = searcher.Search(booleanQuery, null, 100);//执行搜索，获取查询结果集对象

    //            recCount = ts.TotalHits;//获取命中的文档个数
    //            ScoreDoc[] hits = ts.ScoreDocs;//获取命中的文档信息对象
    //            st.Stop();//计时停止
    //            Console.WriteLine(string.Format("{0}毫秒,生成的Query语句:{1}", st.ElapsedMilliseconds, booleanQuery.ToString()));

    //            list = new List<Article>();
    //            foreach (var item in hits)
    //            {
    //                list.Add(new Article()
    //                {
    //                    Id = searcher.Doc(item.Doc).Get("id"),
    //                    ClassId = searcher.Doc(item.Doc).Get("classid"),
    //                    ClassName = searcher.Doc(item.Doc).Get("classname"),
    //                    Title = searcher.Doc(item.Doc).Get("title"),
    //                    Summary = searcher.Doc(item.Doc).Get("summary"),
    //                    Score = item.Score.ToString(),
    //                    CreateTime = DateTime.Parse(searcher.Doc(item.Doc).Get("createtime"))
    //                });
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            if (searcher != null)
    //            {
    //                searcher.Close();
    //            }
    //        }

    //        return list;
    //    }

    //    static void AddIndex()
    //    {
    //        if (System.IO.Directory.Exists(INDEX_STORE_PATH))
    //        {
    //            System.IO.Directory.Delete(INDEX_STORE_PATH, true);
    //        }

    //        Directory indexDirectory = FSDirectory.Open(new System.IO.DirectoryInfo(INDEX_STORE_PATH));
    //        Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
    //        IndexWriter writer = null;

    //        try
    //        {
    //            检查索引文件是否存在
    //            bool iscreate = !Lucene.Net.Index.IndexReader.IndexExists(indexDirectory);
    //            如果索引文件不存在则创建索引文件，否则创建索引文件
    //            writer = new IndexWriter(indexDirectory, analyzer, iscreate, IndexWriter.MaxFieldLength.UNLIMITED);

    //            开始添加索引
    //            foreach (var item in Get())
    //            {
    //                Document doc = new Document();
    //                doc.Add(new Field("id", item.Id, Field.Store.YES, Field.Index.ANALYZED));//存储,分词索引
    //                doc.Add(new Field("classid", item.ClassId, Field.Store.YES, Field.Index.NOT_ANALYZED));//存储,不分词索引
    //                doc.Add(new Field("classname", item.ClassName, Field.Store.YES, Field.Index.ANALYZED));//存储,分词索引
    //                doc.Add(new Field("title", item.Title, Field.Store.YES, Field.Index.ANALYZED));//存储,分词索引
    //                doc.Add(new Field("summary", item.Summary, Field.Store.YES, Field.Index.ANALYZED));//存储,分词索引
    //                doc.Add(new Field("createtime", item.CreateTime.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//存储,分词索引
    //                writer.AddDocument(doc);
    //            }
    //            writer.Optimize();
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            if (analyzer != null)
    //            {
    //                analyzer.Close();
    //            }
    //            if (writer != null)
    //            {
    //                writer.Close();
    //            }
    //            if (indexDirectory != null)
    //            {
    //                indexDirectory.Dispose();
    //            }
    //        }

    //    }

    //    public static List<Article> Get()
    //    {
    //        List<Article> list = new List<Article>();
    //        list.Add(new Article() { Id = "1", ClassId = "1", ClassName = "体育新闻", Title = "微软发布MVC4.0了", Summary = "微软发布MVC4.0了，此版本更加强大", CreateTime = DateTime.Parse("2012-2-3") });
    //        list.Add(new Article() { Id = "2", ClassId = "1", ClassName = "IT新闻", Title = "跟谷歌测试工程师的对话", Summary = "本文主人公Alan是谷歌的一名的软件测试工程师，他的工作对象是谷歌的DoubleClick广告管理系统(Bid Manager)，这个系统提供让广告代理商和广告客户在多个广告上进行报价竞标的功能。", CreateTime = DateTime.Parse("2012-3-3") });
    //        list.Add(new Article() { Id = "3", ClassId = "1", ClassName = "体育 新闻", Title = "好的程序员应该熟悉的几门编程语言", Summary = "如果想成为一个好的程序员，甚至架构师、技术总监等，显然只精通一种编程语言是不够的，还应该在常见领域学会几门编程语言，正如我们要成为高级人才不仅要会中文还要会英文", CreateTime = DateTime.Parse("2012-4-3") });
    //        list.Add(new Article() { Id = "4", ClassId = "2", ClassName = "娱乐新闻", Title = "Javascript开发《三国志曹操传》-开源讲座(五)-可移动地图的实现", Summary = "这一讲的内容很简单，大家理解起来会更快。因此我只对重点加以分析，其他的就轮到大家思考哦！首先来说，我对游戏开发可以算是不怎么深入，因为现在的程序员爱用canvas，我却就只会拿几个div凑和。", CreateTime = DateTime.Parse("2012-5-3") });
    //        list.Add(new Article() { Id = "5", ClassId = "2", ClassName = "体育新闻", Title = "Android之BaseExpandableListAdapter使用心得", Summary = " 但是我最近做那个QQ项目是遇到一个问题，如果给这个ExpandableListView添加动态从网上获取的数据呢？前面跟大家分享的时候，是用了静态的数据，很好处理。", CreateTime = DateTime.Parse("2012-6-3") });
    //        list.Add(new Article() { Id = "6", ClassId = "3", ClassName = "sports news", Title = "对话CSDN蒋涛：微软移动互联网马太效应不可避免，小团队需学会利用平台", Summary = "CSDN是全球最大的中文IT社区，也是雷锋网最重要的合作伙伴之一，自1999年创办至今，有着非常强大的业界影响力和号召力，其专注IT信息传播、技术交流、教育培训和专业技术人才服务，在2012年移动开发者大会即将举办之际，雷锋网对CSDN的掌门人蒋涛做了一次专访，一起探讨移动互联网的新技术浪潮和下一波发展趋势。", CreateTime = DateTime.Parse("2012-7-3") });
    //        list.Add(new Article() { Id = "7", ClassId = "3", ClassName = "体育新闻", Title = "基于MySQL的分布式事务控制方案", Summary = "基于MySQL的分布式事务控制方案", CreateTime = DateTime.Parse("2012-8-3") });
    //        list.Add(new Article() { Id = "8", ClassId = "4", ClassName = "sports news", Title = "IOS和Android开发的一些个人感受", Summary = "最近公司的产品 Android版本第二版也算到了收尾，新加了几个功能性模块，我基本也就捡了几个好玩的模块做了下。", CreateTime = DateTime.Parse("2012-9-3") });
    //        list.Add(new Article() { Id = "9", ClassId = "5", ClassName = "IT资讯", Title = "Google Code的简单使用", Summary = "google code简介：用于管理代码的仓库，反正我是这么理解的。就比我们在公司的时候也会有个用于存放公司代码的主机一样，google同样给我们提供了这样的一个host。这样我们可以在不同电脑不同地方随时的checkout，commit，同时分享我们的项目。", CreateTime = DateTime.Parse("2012-10-3") });
    //        list.Add(new Article() { Id = "10", ClassId = "33", ClassName = "IT资讯", Title = "谷歌在印度推Gmail免费短信服务", Summary = "歌一直在努力桥接发展中国家功能手机SMS服务和Gmail之间的服务，这不，近日谷歌在印度推出“Gmail SMS”服务，这使得印度的Gmail用户可以从Gmail的窗口发送信息到手机上并且接受聊天信息的回复，目前谷歌的这项服务已经得到印度的八大运营商的支持。", CreateTime = DateTime.Parse("2012-11-3") });
    //        list.Add(new Article() { Id = "11", ClassId = "11", ClassName = "体育新闻", Title = "鲍尔默：微软新时代 软硬结合“赢”未来", Summary = "微软CEO鲍尔默在年度公开信中表示，微软在未来将紧密结合硬件和软件。鲍尔默认为，这是微软的一个新时代。“我们看到了前所未有的机会，我们对此很兴奋，并且保持着乐观的心态。”", CreateTime = DateTime.Parse("2012-12-3") });
    //        return list;
    //    }
    //}
}
