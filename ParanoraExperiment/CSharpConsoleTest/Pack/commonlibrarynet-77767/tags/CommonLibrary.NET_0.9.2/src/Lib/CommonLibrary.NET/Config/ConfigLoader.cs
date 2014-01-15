using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;


namespace CommonLibrary
{
    public class ConfigLoader
    {
        private static ConnectionInfo _connection;


        /// <summary>
        /// Initialize db connection for LoadFromDb methods.
        /// </summary>
        /// <param name="connection"></param>
        public static void Init(ConnectionInfo connection)
        {
            _connection = connection;
        }


        /// <summary>
        /// Load config from single file or multiple files.
        /// </summary>
        /// <param name="envName">"prod"</param>
        /// <param name="path">"prod.config" or multiple paths delimited by command.
        /// e.g. "prod.config, qa.config, dev.config"</param>
        /// <returns></returns>
        public static IConfigSource LoadFromFile(string envName, string path)
        {
            // CASE 1 : single environment, represented with single configuration file.
            // e.g. "prod", "prod.config".
            if (!path.Contains(","))
                return new IniDocument(envName, path, true, true);

            // CASE 2 : single environment, represented with multiple configuration file.
            // e.g. "prod", "prod.config, qa.config, dev.config".
            string[] configPaths = path.Split(',');
            var configSources = new List<IConfigSource>();
            configPaths.ForEach(configPath =>
            {
                configSources.Add(new IniDocument(configPath, configPath, true, true));
            });

            IConfigSource inheritedConfig = new ConfigSourceMulti(configSources);
            return inheritedConfig;
        }


        /// <summary>
        /// Load from the database
        /// </summary>
        /// <param name="configNames">Comma delimited names of the configs to load
        /// from the database.</param>
        /// <returns></returns>
        public static IConfigSource LoadFromDb(string configNames)
        {
            return null;
        }


        /// <summary>
        /// Load config settings into a configSource from an object using
        /// it's public properties.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static IConfigSource LoadFromObject(object configObj)
        {
            IniDocument doc = new IniDocument();
            PropertyInfo[] props = configObj.GetType().GetProperties(BindingFlags.Public);
            props.ForEach(prop => doc["global", prop.Name] = ReflectionUtils.GetPropertyValueSafely(configObj, prop));
            return doc;
        }


        /// <summary>
        /// Load from string.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static IConfigSource LoadFromString(string configText)
        {
            IConfigSource config = new IniDocument(configText, false);
            return config;
        }
    }
}
