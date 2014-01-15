using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Paranora.Library.Aop;
using Paranora.Library.Aop.PIAB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Paranora.Library.Test.Aop.PIAB
{
    public class OrderProcessor:MarshalByRefObject
    {
        public Order Process(Order order)
        {
            Console.WriteLine("OrderProcessor.Process() is invocated!");
            return order;
        }
    }

    public class Order { }


    public class MyRealProxy<T> : RealProxy
    {
        private T _target;
        public MyRealProxy(T target) : base(typeof(T))
        {
            this._target = target;
        }
     
        public override IMessage Invoke(IMessage msg)
        {
            //Invoke injected pre-operation.
            Console.WriteLine("The injected pre-operation is invoked");
            //Invoke the real target instance.
            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            object returnValue = callMessage.MethodBase.Invoke(this._target, callMessage.Args);
            //Invoke the injected post-operation.
            Console.WriteLine("The injected post-peration is executed");
            //Return
            object[] copiedArgs = Array.CreateInstance(typeof(object), callMessage.Args.Length) as object[];
            callMessage.Args.CopyTo(copiedArgs, 0);
            return new ReturnMessage(returnValue, copiedArgs, copiedArgs.Length, callMessage.LogicalCallContext, callMessage);
        }
    }

    public static class PolicyInjectionFactory
    {
        public static T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            MyRealProxy<T> realProxy = new MyRealProxy<T>(instance);
            T transparentProxy = (T)realProxy.GetTransparentProxy();
            return transparentProxy;
        }
    }
    public class TestObject : MarshalByRefObject
    {
        public void DoSomeThing()
        {
            Console.WriteLine("The method of target object is invoked!");
        }
    }

    #region PolicyInjection Test

    public class CacheHandlerAttribute : PolicyInjectionHandlerAttribute
    {
        public string info { get; set; }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CacheInjection(info);
        }
    }

    public class CacheInjection : PolicyInjectionCallHandler
    {
        public string info { get; set; }

        public CacheInjection(string info)
        {
            this.info = info;
        }

        public override bool IsProcess(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, out Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn returnValue)
        {
            throw new NotImplementedException();
        }

        public override object BeforeProcess(InjectionContext context)
        {
            Console.WriteLine("BeforeProcess :" + info);
            return "BeforeProcess";
        }

        public override void AfterProcess(InjectionContext context)
        {
            Console.WriteLine("BeforeProcess is Return :" + context.MethodReturn);
            Console.WriteLine("AfterProcess :" + info);
        }

        public override object Process(InjectionContext context)
        {
            object result = null;
            PolicyInjectionContext policyContext=context as PolicyInjectionContext;
            result = policyContext.PolicyInjectionNextMethodDelegate()(policyContext.PolicyInjectionMethod, policyContext.PolicyInjectionNextMethodDelegate);

            return result;
        }
    }

    #endregion

    [CacheHandler(info="Class Injection")]
    public interface ITest
    {
        [CacheHandler(info = "Mehtod Injection")]
        void say(string str);
    }

    public class CTest :ITest
    {

        public void say(string str)
        {
            Console.WriteLine(str);
        }
    }

    public class PIABTest
    {
        public static void Test()
        {
            //OrderProcessor processor = PolicyInjection.Create<OrderProcessor>();
            //Order order = new Order();
            //processor.Process(order);
            //processor.Process(order);

            //TestObject obj = PolicyInjectionFactory.Create<TestObject>();
            //obj.DoSomeThing();

            PolicyInjectionCreateor policyInjectionCreator=new PolicyInjectionCreateor();

            ITest obj = policyInjectionCreator.Create<CTest, ITest>();
            obj.say("hello everyone!");
        }
    }
}
