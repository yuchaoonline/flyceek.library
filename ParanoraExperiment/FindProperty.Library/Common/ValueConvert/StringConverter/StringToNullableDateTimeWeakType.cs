using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableDateTimeWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            DateTime? value = null;
            bool canConvert = true;
            canConvert = new StringToNullableDateTime().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
