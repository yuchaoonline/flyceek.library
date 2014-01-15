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


namespace CommonLibrary
{
    /// <summary>
    /// Used to stored the environment for static access.
    /// </summary>
    public class Environments
    {
        private static IDictionary<string, IEnvironment> _environments = new Dictionary<string, IEnvironment>();


        /// <summary>
        /// Initialize w/ the selected environment and config paths.
        /// </summary>
        /// <param name="selectedEnvironment"></param>
        /// <param name="configPaths"></param>
        public static void Init(string selectedEnvironment, string configPaths)
        {
            // Validate.
            Guard.IsFalse(string.IsNullOrEmpty(selectedEnvironment), "Must provide the selected environment.");
            List<List<string>> bothEnvsAndConfigs = EnvironmentUtils.Parse(selectedEnvironment, configPaths);
            List<string> envsSelected = bothEnvsAndConfigs[0];
            List<string> configs = bothEnvsAndConfigs[1];
            bool hasConfigs = configs.Count > 0;

            // Create the environment.
            string firstEnv = envsSelected[0];
            EnvItem env = new EnvItem(firstEnv, "file", false, EnvironmentUtils.GetEnvType(firstEnv), "", "");
            env.Path = hasConfigs ? configs[0] : "";
            List<EnvItem> envs = new List<EnvItem>() { env };

            // More than 1 supplied. This represents an inheritance path.
            if (envsSelected.Count > 1)
            {
                for (int ndx = 1; ndx < envsSelected.Count; ndx++)
                {
                    string envName = envsSelected[ndx];
                    string configPath = configs[ndx];
                    var newEnv = new EnvItem(envName, "file", false, EnvironmentUtils.GetEnvType(envName), "", configPath);
                    envs.Add(newEnv);

                    // Append the inheritance path to the first one.
                    envs[0].InheritPath += (string.IsNullOrEmpty(env.InheritPath)) ? envName : "," + envName;                    
                }
            }
            else if (envsSelected.Count == 1 && configs.Count > 1)
            {
                for(int ndx = 1; ndx < configs.Count; ndx++)
                    env.Path += "," + configs[ndx];
            }

            // Init now via the context.
            EnvironmentContext ctx = new EnvironmentContext() { Envs = envs };
            Init("default", ctx, firstEnv);

            // Set the current default environment.
            Init(Get("default"));
        }


        /// <summary>
        /// Register the environment context and the selected environment.
        /// </summary>
        public static void Init(EnvironmentContext ctx, string selectedEnvironment)
        {
            Init("default", ctx, selectedEnvironment);
        }


        /// <summary>
        /// Register an environment for a specific group and also set the selected environment
        /// within the environment context for that group.
        /// </summary>
        /// <param name="envGroup">e.g. "database"</param>
        /// <param name="ctx">Collection of environments for the <paramref name="envGroup"/></param>
        /// <param name="selectedEnvironment">The selected environment within the Environment context <paramref name="ctx"/></param>
        public static void Init(string envGroup, EnvironmentContext ctx, string selectedEnvironment)
        {
            // Initalize the environment with the specified environment
            // passed on the command line.
            // e.g. -env:Prod
            IEnvironment env = new EnvironmentDefinition(ctx, selectedEnvironment);
            _environments[envGroup] = env;

        }


        /// <summary>
        /// Registers the environment supplied as the current environment by 
        /// setting up the static accessor EnvironmentCurrent.
        /// </summary>
        /// <param name="env"></param>
        public static void Init(IEnvironment env)
        {
            _environments["default"] = env;
            EnvironmentCurrent.Init(env);
        }


        /// <summary>
        /// Return the environment service the respective environment group.
        /// </summary>
        /// <param name="group">"database"</param>
        /// <returns></returns>
        public static IEnvironment Get(string group)
        {
            if (!_environments.ContainsKey(group)) return null;

            return _environments[group];            
        }
    }
}
