/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;


namespace CommonLibrary
{    

    /// <summary>
    /// Utility class for loading inheritance based environments.
    /// </summary>
    public class EnvironmentUtils
    {
        /// <summary>
        /// Get environment type from name.
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static EnvironmentType GetEnvType(string env)
        {
            env = env.ToLower().Trim();
            // Determine prod/qa etc.
            if (env == "prod") return EnvironmentType.Prod;
            if (env == "uat") return EnvironmentType.Uat;
            if (env == "qa") return  EnvironmentType.Qa;
            if (env == "dev") return EnvironmentType.Dev;
            return EnvironmentType.Unknown;
        }


        /// <summary>
        /// Parse the selected environments and config paths.
        /// </summary>
        /// <param name="selectedEnvironment"></param>
        /// <param name="configPaths"></param>
        /// <returns></returns>
        public static List<List<string>> Parse(string selectedEnvironment, string configPaths)
        {
            bool hasConfigs = !string.IsNullOrEmpty(configPaths);
            bool hasMultiConfigs = hasConfigs && configPaths.Contains(",");
            bool hasMultiEnvs = selectedEnvironment.Contains(",");

            var selectedEnvs = new string[] { selectedEnvironment };
            var configs = hasConfigs ? new string[] { configPaths } : new string[] { };

            // "prod,dev"
            // "..\prod.config,..\dev.config"
            if (hasMultiEnvs) selectedEnvs = selectedEnvironment.Split(',');
            if (hasConfigs && hasMultiConfigs) configs = configPaths.Split(',');

            List<List<string>> both = new List<List<string>>() { new List<string>(), new List<string>() };
            foreach (string env in selectedEnvs) both[0].Add(env);
            foreach (string config in configs) both[1].Add(config);

            return both;
        }


        /// <summary>
        /// Replaces substitution names such as ${name} with the substitution value.
        /// </summary>
        /// <param name="envs"></param>
        /// <param name="ctx"></param>
        public static void DoSubstitutions( EnvironmentContext ctx)
        {
            string pattern = "\\$\\{(?<subName>[\\S]+)\\}";
            List<EnvItem> envs = ctx.Envs;

            foreach (EnvItem env in envs)
            {
                if (!string.IsNullOrEmpty(env.Path))
                {
                    Match match = Regex.Match(env.Path, pattern);
                    if (match != null && match.Success)
                    {
                        string subname = match.Groups["subName"].Value;
                        string replacement = ctx.Substitutions[subname];

                        env.Path = env.Path.Replace("${" + subname + "}", replacement);
                    }
                }
            }
        }


        /// <summary>
        /// Traverses the nodes inheritance path to build a single flat delimeted line of 
        /// inheritance paths.
        /// e.g. returns "Prod,Uat,Qa,Dev".
        /// </summary>
        /// <returns></returns>
        public static string ConvertNestedToFlatInheritance(EnvItem env, EnvironmentContext ctx)
        {
            // Return name of environment provided if it doesn't have 
            // any inheritance chain.
            if (string.IsNullOrEmpty(env.InheritPath))
                return env.Name;

            // Single parent.
            if (env.InheritPath.IndexOf(",") < 0)
            {
                // Get the parent.
                EnvItem parent = ctx.EnvsByName[env.InheritPath.Trim()];
                return env.Name + "," + ConvertNestedToFlatInheritance(parent, ctx);
            }
            
            // Multiple parents.
            string[] parents = env.InheritPath.Split(',');
            string path = env.Name;
            foreach (string parent in parents)
            {
                EnvItem parentEnv = ctx.EnvsByName[env.InheritPath.Trim()];
                path += "," + ConvertNestedToFlatInheritance(parentEnv, ctx);
            }
            return path;
        }


