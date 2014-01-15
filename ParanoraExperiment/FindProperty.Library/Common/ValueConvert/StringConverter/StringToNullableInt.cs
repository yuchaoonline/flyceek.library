using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableInt : Interface.IValueToNullableInt<string>
    {
        public bool Convert(string input, out int? result)
        {
            int value = 0;
            result = null;
            bool canConvert = new StringToInt().Convert(input, out value);
            if (canConvert)
            {
                result = value;
            }
            return canConvert;
        }
    }
}
