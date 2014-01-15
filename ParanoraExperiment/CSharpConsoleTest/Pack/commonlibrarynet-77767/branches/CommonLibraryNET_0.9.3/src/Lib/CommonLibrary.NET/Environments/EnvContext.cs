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
    /// Contextual information for environment definitions.
    /// </summary>
    /// <remarks>
    /// networkloc: "z:/env"
    /// localloc:  "c:/env"
    /// conn_dev2: "server:localhost, user=kishore, pass=kishore"
    /// 
    /// env: name=Dev,   source=file, path="${localloc}/dev.config",    Type=Dev,  Inherits="",           DeepInherit=Yes, Ref=""
    /// env: name=Dev2,  source=file, path="${localloc}/dev2.config",   Type=Dev,  Inherits="",           DeepInherit=Yes, Ref=""
    /// env: name=Qa,    source=file, path="${localloc}/qa.config",     Type=Qa,   Inherits="Dev",        DeepInherit=Yes, Ref=""
    /// env: name=Uat,   source=file, path="${localloc}/uat.config",    Type=Uat,  Inherits="Qa",         DeepInherit=Yes, Ref=""
    /// env: name=Prod,  source=file, path="${localloc}/prod.config",   Type=Prod, Inherits="Uat",        DeepInherit=Yes, Ref=""
    /// env: name=Lon,   source=file, path="${localloc}/london.config", Type=Prod, Inherits="Prod",       DeepInherit=Yes, Ref=""
    /// env: name=kish,  source=db,   path="${conn_dev2}",              Type=Dev,  Inherits="Dev2,Prod",  DeepInherit=No,  Ref="myGroupId"
    /// </remarks>    
    public class EnvContext
    {
        /// <summary>
        /// Names of various folders where the config files may reside.
        /// </summary>
        public Dictionary<string, string> Substitutions;


        private List<EnvItem> _envs;
        /// <summary>
        /// Collection of supported different environments.
        /// </summary>
        public List<EnvItem> Envs
        {
            get { return _envs; }
            set
            {
                _envs = value;

                // Store all the environments by their names.
                Dictionary<string, EnvItem> envMap = new Dictionary<string, EnvItem>();
                _envs.ForEach<EnvItem>(e => envMap[e.Name] = e);
                _envsByName = envMap;
            }            
        }


        private Dictionary<string, EnvItem> _envsByName;
        /// <summary>
        /// Dictionary of the environments based on environment name.
        /// </summary>
        public Dictionary<string, EnvItem> EnvsByName
        {
            get { return _envsByName; }
        }
    }
}
