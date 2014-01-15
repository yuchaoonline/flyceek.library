using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common.ValueConvert.Interface
{
    public interface IDataTableToListModelConverter<T> : IObjectConverter<List<T>, DataTable> where T:class,new()
    {
    }
}
