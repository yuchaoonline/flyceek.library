using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Aop
{
    public abstract class InjectionContext
    {
        public object Method { get; set; }
        public object MethodReturn { get; set; }
        public object NextMethodDelegate { get; set; }
        public IDictionary<object, object> Properties { get; set; }
    }

    public interface IInjectionCreator
    {
        TInterface Wrap<TInterface>(object instance);

        object Wrap(Type typeToReturn, object instance);

        TInterface Create<TObject, TInterface>(params object[] args) where TObject : TInterface;

        TObject Create<TObject>(params object[] args);

        object Create(Type typeToCreate, params object[] args);

        object Create(Type typeToCreate, Type typeToReturn, params object[] args);
    }

    public interface IInjectionCallHandler
    {
        void BeforeProcess(InjectionContext context);
        object Process(InjectionContext context);
        void AfterProcess(InjectionContext context);
    }
}
