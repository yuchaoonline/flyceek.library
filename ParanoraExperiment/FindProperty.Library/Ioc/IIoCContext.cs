using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Ioc
{
    public interface IIoCContext
    {
        IIoCComponent Componet { get; }

        IIoCContext Context { get; }

        IIoCCreator Creator { get; }

        T Kernel<T>();
    }
}
