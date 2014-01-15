using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringConvertFactoryWeakType:Interface.IValueConveterFactoryWeakType<string>
    {
        public Interface.IValueToObject<string> ValueToString
        {
            get { return new StringToStringWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToBoolean
        {
            get { return new StringToBooleanWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToDateTime
        {
            get { return new StringToDateTimeWeadType(); }
        }

        public Interface.IValueToObject<string> ValueToDecimal
        {
            get { return new StringToDecimalWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToDouble
        {
            get { return new StringToDoubleWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToGuid
        {
            get { return new StringToGuidWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToInt
        {
            get { return new StringToIntWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToLong
        {
            get { return new StringToLongWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToNullableDateTime
        {
            get { return new StringToNullableDateTimeWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToNullableDecimal
        {
            get { return new StringToNullableDecimalWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToNullableDouble
        {
            get { return new StringToNullableDoubleWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToNullableInt
        {
            get { return new StringToNullableIntWeakType(); }
        }

        public Interface.IValueToObject<string> ValueToNullableLong
        {
            get { return new StringToNullableLongWeakType(); }
        }
    }
}
