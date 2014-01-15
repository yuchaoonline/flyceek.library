using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToDecimalWeakType:Interface.IValueToObject<string>
    {

        public bool Convert(string input, out object result)
        {
            result = null;
            decimal value = 0;
            bool canConvert = true;
            canConvert = new StringToDecimal().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
