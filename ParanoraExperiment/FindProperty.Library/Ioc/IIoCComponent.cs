using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.Ioc
{
    public interface IIoCComponent : IDisposable
    {
        IIoCContext Context { get; }

        void Register<T>(T instance);

        void Inject<T>(T existing);

        T Resolve<T>(Type type);

        T Resolve<T>(Type type, string name);

        T Resolve<T>();

        T Resolve<T>(string name);

        IEnumerable<T> ResolveAll<T>();
    }

}
