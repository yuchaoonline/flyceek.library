using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToGuidWeakType:Interface.IValueToObject<string>
    {
        public bool Convert(string input, out object result)
        {
            result = null;
            Guid value = Guid.Empty;
            bool canConvert = true;
            canConvert = new StringToGuid().Convert(input, out value);
            result = value;
            return canConvert;
        }
    }
}
