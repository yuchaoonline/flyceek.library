using LuceneNetMvcAppTest.Lib.Common;
using LuceneNetMvcAppTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneNetMvcAppTest.ViewModels
{
    public class PropertySearch
    {
        public List<Property> Propertys { get; set; }
        public double SearchTimeConsuming { get; set; }
        public PagerInfo PagerInfo { get; set; }
        public string KeyWord { get; set; }
    }
}