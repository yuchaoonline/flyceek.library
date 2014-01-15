using RemotingInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemotingServerObject
{
    public class Adder : MarshalByRefObject, IAdder,IDisposable
    {
        public delegate void MessageHandler(Guid id,string msg);
        public static event MessageHandler OnSendMessage;

        Guid _id;

        public void SendMessage(string msg) //发送消息
        {
            if (OnSendMessage != null)
            {
                OnSendMessage(_id,msg);
            }
        }

        public Adder()
        {
            Console.WriteLine("current domain : {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("current thread id :{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Adder object is create.");
            _id = Guid.NewGuid();
        }
        public double Add(double a, double b)
        {
            return a + b;
        }
    
        public void Dispose()
        {
            Console.WriteLine("id : {0} object  is distory!", _id);
        }

        ~Adder()
        {
            Console.WriteLine("id : {0} object  is distory!", _id);
        }
    }

    public class Fatory : MarshalByRefObject,IFatory
    {
        public Fatory()
        {
            Console.WriteLine("Fatory object is create.");
        }

        public IAdder BuilderANewAdder()
        {
            return new Adder();
        }
    }
}
