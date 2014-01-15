using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToDateTime : Interface.IValueToDateTime<string>
    {
        public bool Convert(string input, out DateTime result)
        {
            bool canConvert = true;
            result = new DateTime(1900, 1, 1);
            if (!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out result))
            {
                ;
            }
            else
            {
                result = new DateTime(1900, 1, 1);
                canConvert = false;
            }

            return canConvert;
        }
    }
}
