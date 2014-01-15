using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace FindProperty.Lib.Dto
{
    public class EmitMapperFacade<S,T>:IObjectMapperManager<S,T>
    {

        public T Map(S obj)
        {
            //return ObjectMapperManager.DefaultInstance.GetMapper<S, T>().Map(obj);
            return ObjectMapperManager.DefaultInstance.GetMapper<S, T>(new DefaultMapConfig().NullSubstitution<string, string>(x => "").
               NullSubstitution<decimal?, decimal?>(x => 0)).Map(obj);
        }
    }
}
