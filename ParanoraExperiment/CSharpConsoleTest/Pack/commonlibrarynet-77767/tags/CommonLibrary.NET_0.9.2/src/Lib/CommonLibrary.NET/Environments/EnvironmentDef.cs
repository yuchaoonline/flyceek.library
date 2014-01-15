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
    /// Environment service.
    /// </summary>
    public class EnvironmentDefinition : IEnvironment
    {
        private EnvironmentType _envType;
        private EnvItem _current;
        private EnvironmentContext _ctx;
        private List<EnvItem> _inheritedChainedEnvs;


        /// <summary>
        /// Event handler when the active environment changes ( e.g. from Prod to Qa.
        /// </summary>
        public event EventHandler OnEnvironmentChange;


        /// <summary>
        /// Initializes this environment definition with the 
        /// collection of the environments(Prod, Qa, Dev, etc )
        /// and the selected environment ( Qa )
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="selectedEnvironment"></param>
        public EnvironmentDefinition(EnvironmentContext ctx, string selectedEnvironment)
        {
            Init(ctx, selectedEnvironment);
        }
                

        #region Public Properties
        /// <summary>
        /// The environment context containing all the environments
        /// </summary>
        public EnvironmentContext Context
        {
            get { return _ctx; }
        }


        /// <summary>
        /// The Top most environment in the inheritance chain ).
        /// E.g. If Prod inherits from Qa, Qa inherits from Dev
        /// This is the selected environment among Prod, Qa, Dev.
        /// </summary>
        public EnvItem SelectedEnv
        {
            get { return _current; }
        }


        /// <summary>
        /// List of the inherited environments that make up this environment.
        /// </summary>
        public List<EnvItem> Inheritance
        {
            get { return _inheritedChainedEnvs; }
        }


        /// <summary>
        /// Get the inheritance path, e.g. prod;qa;dev.
        /// </summary>
        public string InheritancePath
        {
            get { return EnvironmentUtils.CollectEnvironmentProps(_inheritedChainedEnvs, ",", env => env.Name); }
        }


        /// <summary>
        /// Provides list of names of available environments than can be selected by user.
        /// </summary>
        public List<string> SelectableEnvs
        {
            get { return EnvironmentUtils.GetSelectableEnvironments(_ctx); }
        }


        /// <summary>
        /// Return the current environment type.
        /// </summary>
        public EnvironmentType EnvType { get { return _envType; } }


        /// <summary>
        /// Is production.
        /// </summary>
        public bool IsProd { get { return _envType == EnvironmentType.Prod; } }


        /// <summary>
        /// Is Qa
        /// </summary>
        public bool IsQa { get { return _envType == EnvironmentType.Qa; } }


        /// <summary>
        /// Is development.
        /// </summary>
        public bool IsDev { get { return _envType == EnvironmentType.Dev; } }


        /// <summary>
        /// Is uat.
        /// </summary>
        public bool IsUat { get { return _envType == EnvironmentType.Uat; } }
        #endregion


        #region Public Methods
        /// <summary>
        /// Initalize using context and selected environment name.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="envName"></param>
        public void Init(EnvironmentContext ctx, string envName)
        {
            _ctx = ctx;
            Change(envName);
        }


        /// <summary>
        /// Get all the environments that are part of the EnvironmentContext
        /// for this definition.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<EnvItem> GetAll()
        {
            return new ReadOnlyCollection<EnvItem>(_ctx.Envs);
        }


        /// <summary>
        /// Set the current environment.
        /// </summary>
        /// <param name="envName"></param>
        public void Change(string envName)
        {
            if (!_ctx.EnvsByName.ContainsKey(envName)) throw new ArgumentException("Environment " + envName + " does not exist.");

            // set the current environment.
            _current = _ctx.EnvsByName[envName];
            Load();

            // Notify.
            if (OnEnvironmentChange != null)
                OnEnvironmentChange(this, new EventArgs());
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Loads all the environments.
        /// </summary>
        private void Load()
        {
            // Interpret the environments by
            // replacing ${ref} to the actual value.
            // Also for recursive inheritance, get all the environments.
            // and put them into a list.
            EnvironmentUtils.DoSubstitutions(_ctx);
            _inheritedChainedEnvs = EnvironmentUtils.LoadInheritance(_current, _ctx);

            // This could be a single envitem in the context.
            // new EnvItem(){ Name = "Prod",       Source = "File", Path = @"prod.config,qa.config,dev.config", 
            //                DeepInherit = true,  EnvType = Prod,  InheritPath = "Qa,Dev" },

            if (_inheritedChainedEnvs == null || _inheritedChainedEnvs.Count == 0)
            {
                _envType = _current.EnvType;
                _inheritedChainedEnvs = new List<EnvItem>() { _current };
            }
            else if (_inheritedChainedEnvs != null && _inheritedChainedEnvs.Count > 0)
            {
                _envType = EnvironmentUtils.GetEnvironmentType(_inheritedChainedEnvs);
            }
        }
        #endregion
    }

}
