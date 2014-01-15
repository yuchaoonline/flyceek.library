using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToBooleanWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            bool value = true;
            bool canConvert = true;
            canConvert =new StringToBoolean().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
