using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Paranora.Library.Aop.Proxy
{
    public abstract class HandlerAttribute : Attribute
    {
        public abstract ICallHandler CreateCallHandler();

        public int Ordinal { get; set; }

        public bool ReturnIfError { get; set; }
    }

    public interface ICallHandler
    {
        object PreInvoke(InvocationContext context);

        void PostInvoke(InvocationContext context, object correlationState);

        int Ordinal { get; set; }

        bool ReturnIfError { get; set; }
    }

    public abstract class CallHandlerBase : ICallHandler
    {
        #region ICallHandler Members
        public abstract object PreInvoke(InvocationContext context);

        public abstract void PostInvoke(InvocationContext context, object correlationState);

        public int Ordinal { get; set; }

        public bool ReturnIfError{ get; set; }
        #endregion
    }

    public class CallHandlerPipeline
    {
        private object _target;
        private IList<ICallHandler> _callHandlers;

        public CallHandlerPipeline(object target) : this(new List<ICallHandler>(), target)
        {

        }

        public CallHandlerPipeline(IList<ICallHandler> callHandlers, object target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (callHandlers == null)
            {
                throw new ArgumentNullException("callHandlers");
            }

            this._target = target;
            this._callHandlers = callHandlers;
        }

        public void Invoke(InvocationContext context)
        {
            Stack<object> correlationStates = new Stack<object>();
            Stack<ICallHandler> callHandlers = new Stack<ICallHandler>();

            foreach (ICallHandler callHandler in this._callHandlers)
            {
                correlationStates.Push(callHandler.PreInvoke(context));
                if (context.Reply != null && context.Reply.Exception != null && callHandler.ReturnIfError)
                {
                    context.Reply = new ReturnMessage(context.Reply.Exception, context.Request);
                    return;
                }
                callHandlers.Push(callHandler);
            }

            //Invoke Target Object.
            object[] copiedArgs = Array.CreateInstance(typeof(object), context.Request.Args.Length) as object[];
            context.Request.Args.CopyTo(copiedArgs, 0);
            try
            {
                object returnValue = context.Request.MethodBase.Invoke(this._target, copiedArgs);
                context.Reply = new ReturnMessage(returnValue, copiedArgs, copiedArgs.Length, context.Request.LogicalCallContext, context.Request);
            }
            catch (Exception ex)
            {
                context.Reply = new ReturnMessage(ex, context.Request);
            }

            //PostInvoke.
            while (callHandlers.Count > 0)
            {
                ICallHandler callHandler = callHandlers.Pop();
                object correlationState = correlationStates.Pop();
                callHandler.PostInvoke(context, correlationState);
            }
        }

        public void Sort()
        {
            ICallHandler[] callHandlers = this._callHandlers.ToArray<ICallHandler>();
            ICallHandler swaper = null;
            for (int i = 0; i < callHandlers.Length - 1; i++)
            {
                for (int j = i + 1; j < callHandlers.Length; j++)
                {
                    if (callHandlers[i].Ordinal > callHandlers[j].Ordinal)
                    {
                        swaper = callHandlers[i];
                        callHandlers[i] = callHandlers[j];
                        callHandlers[j] = swaper;
                    }
                }
            }

            this._callHandlers = callHandlers.ToList<ICallHandler>();
        }

        public void Combine(CallHandlerPipeline pipeline)
        {
            if (pipeline == null)
            {
                throw new ArgumentNullException("pipeline");
            }

            foreach (ICallHandler callHandler in pipeline._callHandlers)
            {
                this.Add(callHandler);
            }
        }

        public void Combine(IList<ICallHandler> callHandlers)
        {
            if (callHandlers == null)
            {
                throw new ArgumentNullException("callHandlers");
            }

            foreach (ICallHandler callHandler in callHandlers)
            {
                this.Add(callHandler);
            }
        }

        public ICallHandler Add(ICallHandler callHandler)
        {
            if (callHandler == null)
            {
                throw new ArgumentNullException("callHandler");
            }

            this._callHandlers.Add(callHandler);
            return callHandler;
        }
    }

    public class InstanceBuilder
    {
        public static TInterface Create<TObject, TInterface>() where TObject : TInterface
        {
            TObject target = Activator.CreateInstance<TObject>();
            //object[] attributes = typeof(TObject).GetCustomAttributes(typeof(HandlerAttribute),true);
            //IList<ICallHandler> callHandlers = new List<ICallHandler>();
            //foreach (var attribute in attributes)
            //{
            //    HandlerAttribute handlerAttribute = attribute as HandlerAttribute;
            //    callHandlers.Add(handlerAttribute.CreateCallHandler());
            //}

            InterceptingRealProxy<TInterface> realProxy = new InterceptingRealProxy<TInterface>(target, CreateCallHandlerPipeline<TObject, TInterface>(target));
            return (TInterface)realProxy.GetTransparentProxy();
        }

        public static T Create<T>()
        {
            return Create<T, T>();
        }

        public static IDictionary<MemberInfo, CallHandlerPipeline> CreateCallHandlerPipeline<TObject, TInterfce>(TObject target)
        {
            CallHandlerPipeline pipeline = new CallHandlerPipeline(target);
            object[] attributes = typeof(TObject).GetCustomAttributes(typeof(HandlerAttribute), true);
            foreach (var attribute in attributes)
            {
                HandlerAttribute handlerAttribute = attribute as HandlerAttribute;
                pipeline.Add(handlerAttribute.CreateCallHandler());
            }

            IDictionary<MemberInfo, CallHandlerPipeline> keyedCallHandlerPipelines = new Dictionary<MemberInfo, CallHandlerPipeline>();

            foreach (MethodInfo methodInfo in typeof(TObject).GetMethods())
            {
                MethodInfo declareMethodInfo = typeof(TInterfce).GetMethod(methodInfo.Name, BindingFlags.Public | BindingFlags.Instance);
                if (declareMethodInfo == null)
                {
                    continue;
                }
                keyedCallHandlerPipelines.Add(declareMethodInfo, new CallHandlerPipeline(target));
                foreach (var attribute in methodInfo.GetCustomAttributes(typeof(HandlerAttribute), true))
                {
                    HandlerAttribute handlerAttribute = attribute as HandlerAttribute;
                    keyedCallHandlerPipelines[declareMethodInfo].Add(handlerAttribute.CreateCallHandler());
                }
                keyedCallHandlerPipelines[declareMethodInfo].Combine(pipeline);
                keyedCallHandlerPipelines[declareMethodInfo].Sort();
            }

            return keyedCallHandlerPipelines;
        }
    }

    public class InterceptingRealProxy<T> : RealProxy
    {
        private IDictionary<MemberInfo, CallHandlerPipeline> _callHandlerPipelines;

        public InterceptingRealProxy(object target, IDictionary<MemberInfo, CallHandlerPipeline> callHandlerPipelines) : base(typeof(T))
        {
            if (callHandlerPipelines == null)
            {
                throw new ArgumentNullException("callHandlerPipelines");
            }

            this._callHandlerPipelines = callHandlerPipelines;
        }

        public override IMessage Invoke(IMessage msg)
        {
            InvocationContext context = new InvocationContext();
            context.Request = (IMethodCallMessage)msg;
            this._callHandlerPipelines[context.Request.MethodBase].Invoke(context);
            return context.Reply;
        }
    }

    public class InvocationContext
    {
        public IMethodCallMessage Request { get; set; }

        public ReturnMessage Reply { get; set; }

        public IDictionary<object, object> Properties { get; set; }
    }
}
