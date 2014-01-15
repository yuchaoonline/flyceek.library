using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using FindProperty.Lib.Ioc.Unity;

namespace FindProperty.Lib.Ioc
{
    public class IoCComponentFactoryBase : IIoCComponentFactory
    {
        private readonly Type _resolverType;

        public IoCComponentFactoryBase(string resolverTypeName)
        {
             _resolverType=Type.GetType(resolverTypeName, true, true); 
        }

        public IIoCComponent Create(params object[] parameter)
        {
            return Activator.CreateInstance(_resolverType) as UnityComponent;
        }
    }
}
