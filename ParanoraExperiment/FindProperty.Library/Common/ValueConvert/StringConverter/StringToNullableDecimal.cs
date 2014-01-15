using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableDecimal : Interface.IValueToNullableDecimal<string>
    {
        public bool Convert(string input, out decimal? result)
        {
            decimal value = 0;
            result = null;
            bool canConvert = new StringToDecimal().Convert(input, out value);
            if (canConvert)
            {
                result = value;
            }
            return canConvert;
        }
    }
}
