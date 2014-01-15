using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using RemotingInterface;
using RemotingServerObject;
using System.Runtime.Remoting.Activation;
using System.Threading;
using System.Runtime.Remoting.Lifetime;

namespace RemotingClientConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("current domain : {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("current thread id :{0}", Thread.CurrentThread.ManagedThreadId);

            Fun6();

            Console.ReadKey();
        }

        static void Fun6()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterActivatedClientType(typeof(Adder),
                "http://localhost:65100");

            Adder adder = new Adder();

            adder.SendMessage("paranora is come!");


            ILease iLease = (ILease)adder.GetLifetimeService();
            iLease.Renew(TimeSpan.FromSeconds(5));

            Console.WriteLine("IsObjectOutOfAppDomain:{0}", RemotingServices.IsObjectOutOfAppDomain(adder));
            Console.WriteLine("IsTransparentProxy:{0}", RemotingServices.IsTransparentProxy(adder));

            Console.WriteLine(adder.Add(100, 0.5));
            GC.Collect();
            
        }

        static void Fun5()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterWellKnownClientType(typeof(Adder),
                "http://localhost:65100/AdderServer");

            Adder adder = new Adder();

            adder.SendMessage("paranora is come!");

            Console.WriteLine("IsObjectOutOfAppDomain:{0}", RemotingServices.IsObjectOutOfAppDomain(adder));
            Console.WriteLine("IsTransparentProxy:{0}", RemotingServices.IsTransparentProxy(adder));

            Console.WriteLine(adder.Add(100, 0.5));
        }

        static void Fun4()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            MarshalByRefObject obj = (MarshalByRefObject)RemotingServices.Connect(typeof(IAdder),
                "http://localhost:65100/FatoryServer");
            IFatory fatory = obj as IFatory;
            IAdder adder = fatory.BuilderANewAdder();

            Console.WriteLine(adder.Add(100, 0.5));
        }

        static void Fun3()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterActivatedClientType(typeof(Adder),
                "http://localhost:65100");

            IAdder adder = new Adder();

            Console.WriteLine(adder.Add(100, 0.5));
        }

        static void Fun2()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            IAdder adder = Activator.CreateInstance(typeof(Adder),
                null,
                new object[] { new UrlAttribute("http://localhost:65100") }) as IAdder;

            Console.WriteLine(adder.Add(100, 0.5));
        }

        static void Fun1()
        {
            HttpChannel httpChannel = new HttpChannel(0);
            ChannelServices.RegisterChannel(httpChannel, false);
            MarshalByRefObject obj = (MarshalByRefObject)RemotingServices.Connect(typeof(IAdder),
                "http://localhost:65100/AdderServer");
            IAdder adder = obj as IAdder;

            Console.WriteLine(adder.Add(100, 0.5));
        }
    }
}
