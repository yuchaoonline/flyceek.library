using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToGuid : Interface.IValueToGuid<string>
    {
        public bool Convert(string input, out Guid result)
        {
            bool canConvert = true;
            result = Guid.Empty;
            if (!string.IsNullOrEmpty(input) && Guid.TryParse(input, out result))
            {

            }
            else
            {
                result = Guid.Empty;
                canConvert = false;
            }
            return canConvert;
        }
    }
}
