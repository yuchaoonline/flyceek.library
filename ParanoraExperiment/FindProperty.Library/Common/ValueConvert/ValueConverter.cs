using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.Interface;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.Common.ValueConvert
{
    public class ValueConverter<TInput>
    {
        private IValueConveterFactory<TInput> _valueConvretFactory;

        public ValueConverter(IValueConveterFactory<TInput> valueConvretFactory)
        {
            _valueConvretFactory = valueConvretFactory;
        }

        public ValueConverter():this(ValueConverterFactoryCreator<TInput>.Create())
        {
        }

        public TResult Convert<TResult>(TInput inputValue, Type inputType,Type resultType)
        {
            TResult result = (TResult)Convert(inputValue, inputType);
            return result;
        }

        public object Convert(TInput inputValue, Type inputType)
        {
            object result = null;

            if (inputType.FullName == typeof(bool).FullName)
            {
                bool value=true;
                if (_valueConvretFactory.ValueToBoolean.Convert(inputValue,out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(decimal).FullName)
            {
                decimal value = 0;
                if (_valueConvretFactory.ValueToDecimal.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(string).FullName)
            {
                string value = string.Empty;
                if (_valueConvretFactory.ValueToString.Convert(inputValue, out value))
                {
                    result = value.Trim();
                }
            }
            else if (inputType.FullName == typeof(Guid).FullName)
            {
                Guid value = Guid.Empty;
                if (_valueConvretFactory.ValueToGuid.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(int).FullName)
            {
                int value = 0;
                if (_valueConvretFactory.ValueToInt.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(double).FullName)
            {
                double value = 0;
                if (_valueConvretFactory.ValueToDouble.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(long).FullName)
            {
                long value = 0;
                if (_valueConvretFactory.ValueToLong.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(DateTime).FullName)
            {
                DateTime value = new DateTime(1970, 1, 1);
                if (_valueConvretFactory.ValueToDateTime.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(Nullable<DateTime>).FullName)
            {
                Nullable<DateTime> value = null;
                if (_valueConvretFactory.ValueToNullableDateTime.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(Nullable<int>).FullName)
            {
                Nullable<int> value = null;
                if (_valueConvretFactory.ValueToNullableInt.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(Nullable<decimal>).FullName)
            {
                Nullable<decimal> value = null;
                if (_valueConvretFactory.ValueToNullableDecimal.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(Nullable<long>).FullName)
            {
                Nullable<long> value = null;
                if (_valueConvretFactory.ValueToNullableLong.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else if (inputType.FullName == typeof(Nullable<double>).FullName)
            {
                Nullable<double> value = null;
                if (_valueConvretFactory.ValueToNullableDouble.Convert(inputValue, out value))
                {
                    result = value;
                }
            }
            else
            {
            }

            return result;
        }
    }
}
