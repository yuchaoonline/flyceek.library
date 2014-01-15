using LuceneNetMvcAppTest.Lib.Common;
using LuceneNetMvcAppTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneNetMvcAppTest.ViewModels
{
    public class PropertyLuceneSearch
    {
        public List<PropertyLuceneIndexDocument> PropertyLuceneIndexDocuments { get; set; }
        public double SearchTimeConsuming { get; set; }
        public PagerInfo PagerInfo { get; set; }
        public string KeyWord { get; set; }
    }
}