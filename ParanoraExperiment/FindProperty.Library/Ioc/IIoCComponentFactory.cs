using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.Ioc
{
    public interface IIoCComponentFactory
    {
        IIoCComponent Create(params object[] parameter);
    }
}
