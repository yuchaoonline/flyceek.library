using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common;
using FindProperty.Lib.Ioc;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace FindProperty.Lib.Ioc.Unity
{
    public class UnityCreator:IIoCCreator
    {
        public T Create<T>(params object[] parameter)
        {
            return Create<T>(PathHelper.LocateServerPath(ConfigInfo.UnityConfigFilePath));
        }

        public T Create<T>(string configFilePath, params object[] parameter)
        {
            try
            {
                IUnityContainer container = new UnityContainer();
                UnityContainerConfigurator configurator = new UnityContainerConfigurator(container);
                EnterpriseLibraryContainer.ConfigureContainer(configurator, ConfigurationSourceFactory.Create());
                container.AddNewExtension<InterceptionExtension>();

                ExeConfigurationFileMap basicFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFilePath };

                UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager
                    .OpenMappedExeConfiguration(basicFileMap, ConfigurationUserLevel.None)
                    .GetSection("unity");
                foreach (var item in section.Containers)
                {
                    section.Configure(container, item.Name);
                }
                return (T)container;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
