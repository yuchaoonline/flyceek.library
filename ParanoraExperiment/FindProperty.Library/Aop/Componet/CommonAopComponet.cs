using FindProperty.Lib.Aop.Provider.PIAB;
using FindProperty.Lib.Cache.Component;
using FindProperty.Lib.Cache.Component.CacheKeyCreator;
using FindProperty.Lib.Cache.Component.ResultExaminer;
using FindProperty.Lib.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Aop.Componet
{
    delegate List<string> getIds(params object[] args);
    public class CommonInjectionRuntimeContext
    {
        public IResultExaminer ResultExaminer
        {
            get;
            set;
        }

        public ICacheKeyCreator CacheKeyCreator
        {
            get;
            set;
        }

        public CacheComponetFundation CacheComponet
        {
            get;
            set;
        }

        public bool EnableCache
        {
            get;
            set;
        }

        public string CacheKey
        {
            get;
            set;
        }

        public int CacheSecond
        {
            get;
            set;
        }

        public object CachedObject { get; set; }
    }

    public class CommonCallHandlerAttribute : PolicyInjectionHandlerAttribute
    {
        public Type CacheKeyCreatorType
        {
            get;
            set;
        }

        public Type ResultExaminerType
        {
            get;
            set;
        }

        public Type CacheComponetType
        {
            get;
            set;
        }

        public string CacheKey
        {
            get;
            set;
        }

        public int CacheSecond
        {
            get;
            set;
        }

        public bool DisableCache
        {
            get;
            set;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            CommonCallHandler callHandler = new CommonCallHandler();
            callHandler.CurrentRuntime = new CommonInjectionRuntimeContext();

            #region cache componet

            if (this.DisableCache)
            {
                callHandler.CurrentRuntime.EnableCache = false;
            }
            else
            {
                callHandler.CurrentRuntime.EnableCache = (ConfigInfo.EnableCache > 0); ;
            }

            if (callHandler.CurrentRuntime.EnableCache)
            {
                callHandler.CurrentRuntime.CacheKey = this.CacheKey;
                callHandler.CurrentRuntime.CacheSecond = this.CacheSecond;
                if (CacheKeyCreatorType != null)
                {
                    callHandler.CurrentRuntime.CacheKeyCreator = Activator.CreateInstance(CacheKeyCreatorType, null) as ICacheKeyCreator;
                }
                else
                {
                    callHandler.CurrentRuntime.CacheKeyCreator = new MethodCacheKeyCreator();
                }
                if (ResultExaminerType != null)
                {
                    callHandler.CurrentRuntime.ResultExaminer = Activator.CreateInstance(ResultExaminerType, null) as IResultExaminer;
                }
                else
                {
                    callHandler.CurrentRuntime.ResultExaminer = new IntelligentResultExaminer();
                }

                if (callHandler.CurrentRuntime.CacheComponet != null)
                {
                    callHandler.CurrentRuntime.CacheComponet = Activator.CreateInstance(CacheComponetType, null) as CacheComponetFundation;
                }
                else
                {
                    callHandler.CurrentRuntime.CacheComponet = new SingleResultCacheComponet();
                }
            }
            #endregion

            return callHandler;
        }
    }

    public class CommonCallHandler : PolicyInjectionCallHandler
    {
        public CommonInjectionRuntimeContext CurrentRuntime
        {
            get;
            set;
        }

        PolicyInjectionContext CurrentPolicyContext
        {
            get;
            set;
        }

        public CommonCallHandler()
        {
        }

        public override bool IsProcess(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, out Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn returnValue)
        {
            throw new NotImplementedException();
        }

        public override void BeforeProcess(InjectionContext context)
        {
            CurrentPolicyContext = context as PolicyInjectionContext;
            #region cache task
            if (CurrentRuntime.EnableCache)
            {
                if (string.IsNullOrEmpty(CurrentRuntime.CacheKey))
                {
                    CurrentRuntime.CacheKey = CurrentRuntime.CacheKeyCreator.CreateKey(CurrentPolicyContext.PolicyInjectionMethod.MethodBase.Name,
                        CurrentPolicyContext.PolicyInjectionMethod.Arguments);
                }
                CurrentRuntime.CachedObject = CurrentRuntime.CacheComponet.GetValue(CurrentRuntime.CacheKey);
            }
            #endregion
        }        

        public override object Process(InjectionContext context)
        {
            object result = null;
            bool isProcess = false;

            #region cache task
            if (CurrentRuntime.EnableCache)
            {
                if (CurrentRuntime.CachedObject == null)
                {
                    result = CurrentPolicyContext.PolicyInjectionNextMethodDelegate()(CurrentPolicyContext.PolicyInjectionMethod, CurrentPolicyContext.PolicyInjectionNextMethodDelegate);
                }
                else
                {
                    result = CurrentPolicyContext.PolicyInjectionMethod.CreateMethodReturn(CurrentRuntime.CachedObject);
                }
                isProcess = true;
            }            
            #endregion

            if (!isProcess)
            {
                result = CurrentPolicyContext.PolicyInjectionNextMethodDelegate()(CurrentPolicyContext.PolicyInjectionMethod, CurrentPolicyContext.PolicyInjectionNextMethodDelegate);
            }

            return result;
        }

        public override void AfterProcess(InjectionContext context)
        {
            #region cache task
            if (CurrentRuntime.EnableCache)
            {
                if (CurrentRuntime.CachedObject == null&&!CurrentRuntime.ResultExaminer.IsNullOrEmpty(context.MethodReturn))
                {
                    CurrentRuntime.CacheComponet.SetValue(CurrentRuntime.CacheKey,
                       context.MethodReturn,
                       CurrentRuntime.CacheSecond);
                }
            }
            #endregion
        }
    }
}
