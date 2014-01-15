using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableIntWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            int? value = null;
            result = null;
            bool canConvert = new StringToNullableInt().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
