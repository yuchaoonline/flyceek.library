﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringToNullableDouble : Interface.IValueToNullableDouble<string>
    {
        public bool Convert(string input, out double? result)
        {
            double value = 0;
            result = null;
            bool canConvert = new StringToDouble().Convert(input, out value);
            if (canConvert)
            {
                result = value;
            }
            return canConvert;
        }
    }
}
