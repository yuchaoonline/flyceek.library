using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToDecimal : Interface.IValueToDecimal<string>
    {
        public bool Convert(string input, out decimal result)
        {
            bool canConvert = true;
            result = 0;
            if (!string.IsNullOrEmpty(input) && decimal.TryParse(input, out result))
            {

            }
            else
            {
                result = 0;
                canConvert = false;
            }
            return canConvert;
        }
    }
}
