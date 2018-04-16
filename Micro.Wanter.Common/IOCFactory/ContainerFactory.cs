using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Unity;

namespace Micro.Wanter.Common.IOCFactory
{
    public sealed  class ContainerFactory
    {
        public static IUnityContainer container {
            private set;
            get;
        }

        static ContainerFactory()
        {
            CreateContainer();
        }

        public static IUnityContainer CreateContainer()
        {
            if (container == null)
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");//找配置文件的路径
                Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

                container = new UnityContainer();
                section.Configure(container, "ruanmouContainer");
            }
            return container;
        }
    }
}