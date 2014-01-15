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


namespace CommonLibrary
{
    /// <summary>
    /// Interface for 
    /// </summary>
    public interface IShellCommand
    {
        /// <summary>
        /// Objects to store the arguments supplied.
        /// </summary>
        object ArgsReciever { get; }

        
        /// <summary>
        /// Validates the args against the args reciever.
        /// </summary>
        /// <param name="commandArgs"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Validate(string[] commandArgs);
        
        
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        BoolMessageItem Execute(string[] args);


        /// <summary>
        /// Get a string showing the usage of this command.
        /// </summary>
        /// <returns></returns>
        string GetUsage();
    }

}
