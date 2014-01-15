using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableLongWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            long? value = null;
            bool canConvert = true;
            canConvert = new StringToNullableLong().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
