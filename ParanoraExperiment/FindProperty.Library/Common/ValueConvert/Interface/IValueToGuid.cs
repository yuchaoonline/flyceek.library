using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.Interface
{
    public interface IValueToGuid<T>:IValueConverter<Guid,T>
    {
    }
}
