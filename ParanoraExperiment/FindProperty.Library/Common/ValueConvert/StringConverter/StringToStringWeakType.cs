using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToStringWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            string value = string.Empty;
            bool canConvert = true;
            canConvert = new StringToString().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
