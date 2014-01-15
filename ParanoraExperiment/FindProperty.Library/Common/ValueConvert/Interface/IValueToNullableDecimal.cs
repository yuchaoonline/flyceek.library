using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.Interface
{
    public interface IValueToNullableDecimal<T>:IValueConverter<Nullable<decimal>,T>
    {
    }
}
