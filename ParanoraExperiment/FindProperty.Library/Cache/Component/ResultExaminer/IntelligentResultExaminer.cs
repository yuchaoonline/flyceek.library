using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.ResultExaminer
{
    public class IntelligentResultExaminer : IResultExaminer
    {
        public bool IsNullOrEmpty(object v)
        {
            bool check = false;

            string tag = v.GetType().Name;
            if (tag.ToLower() == "string")
            {
                return string.IsNullOrEmpty(v.ToString());
            }
            else if (tag.ToLower() == "int64")
            {
                return ((Int64)v == 0);
            }
            string vNs = v.GetType().Namespace;

            if (vNs == typeof(int).Namespace)
            {
                check = ((int)v == 0);
            }
            else if (vNs == typeof(List<int>).Namespace)
            {
                check = (v as ICollection).Count < 1;
            }

            else
            {
                check = (v == null);
            }

            return check;
        }
    }
}
