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
    /// Class to represent the currently selected environment.
    /// This is just a provider pattern.
    /// </summary>
    public class EnvironmentCurrent
    {
        private static IEnvironment _current;


        /// <summary>
        /// Event handler for an environment change.
        /// </summary>
        public static event EventHandler OnChange;


        /// <summary>
        /// Initialize with current environment.
        /// </summary>
        /// <param name="environment"></param>
        public static void Init(IEnvironment environment)
        {
            _current = environment;
            _current.OnEnvironmentChange += new EventHandler(Current_OnEnvironmentChange);
        }
        

        #region Public Properties
        /// <summary>
        /// The current default environment.
        /// </summary>
        public static IEnvironment Selected
        {
            get { return _current; }
        }


        /// <summary>
        /// Provides list of names of available environments than can be selected by user.
        /// </summary>
        public static List<string> SelectableEnvs
        {
            get { return _current.SelectableEnvs; }
        }
        
        
        /// <summary>
        /// Name of current envionment.
        /// </summary>
        public static string Name
        {
            get { return _current.SelectedEnv.Name; }
        }


        /// <summary>
        /// Inheritance list of environments.
        /// Prod->Qa->Dev
        /// </summary>
        public static List<EnvItem> Inheritance
        {
            get { return _current.Inheritance; }
        }


        /// <summary>
        /// Inheritance path ( prod,qa,dev ).
        /// </summary>
        public static string InheritancePath
        {
            get { return _current.InheritancePath; }
        }


        /// <summary>
        /// Inheritance path ( prod,qa,dev ).
        /// </summary>
        public static string ConfigPath
        {
            get 
            {                 
                string paths = EnvironmentUtils.CollectEnvironmentProps(_current.Inheritance, ",", env => env.Path);
                paths = paths.Replace("/", "\\");
                return paths;
            }
        }


        /// <summary>
        /// Is production.
        /// </summary>
        public static bool IsProd { get { return _current.IsProd; } }


        /// <summary>
        /// Is Qa
        /// </summary>
        public static bool IsQa { get { return _current.IsQa; } }


        /// <summary>
        /// Is development.
        /// </summary>
        public static bool IsDev { get { return _current.IsDev; } }


        /// <summary>
        /// Is uat.
        /// </summary>
        public static bool IsUat { get { return _current.IsUat; } }


        /// <summary>
        /// The environment type (prod, qa, etc ) of current selected environment
        /// </summary>
        public static EnvironmentType EnvType { get { return _current.EnvType; } }
        #endregion


        /// <summary>
        /// Change the environment.
        /// </summary>
        /// <param name="environmentName"></param>
        public static  void Change(string environmentName)
        {
            _current.Change(environmentName);
        }


        /// <summary>
        /// Notifiy environment changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Current_OnEnvironmentChange(object sender, EventArgs e)
        {
            if (OnChange != null)
                OnChange(null, e);
        }

    }

}
