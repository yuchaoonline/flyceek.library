using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Dto
{
    public interface IObjectMapperManagerFactory<S,T>
    {
        IObjectMapperManager<S, T> Create(params object[] parameter);
    }
}
