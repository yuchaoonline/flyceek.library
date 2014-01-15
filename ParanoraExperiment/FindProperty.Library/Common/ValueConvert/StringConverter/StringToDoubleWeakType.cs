using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToDoubleWeakType:Interface.IValueToObject<string>
    {

        public bool Convert(string input, out object result)
        {
            result = null;
            double value = 0;
            bool canConvert = true;
            canConvert = new StringToDouble().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
