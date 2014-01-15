using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToLong : Interface.IValueToLong<string>
    {
        public bool Convert(string input, out long result)
        {
            bool canConvert = true;
            result = 0;
            if (!string.IsNullOrEmpty(input) && long.TryParse(input, out result))
            {

            }
            else
            {
                result = 0;
                canConvert = false;
            }
            return canConvert;
        }
    }
}
