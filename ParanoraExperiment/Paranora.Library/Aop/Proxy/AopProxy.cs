using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace Paranora.Library.Aop.Proxy
{
    /// <summary>
    /// AopProxyAttribute 
    /// AOP代理特性，如果一个类想实现具体的AOP，只要实现AopProxyBase和IAopProxyFactory，然后加上该特性即可。
    /// 2005.04.11 
    /// </summary>

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AopProxyAttribute : ProxyAttribute
    {
        private IAopProxyFactory proxyFactory = null;
        private object parameter = null;

        public AopProxyAttribute(Type factoryType,object parameter)
        {
            this.proxyFactory = (IAopProxyFactory)Activator.CreateInstance(factoryType);
        }

        #region CreateInstance
        /// <summary>
        /// 获得目标对象的自定义透明代理
        /// </summary>
        public override MarshalByRefObject CreateInstance(Type serverType)//serverType是被AopProxyAttribute修饰的类
        {
            //未初始化的实例的默认透明代理
            MarshalByRefObject target = base.CreateInstance(serverType); //得到位初始化的实例（ctor未执行）
            object[] args = { target, serverType };
            //AopProxyBase rp = (AopProxyBase)Activator.CreateInstance(this.realProxyType ,args) ; //Activator.CreateInstance在调用ctor时通过了代理，所以此处将会失败

            //得到自定义的真实代理
            AopProxy rp = this.proxyFactory.CreateAopProxyInstance(target,serverType, parameter);//new AopControlProxy(target ,serverType) ;
            return (MarshalByRefObject)rp.GetTransparentProxy();
        }
        #endregion
    }

    /// <summary>
    /// AopProxyBase 所有自定义AOP代理类都从此类派生，覆写IAopOperator接口，实现具体的前/后处理 。
    /// 2005.04.12
    /// </summary>
    public abstract class AopProxy : RealProxy, IAopOperator
    {
        private readonly MarshalByRefObject target; //默认透明代理

        public AopProxy(MarshalByRefObject obj, Type type) : base(type)
        {
            this.target = obj;
        }

        public AopProxy(MarshalByRefObject obj) : base(obj.GetType())
        {
            this.target = obj;
        }

        #region Invoke
        public override IMessage Invoke(IMessage msg)
        {
            bool useAspect = false;
            IMethodCallMessage call = (IMethodCallMessage)msg;
            //查询目标方法是否使用了启用AOP的MethodAopSwitcherAttribute
            foreach (Attribute attr in call.MethodBase.GetCustomAttributes(false))
            {
                MethodAopSwitcherAttribute mehodAopAttr = attr as MethodAopSwitcherAttribute;
                if (mehodAopAttr != null)
                {
                    if (mehodAopAttr.UseAspect)
                    {
                        useAspect = true;
                        break;
                    }
                }
            }
            if (useAspect)
            {
                this.PreProcess(msg);
            }
            //如果触发的是构造函数，此时target的构建还未开始
            IConstructionCallMessage ctor = call as IConstructionCallMessage;
            if (ctor != null)
            {
                //获取最底层的默认真实代理
                RealProxy default_proxy = RemotingServices.GetRealProxy(this.target);
                default_proxy.InitializeServerObject(ctor);
                MarshalByRefObject tp = (MarshalByRefObject)this.GetTransparentProxy(); //自定义的透明代理 this
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);
            }

            IMethodReturnMessage result_msg = RemotingServices.ExecuteMessage(this.target, call); //将消息转化为堆栈，并执行目标方法，方法完成后，再将堆栈转化为消息

            if (useAspect)
            {
                this.PostProcess(msg, result_msg);
            }
            return result_msg;
        }
        #endregion

        #region IAopOperator 成员
        public abstract void PreProcess(IMessage requestMsg);
        public abstract void PostProcess(IMessage requestMsg, IMessage Respond);
        #endregion
    }

    public interface IAopOperator
    {
        void PreProcess(IMessage requestMsg);
        void PostProcess(IMessage requestMsg, IMessage Respond);
    }

    /// <summary>
    /// IAopProxyFactory 用于创建特定的Aop代理的实例，IAopProxyFactory的作用是使AopProxyAttribute独立于具体的AOP代理类。
    /// </summary>
    public interface IAopProxyFactory
    {
        AopProxy CreateAopProxyInstance(MarshalByRefObject obj,Type type, object parameter);

        AopProxy CreateAopProxyInstance(MarshalByRefObject obj, object parameter);
    }

    /// <summary>
    /// MethodAopSwitcherAttribute 用于决定一个被AopProxyAttribute修饰的class的某个特定方法是否启用截获 。
    /// 创建原因：绝大多数时候我们只希望对某个类的一部分Method而不是所有Method使用截获。
    /// 使用方法：如果一个方法没有使用MethodAopSwitcherAttribute特性或使用MethodAopSwitcherAttribute(false)修饰，
    ///     都不会对其进行截获。只对使用了MethodAopSwitcherAttribute(true)启用截获。
    /// 2005.05.11
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodAopSwitcherAttribute : Attribute
    {
        private bool useAspect = false;
        public MethodAopSwitcherAttribute(bool useAop)
        {
            this.useAspect = useAop;
        }
        public bool UseAspect
        {
            get
            {
                return this.useAspect;
            }
        }
    }
}