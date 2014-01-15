using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common
{
    public class MethodCommon
    {
        public static bool IsNullOrEmpty(string str)
        {
            bool result = false;

            result = string.IsNullOrEmpty(str);

            if (!result&&str.Trim().Length < 1)
            {
                result = true;
            }

            return result;
        }
    }
}
