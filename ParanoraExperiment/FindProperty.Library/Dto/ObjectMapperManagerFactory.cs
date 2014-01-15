using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Dto
{
    public class ObjectMapperManagerFactory<S,T>:IObjectMapperManagerFactory<S,T>
    {
        private static IObjectMapperManager<S, T> _objectMapperManager;

        public IObjectMapperManager<S, T> Create(params object[] parameter)
        {
            if (_objectMapperManager == null)
            {
                _objectMapperManager=new EmitMapperFacade<S, T>();
            }
            return _objectMapperManager;
        }
    }
}
