using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.Interface;
using FindProperty.Lib.Common.ValueConvert.StringConverter;

namespace FindProperty.Lib.Common.ValueConvert
{
    public class ValueConverterFactoryCreator<TInput>
    {
        public static IValueConveterFactory<TInput> Create()
        {
            IValueConveterFactory<TInput> factory = null;

            if (typeof(TInput) == typeof(string))
            {
                factory = (IValueConveterFactory<TInput>)new StringConvertFactory();
            }

            return factory;
        }

        public static IValueConveterFactoryWeakType<TInput> CreateWeakType()
        {
            IValueConveterFactoryWeakType<TInput> factory = null;

            if (typeof(TInput) == typeof(string))
            {
                factory = (IValueConveterFactoryWeakType<TInput>)new StringConvertFactoryWeakType();
            }

            return factory;
        }
    }
}
