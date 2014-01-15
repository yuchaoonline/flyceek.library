using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.Interface
{
    public interface IValueConveterFactory<TInput>
    {
        IValueToString<TInput> ValueToString { get; }
        
        IValueToBoolean<TInput> ValueToBoolean { get; }

        IValueToDateTime<TInput> ValueToDateTime { get; }

        IValueToDecimal<TInput> ValueToDecimal { get; }

        IValueToDouble<TInput> ValueToDouble { get; }

        IValueToGuid<TInput> ValueToGuid { get; }

        IValueToInt<TInput> ValueToInt { get; }

        IValueToLong<TInput> ValueToLong { get; }

        IValueToNullableDateTime<TInput> ValueToNullableDateTime { get; }

        IValueToNullableDecimal<TInput> ValueToNullableDecimal { get; }

        IValueToNullableDouble<TInput> ValueToNullableDouble { get; }

        IValueToNullableInt<TInput> ValueToNullableInt { get; }

        IValueToNullableLong<TInput> ValueToNullableLong { get; }
    }
}
