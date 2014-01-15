using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemotingConsoleAppTest
{
    public class Foo : MarshalByRefObject
    {
        public void Display(string str)
        {
            Console.WriteLine(str);
            Console.WriteLine("current domain : {0}",AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("current thread id :{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }

    [Serializable]
    public class Foo1 
    {
        public Foo1()
        {
            Console.WriteLine("Foo1 object is create.");
        }
        public void Display(string str)
        {
            Console.WriteLine(str);
            Console.WriteLine("current domain : {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("current thread id :{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Test1();

            Console.ReadKey();
        }

        static void Test1()
        {
            Foo obj1 = new Foo();
            obj1.Display("abc");

            Console.WriteLine("IsObjectOutOfAppDomain:{0}", RemotingServices.IsObjectOutOfAppDomain(obj1));
            Console.WriteLine("IsTransparentProxy:{0}", RemotingServices.IsTransparentProxy(obj1));

            AppDomain appdomain = AppDomain.CreateDomain("testAppdomain");
            Foo obj2 = (Foo)appdomain.CreateInstanceAndUnwrap("RemotingConsoleAppTest", "RemotingConsoleAppTest.Foo");
            obj2.Display("def");
            Console.WriteLine("IsObjectOutOfAppDomain:{0}", RemotingServices.IsObjectOutOfAppDomain(obj2));
            Console.WriteLine("IsTransparentProxy:{0}", RemotingServices.IsTransparentProxy(obj2));

            ObjectHandle objectHandle = AppDomain.CreateDomain("testAppdomin").CreateInstance("RemotingConsoleAppTest", "RemotingConsoleAppTest.Foo1");
            Foo1 obj3 = (Foo1)objectHandle.Unwrap();
            obj3.Display("leng");
            Console.WriteLine("IsObjectOutOfAppDomain:{0}", RemotingServices.IsObjectOutOfAppDomain(obj3));
            Console.WriteLine("IsTransparentProxy:{0}", RemotingServices.IsTransparentProxy(obj3));
        }
    }
}
