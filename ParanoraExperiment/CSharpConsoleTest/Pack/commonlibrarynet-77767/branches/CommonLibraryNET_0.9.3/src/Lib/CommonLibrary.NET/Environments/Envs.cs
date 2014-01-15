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


namespace ComLib.Environments
{
    /// <summary>
    /// Used to stored the environment for static access.
    /// </summary>
    public class Envs
    {
        private static IDictionary<string, IEnv> _environments = new Dictionary<string, IEnv>();


        /// <summary>
        /// Initialize w/ the selected environment and config paths.
        /// </summary>
        /// <param name="selected">Set either "prod | uat | qa | dev"</param>
        /// <param name="configPaths"></param>
        public static void Set(string selected)
        {
            Set(selected, "prod,uat,qa,dev", string.Empty);
        }


        /// <summary>
        /// Set the current environment and also supply available environment names/types.
        /// </summary>
        /// <param name="selected">"prod" or "prod1". This should match the name
        /// from <paramref name="availableEnvsCommaDelimited"/></param>
        /// <param name="availableEnvsCommaDelimited">
        /// 1. "prod,qa,dev". If the names are the same as the types.
        /// 2. "prod1:prod,qa1:qa,mydev:dev" If the names are different that the env type names.
        /// </param>
        /// <param name="refPaths">The config reference paths. e.g. "prod.config,qa.config".</param>
        public static void Set(string selected, string availableEnvsCommaDelimited)
        {
            Set(selected, availableEnvsCommaDelimited, string.Empty);
        }


        /// <summary>
        /// Set the current environment and also supply available environment names/types.
        /// </summary>
        /// <param name="selected">"prod" or "prod1". This should match the name
        /// from <paramref name="availableEnvsCommaDelimited"/></param>
        /// <param name="availableEnvsCommaDelimited">
        /// 1. "prod,qa,dev". If the names are the same as the types.
        /// 2. "prod1:prod,qa1:qa,mydev:dev" If the names are different that the env type names.
        /// </param>
        /// <param name="refPaths">The config reference paths. e.g. "prod.config,qa.config".</param>
        public static void Set(string selected, string availableEnvsCommaDelimited, string refPaths)
        {
            // Check available envs were supplied.
            if (string.IsNullOrEmpty(availableEnvsCommaDelimited))
                availableEnvsCommaDelimited = "prod,uat,qa,dev";

            var availableEnvs = EnvUtils.ParseEnvsToItems(availableEnvsCommaDelimited);
            Set("default", selected, availableEnvs, refPaths);
        }


        /// <summary>
        /// Set the current environment.
        /// </summary>
        /// <param name="selected">Name of the selected environment</param>
        /// <param name="availableEnvsCommaDelimited"></param>
        public static void Set(string selected, List<EnvItem> availableEnvs, string refPaths)
        {
            Set("default", selected, availableEnvs, refPaths); 
        }


        /// <summary>
        /// Set the current environment.
        /// </summary>
        /// <param name="envGroupName">Can have multiple environment definitions.
        /// If you just want to use the default one, supply emtpy string.</param>
        /// <param name="selected">"prod"</param>
        /// <param name="availableEnvs">All the available environments.</param>
        public static void Set(string envGroupName, string selected, List<EnvItem> availableEnvs, string refPaths)
        {
            // Check if refpaths(configs) were supplied.
            bool hasConfigs = !string.IsNullOrEmpty(refPaths);
            bool hasMultiConfigs = hasConfigs && refPaths.Contains(",");
            string[] configs = hasConfigs ? refPaths.Split(',') : new string[] { };

            // Multiple env selection indicating inheritance.
            // e.g. prod,qa indicates prod inherits from qa.
            bool isEnvInherited = selected.Contains(",");
            string[] envsSelected = isEnvInherited ? selected.Split(',') : new string[] { selected };
            selected = envsSelected[0];

            IEnv envDef = new EnvDef(availableEnvs, selected);

            // Case 1: Single config file = "prod",  "prod,qa,dev", "prod.config"
            if (configs.Length == 1 && envsSelected.Length == 1)
                envDef.Get(selected).RefPath = refPaths;

            // Case 2: Config Inheritance = "prod",  "prod,qa,dev", "prod.config,dev.config"
            else if (configs.Length > envsSelected.Length || configs.Length < envsSelected.Length)
                envDef.Get(selected).RefPath = refPaths;

            // Case 3: Env & Config Inherit = "prod,qa,dev", "prod,qa,dev", "prod.config,qa.config,dev.config".
            // Apply prod.config to "prod" refpath, qa.config to "qa" refPath.
            else if (configs.Length > 1 && envsSelected.Length > 1 && configs.Length == envsSelected.Length)
                for (int ndx = 0; ndx < envsSelected.Length; ndx++)
                    envDef.Get(envsSelected[ndx]).RefPath = configs[ndx];

            Set(envGroupName, envDef);
        }


        /// <summary>
        /// Register an environment for a specific group and also set the selected environment
        /// within the environment context for that group.
        /// </summary>
        /// <param name="envGroup">e.g. "database"</param>
        /// <param name="ctx">Collection of environments for the <paramref name="envGroup"/></param>
        /// <param name="selectedEnvironment">The selected environment within the Environment context <paramref name="ctx"/></param>
        public static void Set(IEnv env)
        {
            Set("default", env);
        }


        /// <summary>
        /// Register an environment for a specific group and also set the selected environment
        /// within the environment context for that group.
        /// </summary>
        /// <param name="envGroup">e.g. "database"</param>
        /// <param name="ctx">Collection of environments for the <paramref name="envGroup"/></param>
        /// <param name="selectedEnvironment">The selected environment within the Environment context <paramref name="ctx"/></param>
        public static void Set(string envGroup, IEnv env)
        {
            if (string.IsNullOrEmpty(envGroup)) envGroup = "default";

            _environments[envGroup] = env;
            if (envGroup == "default")
            {
                Env.Init(env);
            }
        }


        /// <summary>
        /// Get the current environment.
        /// </summary>
        public static IEnv Current { get { return _environments["default"]; } }


        /// <summary>
        /// Return the environment service the respective environment group.
        /// </summary>
        /// <param name="group">"database"</param>
        /// <returns></returns>
        public static IEnv Get(string group)
        {            
            if (!_environments.ContainsKey(group)) return null;

            return _environments[group];            
        }
    }
}
