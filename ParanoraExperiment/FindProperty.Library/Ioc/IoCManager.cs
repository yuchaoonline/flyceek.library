using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FindProperty.Lib.Ioc
{
    public static class IoCManager
    {
        private static IIoCComponent _componet;

        public static IIoCComponent Componet()
        {
           return _componet; 
        }

        [DebuggerStepThrough]
        public static void Initialize(IIoCComponentFactory factory)
        {
            _componet = factory.Create();
        }

        [DebuggerStepThrough]
        public static void Register<T>(T instance)
        {
            _componet.Register(instance);
        }

        [DebuggerStepThrough]
        public static void Inject<T>(T existing)
        {

            _componet.Inject(existing);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type)
        {

            return _componet.Resolve<T>(type);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type, string name)
        {

            return _componet.Resolve<T>(type, name);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>()
        {
            return _componet.Resolve<T>();
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(string name)
        {

            return _componet.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _componet.ResolveAll<T>();
        }

        [DebuggerStepThrough]
        public static void Reset()
        {
            if (_componet != null)
            {
                _componet.Dispose();
            }
        }
    }

}
