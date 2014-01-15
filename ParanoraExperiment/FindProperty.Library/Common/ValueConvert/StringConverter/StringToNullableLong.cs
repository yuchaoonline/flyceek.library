using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableLong : Interface.IValueToNullableLong<string>
    {
        public bool Convert(string input, out long? result)
        {
            long value = 0;
            result = null;
            bool canConvert = new StringToLong().Convert(input, out value);
            if (canConvert)
            {
                result = value;
            }
            return canConvert;
        }
    }
}
