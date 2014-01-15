using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace FindProperty.Lib.Ioc.Unity
{
    public class InterceptionStrategy : BuilderStrategy
    {
        public IUnityContainer UnityContainer { get; private set; }
        public InjectionMember InterceptionMember { get; private set; }

        public InterceptionStrategy()
        {
            IUnityContainer container = new UnityContainer();
            UnityContainerConfigurator configurator = new UnityContainerConfigurator(container);
            EnterpriseLibraryContainer.ConfigureContainer(configurator, ConfigurationSourceFactory.Create());
            this.UnityContainer = container;
            this.InterceptionMember = new InstanceInterceptionPolicySettingInjectionMember(new TransparentProxyInterceptor());
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            if (null == context.Existing ||
                context.Existing.GetType().FullName.StartsWith("Microsoft.Practices") ||
                context.Existing is IInterceptingProxy)
            {
                return;
            }

            context.Existing = this.UnityContainer.Configure<Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity.TransientPolicyBuildUpExtension>().BuildUp
                (context.OriginalBuildKey.Type, context.Existing, null, this.InterceptionMember);
        }
    }
}