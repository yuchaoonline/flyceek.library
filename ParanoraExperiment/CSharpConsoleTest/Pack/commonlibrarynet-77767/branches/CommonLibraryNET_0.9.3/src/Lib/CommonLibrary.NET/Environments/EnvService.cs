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
    /// Environment service.
    /// </summary>
    public class EnvService
    {
        private EnvType _envType;
        private EnvItem _current;
        private EnvContext _ctx;
        private List<EnvItem> _inheritedChainedEnvs;

        public event EventHandler OnEnvironmentChange;


        /// <summary>
        /// Initalize using context and selected environment name.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="envName"></param>
        public void Init(EnvContext ctx, string envName)
        {
            _ctx = ctx;
            Set(envName);
        }


        /// <summary>
        /// The environment context containing all the environments
        /// </summary>
        public EnvContext Context
        {
            get { return _ctx; }
        }



        /// <summary>
        /// The current environment.
        /// </summary>
        public EnvItem Env
        {
            get { return _current; }
        }


        /// <summary>
        /// Full inheritance chain.
        /// </summary>
        public List<EnvItem> Inheritance
        {
            get { return _inheritedChainedEnvs; }
        }


        /// <summary>
        /// Set the current environment.
        /// </summary>
        /// <param name="envName"></param>
        public void Set(string envName)
        {
            if (!_ctx.EnvsByName.ContainsKey(envName)) throw new ArgumentException("Environment " + envName + " does not exist.");                
            
            // set the current environment.
            _current = _ctx.EnvsByName[envName];
            Load();

            // Notify.
            if (OnEnvironmentChange != null)
                OnEnvironmentChange(this, new EventArgs());
        }


        /// <summary>
        /// Get all the environments.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<EnvItem> GetAll()
        {
            return new ReadOnlyCollection<EnvItem>(_ctx.Envs);
        }


        #region Public Properties        
        /// <summary>
        /// Return the current environment type.
        /// </summary>
        public EnvType EnvType { get { return _envType; } }


        /// <summary>
        /// Is production.
        /// </summary>
        public bool IsProd { get { return _envType == EnvType.Prod; } }


        /// <summary>
        /// Is Qa
        /// </summary>
        public bool IsQa { get { return _envType == EnvType.Qa; } }


        /// <summary>
        /// Is development.
        /// </summary>
        public bool IsDev { get { return _envType == EnvType.Dev; } }


        /// <summary>
        /// Is uat.
        /// </summary>
        public bool IsUat { get { return _envType == EnvType.Uat; } }
        #endregion


        /// <summary>
        /// Loads all the environments.
        /// </summary>
        private void Load()
        {
            // Interpret the environments by
            // replacing ${ref} to the actual value.
            // Also for recursive inheritance, get all the environments.
            // and put them into a list.
            EnvUtils.DoSubstitutions(_ctx);
            _inheritedChainedEnvs = EnvUtils.LoadInheritance(_current, _ctx);
            _envType = EnvUtils.GetEnvironmentType(_inheritedChainedEnvs);
        }
    }

}
