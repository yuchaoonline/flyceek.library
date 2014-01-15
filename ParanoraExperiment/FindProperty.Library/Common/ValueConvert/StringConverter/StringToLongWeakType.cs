using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToLongWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            long value = 0;
            bool canConvert = true;
            canConvert = new StringToLong().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
