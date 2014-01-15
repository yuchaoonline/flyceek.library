using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableDateTime : Interface.IValueToNullableDateTime<string>
    {
        public bool Convert(string input, out DateTime? result)
        {
            DateTime time=new DateTime(1900,1,1);
            result = null;
            bool canConvert = new StringToDateTime().Convert(input, out time);
            if (canConvert)
            {
                result = time;
            }
            return canConvert;
        }
    }
}