        /// <summary>
        /// Loads an inheritance chain delimited by ,(comma)
        /// </summary>
        /// <param name="delimitedInheritancePath"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static List<EnvItem> LoadInheritance(EnvItem coreEnv, EnvironmentContext ctx)
        {
            // No inheritance chain.
            if (string.IsNullOrEmpty(coreEnv.InheritPath))
            {
                List<EnvItem> list = new List<EnvItem>();
                list.Add(coreEnv);
                return list;
            }
            string inheritancePath = coreEnv.InheritPath;
            StringBuilder buffer = new StringBuilder();

            // Get flat list "prod,uat,qa,dev" from traversing the refrenced nodes.
            if (coreEnv.DeepInherit)
                inheritancePath = ConvertNestedToFlatInheritance(coreEnv, ctx);

            string[] parents = inheritancePath.Split(',');
            List<EnvItem> inheritanceList = new List<EnvItem>() { coreEnv };

            // Build inheritance path where the first one is the core, following the
            // older parents.
            foreach (string parent in parents)
            {
                if (ctx.EnvsByName.ContainsKey(parent))
                {
                    EnvItem parentEnv = ctx.EnvsByName[parent];
                    inheritanceList.Add(parentEnv);
                }
            }
            return inheritanceList;
        }


        /// <summary>
        /// Get the environment type based on the inheritance chain order.
        /// </summary>
        /// <param name="inheritanceChainedEnvs"></param>
        /// <returns></returns>
        public static EnvironmentType GetEnvironmentType(List<EnvItem> inheritanceChainedEnvs)
        {
            // If this is not purely inherited, it has a confi file of it's own and a
            // environment type.
            foreach (EnvItem env in inheritanceChainedEnvs)
            {
                if (!env.IsPurelyInherited())
                    return env.EnvType;
            }
            return EnvironmentType.Unknown;
        }


        /// <summary>
        /// Initializes the configuration from the Current environment.
        /// </summary>
        public static void InitConfig()
        {
            // Get the inheritance based configuration from the current environment.
            IConfigSource inheritanceBasedConfig = EnvironmentUtils.LoadConfigs();

            // Set the current static config based on the current environment.
            Config.Init(inheritanceBasedConfig);
        }


        /// <summary>
        /// Get the configuration based on the environment inheritance of the EnvironmentCurrent.
        /// </summary>
        /// <returns></returns>
        public static IConfigSource LoadConfigs()
        {
            return LoadConfigs(EnvironmentCurrent.Inheritance);
        }


        /// <summary>
        /// Load config from environment inheritance chain.
        /// </summary>
        /// <param name="env">prod,qa,dev</param>
        /// <returns></returns>
        public static IConfigSource LoadConfigs(IList<EnvItem> envs)
        {
            // Get the list of all the environments and their config files.
            var configSources = new List<IConfigSource>();

            // CASE 1 : single environment, but represented with multiple configuration files.
            // e.g. "prod", "prod.config, qa.config, dev.config".

            // CASE 2: Multiple environment.
            envs.ForEach(env => configSources.Add(LoadConfigs(env)));

            IConfigSource config = new ConfigSourceMulti(configSources);
            return config;
        }


        /// <summary>
        /// Load an configuration source from a sinlge environment item.
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IConfigSource LoadConfigs(EnvItem env)
        {
            return ConfigLoader.LoadFromFile(env.Name, env.Path);
        }

        
        /// <summary>
        /// Get list of names of environments that can be selected.
        /// </summary>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public static List<string> GetSelectableEnvironments(EnvironmentContext ctx)
        {
            var selectable = from env in ctx.Envs where env.IsSelectable = true select env;
            List<string> envNames = new List<string>();
            selectable.ForEach<EnvItem>(env => envNames.Add(env.Name));
            return envNames;
        }

        
        /// <summary>
        /// Build a delimited inheritance path of environments names using each
        /// of the envitems supplied.
        /// e.g. prod,qa,dev.
        /// if "," is the delimeter and "prod", "qa" are the environment names.
        /// </summary>
        /// <param name="_inheritedChainedEnvs"></param>
        /// <returns></returns>
        public static string CollectEnvironmentProps(List<EnvItem> inheritedChainedEnvs, string delimeter, Func<EnvItem, string> propGetter )
        {
            // Check for null/empty.
            if (inheritedChainedEnvs == null || inheritedChainedEnvs.Count == 0)
                return string.Empty;

            // Only 1. no inheritance.
            if (inheritedChainedEnvs.Count == 1)
                return propGetter(inheritedChainedEnvs[0]);

            string chain = propGetter(inheritedChainedEnvs[0]);

            for (int ndx = 1; ndx < inheritedChainedEnvs.Count; ndx++)
                chain += delimeter + propGetter(inheritedChainedEnvs[ndx]);

            return chain;
        }
    }
}
