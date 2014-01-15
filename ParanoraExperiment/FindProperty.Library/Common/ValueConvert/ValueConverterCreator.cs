using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.Interface;

namespace FindProperty.Lib.Common.ValueConvert
{
    public class ValueConverterCreator<TInput>
    {
        public ValueConverterCreator(IValueConveterFactory<TInput> valueConvretFactory)
        {

        }

        public IValueConverter<TResult, TInput> Create<TResult>()
        {
            IValueConverter<TResult, TInput> result=null;

            return result;
        }

        public IValueConverter<object, TInput> CreateWeakType()
        {
            IValueConverter<object, TInput> result = null;

            return result;
        }
    }
}
