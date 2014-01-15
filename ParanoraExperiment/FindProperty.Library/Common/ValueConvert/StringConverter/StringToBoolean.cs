using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToBoolean : Interface.IValueToBoolean<string>
    {
        public bool Convert(string input, out bool result)
        {
            bool canConvert = true;
            result = true;
            if (!string.IsNullOrEmpty(input) &&
                (input == "1" || input.ToUpper() == "TRUE"))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return canConvert;
        }
    }
}
