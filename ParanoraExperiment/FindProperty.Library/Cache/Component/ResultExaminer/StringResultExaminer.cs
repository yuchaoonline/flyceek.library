using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.ResultExaminer
{
    public class StringObjectValueExaminer : IResultExaminer
    {
        public bool IsNullOrEmpty(object v)
        {
            return string.IsNullOrEmpty(v.ToString());
        }
    }
}
