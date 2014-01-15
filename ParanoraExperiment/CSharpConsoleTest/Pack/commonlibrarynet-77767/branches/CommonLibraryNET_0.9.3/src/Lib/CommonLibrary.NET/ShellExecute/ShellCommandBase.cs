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
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ComLib;
using ComLib.Arguments;


namespace CommonLibrary
{
    /// <summary>
    /// Shell execute base class.
    /// </summary>
    public class ShellCommandBase
    {
        protected object _programArgs;
        protected DirectoryInfo _rootDirectory;


        /// <summary>
        /// Reciever of the args.
        /// </summary>
        public object ArgsReciever
        {
            get { return _programArgs; }
        }


        /// <summary>
        /// Validate args usage
        /// </summary>
        /// <returns></returns>
        public bool Validate(string[] commandArgs)
        {
            BoolMessageItem argsResult = Args.Parse(commandArgs, "-", ":", _programArgs);
            if (!argsResult.Success)
            {
                String usage = Args.Build(_programArgs);
                Console.WriteLine(argsResult.Message);
                Console.WriteLine(usage);                
                return false;
            }
            return true;
        }


        /// <summary>
        /// Get the usage of this command.
        /// </summary>
        /// <returns></returns>
        public string GetUsage()
        {
            string usage = Args.Build(_programArgs);
            return usage;
        }


        /// <summary>
        /// Initialize the starting/root directory for this command.
        /// </summary>
        /// <param name="rootDir"></param>
        public void InitializeDir(string rootDir)
        {
            string location = Assembly.GetExecutingAssembly().Location;
            FileInfo info = new FileInfo(location);
            _rootDirectory = info.Directory;
            Console.WriteLine(location);
            if (!string.IsNullOrEmpty(rootDir) && rootDir != ".")
            {
                _rootDirectory = new DirectoryInfo(rootDir);
            }
        }
    }
}
