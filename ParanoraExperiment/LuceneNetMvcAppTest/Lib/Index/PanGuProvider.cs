using Lucene.Net.Analysis.PanGu;
using PanGu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LuceneNetMvcAppTest.Lib.Index
{
    public class PanGuProvider
    {
        public static void PanGuInit(string configPath)
        {
            PanGu.Segment.Init(configPath);
        }

        public static string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(5, word.Rank));
                //result.AppendFormat("{0}^{1}.0 ", word.Word, 1);
            }

            return result.ToString().Trim();
        }

        public static string HighLightContent(string k, string content,string startHtmlTag,string endHtmlTag)
        {
            string highLightHtml = string.Empty;

            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter(startHtmlTag, endHtmlTag);
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 5000;

            highLightHtml = highlighter.GetBestFragment(k, content);
            if (string.IsNullOrEmpty(highLightHtml))
            {
                highLightHtml = content;
            }

            return highLightHtml;
        }
    }
}