using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

using RemotingServerObject;
using System.Threading;

namespace RemotingServerConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("current domain : {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("current thread id :{0}", Thread.CurrentThread.ManagedThreadId);

            Fun6();

            Console.WriteLine("press any key to stop server...");
            Console.ReadKey();
        }

        static void Fun6()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterActivatedServiceType(typeof(Adder));

            Adder.OnSendMessage += Adder_OnSendMessage;
        }

        static void Fun5()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Adder),
                "AdderServer",
                WellKnownObjectMode.Singleton);

            Adder.OnSendMessage += Adder_OnSendMessage;
        }

        static void Adder_OnSendMessage(Guid id,string msg)
        {
            Console.WriteLine("curreit object id is : {0}", id.ToString());
            Console.WriteLine(msg);
        }

        static void Fun4()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Fatory),
                "FatoryServer",
                WellKnownObjectMode.Singleton);
        }

        static void Fun3()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            ActivatedServiceTypeEntry aste = new ActivatedServiceTypeEntry("RemotingServerObject.Adder", "RemotingServerObject");
            RemotingConfiguration.RegisterActivatedServiceType(aste);
        }

        

        static void Fun2()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            ActivatedServiceTypeEntry aste = new ActivatedServiceTypeEntry("RemotingServerObject.Adder", "RemotingServerObject");
            RemotingConfiguration.RegisterActivatedServiceType(aste);
        }

        static void Fun1()
        {
            HttpChannel httpChannel = new HttpChannel(65100);
            ChannelServices.RegisterChannel(httpChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Adder),
                "AdderServer",
                WellKnownObjectMode.SingleCall);
        }
    }
}
