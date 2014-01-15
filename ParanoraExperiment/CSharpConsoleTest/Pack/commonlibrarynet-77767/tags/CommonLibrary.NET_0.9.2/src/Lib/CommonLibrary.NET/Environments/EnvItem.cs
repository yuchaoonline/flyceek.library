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
    /// Represents a single environment.
    /// </summary>
    /// <remarks>
    /// networkloc: "z:/env"
    /// localloc:   "c:/env"
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
    public class EnvItem
    {
        /// <summary>
        /// Dev or Qa, Name that uniquely identifies the environment.
        /// </summary>
        public string Name;


        /// <summary>
        /// Source, e.g. file or database.
        /// e.g. file | database
        /// </summary>
        public string Source;


        /// <summary>
        /// Path to file or reference name to connection string
        /// if database.
        /// e.g. "qa.xml" or "connectionStr_QA" where
        /// connectionstring_QA is a string which actually contains
        /// the connection info.
        /// This is left to developer to decide how to handle the refernces.
        /// </summary>
        public string Path;
        
        
        /// <summary>
        /// Environmental Inheritance path.
        /// e.g. Uat. 
        /// if this environment name is Prod.
        /// and this inherit path is "Uat;CommonDev;"
        /// then this will load Uat backed by CommonDev settings.
        /// </summary>
        public string InheritPath;


        /// <summary>
        /// e.g. Related to InheritPath.
        /// If Inherits from Env "UAT", this setting of true
        /// will also load all the dependent inherited files of UAT.
        /// </summary>
        public bool DeepInherit;


        /// <summary>
        /// Type environment type for this environment.
        /// </summary>
        public EnvironmentType EnvType;


        /// <summary>
        /// Whether or not this is a selectable "Concrete" environment that 
        /// a user can choose from. Similar to abstract/concrete classes.
        /// I.e. Prod_NY might be a selectable environment, but
        /// Prod_Shared might not be as it may be a common envrionment
        /// that concrete environments like "prod_ny", "prod_london" inherit from.
        /// </summary>
        public bool IsSelectable;


        /// <summary>
        /// An environment configuration can be setup to NOT point
        /// to any specific configuration on its OWN but simply
        /// inhert EVERYTHING from it's parents.
        /// </summary>
        /// <returns></returns>
        public bool IsPurelyInherited()
        {
            return string.IsNullOrEmpty(Path) && !string.IsNullOrEmpty(InheritPath);
        }


        /// <summary>
        /// Default construction.
        /// </summary>
        public EnvItem() { }


        /// <summary>
        /// Initialize with the supplied values.
        /// </summary>
        /// <param name="name">"prod"</param>
        /// <param name="source">"file | db "</param>
        /// <param name="deepInherit"></param>
        /// <param name="envType">"Prod | Qa | Uat | Dev"</param>
        public EnvItem(string name, string source, bool deepInherit, EnvironmentType envType, string inheritPath, string configPath)
        {
            Name = name;
            Source = source;
            DeepInherit = deepInherit;
            EnvType = envType;
            InheritPath = inheritPath;
            Path = configPath;
        }
    }
}
