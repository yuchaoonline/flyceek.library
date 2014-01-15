using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Dto
{
    public interface IObjectMapperManager<S, T>
    {
        T Map(S obj);
    }
}
