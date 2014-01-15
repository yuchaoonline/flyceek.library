using Paranora.Library.Aop.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Paranora.Library.Test.Aop.Proxy
{

    public class AopControlProxyFactory : IAopProxyFactory
    {
        #region IAopProxyFactory 成员
        public AopProxy CreateAopProxyInstance(MarshalByRefObject obj,Type type, object parameter)
        {
            return new AopControlProxy(obj, type);
        }
        #endregion


        public AopProxy CreateAopProxyInstance(MarshalByRefObject obj, object parameter)
        {
            return new AopControlProxy(obj);
        }
    }

    //自定义真实代理
    public class AopControlProxy : AopProxy
    {
        protected DateTime sd;
        protected DateTime ed;

        public AopControlProxy(MarshalByRefObject obj,Type type)  : base(obj,type)
        {
        }

        public AopControlProxy(MarshalByRefObject obj):base(obj)
        {
        }

        public override void PreProcess(IMessage requestMsg)
        {
            sd = DateTime.Now;
            Console.WriteLine("Begin Aop !");
        }
        public override void PostProcess(IMessage requestMsg, IMessage Respond)
        {
            ed = DateTime.Now;
            Console.WriteLine("End Aop !");
            Console.WriteLine((ed - sd).Milliseconds);
        }
    }

    public interface IExample
    {
        void say_hello();
    }

    public class Example : MarshalByRefObject, IExample
    {
        private string name;
        public Example(string a)
        {
            this.name = a;
        }


        [MethodAopSwitcherAttribute(true)]
        public void say_hello()
        {
            Console.WriteLine("hello ! " + name);
        }

        public void sayByeBye()
        {
            Console.WriteLine("Bye ! " + name);
        }
    }
    //将自己委托给AOP代理AopControlProxy
    // ContextBoundObject
    //放到特定的上下文中，该上下文外部才会得到该对象的透明代理
    [AopProxy(typeof(AopControlProxyFactory), null)]
    public class Example1 : ContextBoundObject
    {
        private string name;
        public Example1(string a)
        {
            this.name = a;
        }


        [MethodAopSwitcherAttribute(true)]
        public void say_hello()
        {
            Console.WriteLine("hello ! " + name);
        }

        public void sayByeBye()
        {
            Console.WriteLine("Bye ! " + name);
        }
    }

    public class AopProxyTest
    {
        
        public static void Test()
        {
            Example obj2 = new AopControlProxy(new Example("Moon")).GetTransparentProxy() as Example;
            Example obj1 = new AopControlProxyFactory().CreateAopProxyInstance(new Example("Flyceek"), null).GetTransparentProxy() as Example;
            obj1.say_hello();

            obj2.say_hello();

            var obj = new Example1("Paranora");
            obj.say_hello();
        }
    }
}
