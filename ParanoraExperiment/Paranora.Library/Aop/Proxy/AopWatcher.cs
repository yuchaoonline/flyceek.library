using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;

namespace Paranora.Library.Aop.Proxy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AopWatcherAttribute : ProxyAttribute
    {
        private bool _Enabled = false;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public AopWatcherAttribute() : this(false)
        {
        }

        public AopWatcherAttribute(bool enabled)
        {
            this._Enabled = enabled;
        }
        //得到透明代理
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            MarshalByRefObject result = null;
            //未初始化的实例
            MarshalByRefObject target = base.CreateInstance(serverType);
            if (_Enabled)
            {
                AopWatcherProxy rp = new AopWatcherProxy(target, serverType);

                result = (MarshalByRefObject)rp.GetTransparentProxy();
            }
            else
            {
                result = target;
            }
            return result;
        }
    }

    public abstract class AopWatcher : ContextBoundObject
    {
        //存放为该类定制的属性信息
        //key=(string)name
        //value=(object)value
        private System.Collections.Hashtable _Attrs = null;
        public AopWatcher()
        {
            _Attrs = new System.Collections.Hashtable(1024);
        }
        public object GetAttribute(string name)
        {
            object result = null;
            if (_Attrs.ContainsKey(name))
            {
                result = _Attrs[name];
            }
            return result;
        }
        public void SetAttribute(string name, object value)
        {
            if (_Attrs.ContainsKey(name))
            {
                lock (_Attrs[name])
                {
                    _Attrs[name] = value;
                }
            }
            else
            {
                lock (_Attrs.SyncRoot)
                {
                    _Attrs.Add(name, value);
                }
            }
        }
        public abstract void BeforeFunctionCall(string funcName, object[] args);
        public abstract void AfterFunctionCall(string funcName, object[] args);

    }

    public class AopWatcherProxy : RealProxy
    {
        private readonly MarshalByRefObject target;

        public AopWatcherProxy(MarshalByRefObject obj, Type type) : base(type)
        {
            this.target = obj;
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage call = (IMethodCallMessage)msg;
            AopWatcher notifyBase = target as AopWatcher;//映射本地类
            IMethodReturnMessage result_msg = null;//返回调用结果消息

            string funcName = string.Empty;
            object[] funcArgs = new object[call.ArgCount];

            //如果触发的是构造函数，此时target的构建还未开始
            IConstructionCallMessage ctor = call as IConstructionCallMessage;
            if (ctor != null)
            {
                funcName = ctor.ActivationType.Namespace
                    + "." + ctor.ActivationType.Name
                    + "." + ctor.ActivationType.Name;
            }
            else
            {
                string className = string.Empty;
                int index = call.TypeName.IndexOf(',');
                className = call.TypeName.Substring(0, index);
                funcName = className
                    + "." + call.MethodName;
            }
            
            for (int i = 0; i < call.ArgCount; i++)
            {
                funcArgs[i] = call.Args[i];
            }
            
            if (ctor != null)
            {
                RealProxy default_proxy = RemotingServices.GetRealProxy(this.target);

                default_proxy.InitializeServerObject(ctor);
                MarshalByRefObject tp = (MarshalByRefObject)this.GetTransparentProxy();
                result_msg = null;
                result_msg = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctor, tp);

                notifyBase.AfterFunctionCall(funcName, funcArgs);

                return result_msg;

            }
            //方法调用前
            notifyBase.BeforeFunctionCall(funcName, funcArgs);

            result_msg = RemotingServices.ExecuteMessage(this.target, call);

            //方法调用后
            notifyBase.AfterFunctionCall(funcName, funcArgs);

            return result_msg;

        }
    }
}
