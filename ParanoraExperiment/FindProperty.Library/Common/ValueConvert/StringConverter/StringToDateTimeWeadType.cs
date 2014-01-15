using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToDateTimeWeadType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            DateTime value = new DateTime(1900,1,1);
            bool canConvert = true;
            canConvert = new StringToDateTime().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
