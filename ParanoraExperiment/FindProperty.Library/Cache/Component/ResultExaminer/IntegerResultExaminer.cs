using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.ResultExaminer
{
    public class IntegerResultExaminer : IResultExaminer
    {
        public bool IsNullOrEmpty(object v)
        {
            bool result = true;
            int integerValue = 0;
            result = int.TryParse(v.ToString(), out integerValue);
            return !result;
        }
    }
}
