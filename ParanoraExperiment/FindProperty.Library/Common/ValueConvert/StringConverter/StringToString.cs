using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToString : Interface.IValueToString<string>
    {
        public bool Convert(string input, out string result)
        {
            bool canConvert = true;
            result = string.Empty;

            result = input;

            return canConvert;
        }
    }
}
