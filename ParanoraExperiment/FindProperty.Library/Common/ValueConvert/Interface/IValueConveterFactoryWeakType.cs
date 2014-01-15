using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.Interface
{
    public interface IValueConveterFactoryWeakType<TInput>
    {
        IValueToObject<TInput> ValueToString { get; }

        IValueToObject<TInput> ValueToBoolean { get; }

        IValueToObject<TInput> ValueToDateTime { get; }

        IValueToObject<TInput> ValueToDecimal { get; }

        IValueToObject<TInput> ValueToDouble { get; }

        IValueToObject<TInput> ValueToGuid { get; }

        IValueToObject<TInput> ValueToInt { get; }

        IValueToObject<TInput> ValueToLong { get; }

        IValueToObject<TInput> ValueToNullableDateTime { get; }

        IValueToObject<TInput> ValueToNullableDecimal { get; }

        IValueToObject<TInput> ValueToNullableDouble { get; }

        IValueToObject<TInput> ValueToNullableInt { get; }

        IValueToObject<TInput> ValueToNullableLong { get; }
    }
}
