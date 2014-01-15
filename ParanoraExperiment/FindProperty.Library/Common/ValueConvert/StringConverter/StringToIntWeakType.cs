using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToIntWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            int value = 0;
            bool canConvert = true;
            canConvert = new StringToInt().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
