using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class Criteria 
    {
        public string ID { get; set; }

        public decimal Max { get; set; }

        public decimal Min { get; set; }

        public string Value { get; set; }

        public string DisplayString { get; set; }
        /// <summary>
        /// 1: range value
        /// 2: string value
        /// </summary>
        public int Type { get; set; }

        public object Order { get; set; }

        public object OrderBy { get; set; }

        public object Where { get; set; }

        public string Name { set; get; }

        public string GroupName { get; set; }

        public object WParam { get; set; }

        public object LParam { get; set; }

        public Criteria()
        {
            WParam = null;
            LParam = null;
        }
    }
}
