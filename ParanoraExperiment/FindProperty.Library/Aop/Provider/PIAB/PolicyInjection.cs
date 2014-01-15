using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FindProperty.Lib.Aop.Provider.PIAB
{
    public abstract class PolicyInjectionHandlerAttribute : Microsoft.Practices.Unity.InterceptionExtension.HandlerAttribute
    {
    }

    public interface IPolicyInjectionCallHandler : IInjectionCallHandler, Microsoft.Practices.Unity.InterceptionExtension.ICallHandler
    {

    }

    public abstract class PolicyInjectionCallHandler : IPolicyInjectionCallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            PolicyInjectionContext context = new PolicyInjectionContext();
            context.MethodReturn = null;
            context.Method = input;
            context.NextMethodDelegate = getNext;

            BeforeProcess(context);
            context.MethodReturn = Process(context) as IMethodReturn;
            AfterProcess(context);

            return context.MethodReturn as IMethodReturn;
        }

        public int Order
        {
            get;
            set;
        }

        public abstract bool IsProcess(IMethodInvocation input, out IMethodReturn returnValue);

        public abstract void BeforeProcess(InjectionContext context);

        public abstract void AfterProcess(InjectionContext context);

        public abstract object Process(InjectionContext context);
    }

    public class PolicyInjectionCreateor : IInjectionCreator
    {
        public TInterface Wrap<TInterface>(object instance)
        {
            return PolicyInjection.Wrap<TInterface>(instance);
        }

        public object Wrap(Type typeToReturn, object instance)
        {
            return PolicyInjection.Wrap(typeToReturn, instance);
        }

        public TInterface Create<TObject, TInterface>(params object[] args) where TObject : TInterface
        {
            return PolicyInjection.Create<TObject, TInterface>(args);
        }

        public TObject Create<TObject>(params object[] args)
        {
            return PolicyInjection.Create<TObject>(args);
        }

        public object Create(Type typeToCreate, params object[] args)
        {
            return PolicyInjection.Create(typeToCreate, args);
        }

        public object Create(Type typeToCreate, Type typeToReturn, params object[] args)
        {
            return PolicyInjection.Create(typeToCreate, typeToReturn, args);
        }
    }

    public class PolicyInjectionContext : InjectionContext
    {
        public IMethodInvocation PolicyInjectionMethod
        {
            get { return Method as IMethodInvocation; }
        }
        public IMethodReturn PolicyInjectionMethodReturn
        {
            get { return MethodReturn as IMethodReturn; }
        }
        public GetNextHandlerDelegate PolicyInjectionNextMethodDelegate
        {
            get { return NextMethodDelegate as GetNextHandlerDelegate; }
        }
    }

}
