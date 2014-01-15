using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Ioc
{
    public interface IIoCCreator
    {
        T Create<T>(params object[] parameter);

        T Create<T>(string configFilePath, params object[] parameter);
    }
}
