using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.Interface;

namespace FindProperty.Lib.Common.ValueConvert.StringConverter
{
    public class StringConvertFactory : IValueConveterFactory<string>
    {
        public IValueToBoolean<string> ValueToBoolean
        {
            get { return new StringToBoolean(); }
        }

        public IValueToDateTime<string> ValueToDateTime
        {
            get { return new StringToDateTime(); }
        }

        public IValueToDecimal<string> ValueToDecimal
        {
            get { return new StringToDecimal(); }
        }

        public IValueToDouble<string> ValueToDouble
        {
            get { return new StringToDouble(); }
        }

        public IValueToGuid<string> ValueToGuid
        {
            get { return new StringToGuid(); }
        }

        public IValueToInt<string> ValueToInt
        {
            get { return new StringToInt(); }
        }

        public IValueToLong<string> ValueToLong
        {
            get { return new StringToLong(); }
        }

        public IValueToNullableDateTime<string> ValueToNullableDateTime
        {
            get { return new StringToNullableDateTime(); }
        }

        public IValueToNullableDecimal<string> ValueToNullableDecimal
        {
            get { return new StringToNullableDecimal(); }
        }

        public IValueToNullableDouble<string> ValueToNullableDouble
        {
            get { return new StringToNullableDouble(); }
        }

        public IValueToNullableInt<string> ValueToNullableInt
        {
            get { return new StringToNullableInt(); }
        }

        public IValueToNullableLong<string> ValueToNullableLong
        {
            get { return new StringToNullableLong(); }
        }

        public IValueToString<string> ValueToString
        {
            get { return new StringToString(); }
        }
    }
}
