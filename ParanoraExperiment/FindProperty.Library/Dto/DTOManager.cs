using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Dto
{
    public class DTOManager<S,T>
    {
        private static IObjectMapperManagerFactory<S, T> _IObjectMapperManagerFactory;

        public static void Init()
        {
            _IObjectMapperManagerFactory = new ObjectMapperManagerFactory<S, T>();
        }

        public static T Map(S obj)
        {
            if (_IObjectMapperManagerFactory == null)
            {
                Init();
            }

            return _IObjectMapperManagerFactory.Create().Map(obj);
        }
    }
}
