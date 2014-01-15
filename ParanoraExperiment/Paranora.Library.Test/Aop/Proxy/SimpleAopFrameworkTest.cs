using Paranora.Library.Aop.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Paranora.Library.Test.Aop.Proxy
{
    class CustomCallHandler1 : CallHandlerBase
    {
        public override object PreInvoke(InvocationContext context)
        {
            Console.WriteLine("CustomCallHandler1.PreInvoke()");
            return "CustomCallHandler1";
        }

        public override void PostInvoke(InvocationContext context, object correlationState)
        {
            Console.WriteLine("CustomCallHandler1.PostInvoke(,{0})", correlationState);
        }
    }

    class CustomCallHandler2 : CallHandlerBase
    {
        public override object PreInvoke(InvocationContext context)
        {
            Console.WriteLine("CustomCallHandler2.PreInvoke()");
            return "CustomCallHandler2";
        }

        public override void PostInvoke(InvocationContext context, object correlationState)
        {
            Console.WriteLine("CustomCallHandler2.PostInvoke(,{0})", correlationState);
        }
    }

    public class CustomCallHandler1Attribute : HandlerAttribute
    {
        public override ICallHandler CreateCallHandler()
        {
            return new CustomCallHandler1() { Ordinal = this.Ordinal };
        }
    }

    public class CustomCallHandler2Attribute : HandlerAttribute
    {
        public override ICallHandler CreateCallHandler()
        {
            return new CustomCallHandler2() { Ordinal = this.Ordinal };
        }
    }

    public class ExceptionCallHandler : CallHandlerBase
    {
        public string MessageTemplate { get; set; }
        public bool Rethrow{ get; set; }

        public ExceptionCallHandler()
        {
            this.MessageTemplate = "{Message}";
        }

        public override object PreInvoke(InvocationContext context)
        {
            return null;
        }

        public override void PostInvoke(InvocationContext context, object correlationState)
        {
            if (context.Reply.Exception != null)
            {
                string message = this.MessageTemplate.Replace("{Message}", context.Reply.Exception.InnerException.Message)
                    .Replace("{Source}", context.Reply.Exception.InnerException.Source)
                    .Replace("{StackTrace}", context.Reply.Exception.InnerException.StackTrace)
                    .Replace("{HelpLink}", context.Reply.Exception.InnerException.HelpLink)
                    .Replace("{TargetSite}", context.Reply.Exception.InnerException.TargetSite.ToString());
                Console.WriteLine(message);

                if (!this.Rethrow)
                {
                    context.Reply = new ReturnMessage(null, null, 0, context.Request.LogicalCallContext, context.Request);
                }
            }
        }
    }



    public class ExceptionCallHandlerAttribute : HandlerAttribute
    {

        public string MessageTemplate
        { get; set; }

        public bool Rethrow
        { get; set; }

        public ExceptionCallHandlerAttribute()
        {
            this.MessageTemplate = "{Message}";
        }

        public override ICallHandler CreateCallHandler()
        {
            return new ExceptionCallHandler()
            {
                Ordinal = this.Ordinal,
                Rethrow = this.Rethrow,
                MessageTemplate = this.MessageTemplate,
                ReturnIfError = this.ReturnIfError
            };
        }
    }


    public class TransactionScopeCallHandler : CallHandlerBase
    {
        public override object PreInvoke(InvocationContext context)
        {
            return new TransactionScope();
        }

        public override void PostInvoke(InvocationContext context, object correlationState)
        {
            TransactionScope transactionScope = (TransactionScope)correlationState;
            if (context.Reply.Exception == null)
            {
                transactionScope.Complete();
            }
            transactionScope.Dispose();
        }
    }

    public class TransactionScopeCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateCallHandler()
        {
            return new TransactionScopeCallHandler() { Ordinal = this.Ordinal, ReturnIfError = this.ReturnIfError };
        }
    }

    [CustomCallHandler1Attribute(Ordinal = 1)]
    public class Foo : MarshalByRefObject,IFoo
    {
        //[ExceptionCallHandlerAttribute(Ordinal = 3, MessageTemplate = "Message:{Message}\nStackTrace:{StackTrace}", Rethrow = false)]
        [CustomCallHandler2Attribute(Ordinal = 1)]
        public string F(out string str)
        {
            Console.WriteLine("Foo");
            str = "Foo";

            //throw new Exception("ex");
            return str;
        }
    }

    public interface IFoo
    {
        string F(out string str);
    }

    public class SimpleAopFrameworkTest
    {
        public static void Test()
        {
            string str = "";

            InstanceBuilder.Create<Foo, IFoo>().F(out str);

        }
    }
}
