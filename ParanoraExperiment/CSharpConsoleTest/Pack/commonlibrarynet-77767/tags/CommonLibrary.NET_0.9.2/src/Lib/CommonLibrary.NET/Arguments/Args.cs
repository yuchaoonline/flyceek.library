
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
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.ObjectModel;


namespace CommonLibrary
{

   /// <summary>
    /// Class to store the arguments parsed.
    /// </summary>
    public class Args
    {
        /// <summary>
        /// Collection of named arguments.
        /// e.g. If "-config:prod.xml -date:T-1 MyApplicationId" is supplied to command line.
        /// 
        /// NamedArgs["config"] = "prod.xml"
        /// NamedArgs["date"] = "T-1"
        /// </summary>
        public Dictionary<string, string> NamedArgs = new Dictionary<string, string>();


        /// <summary>
        /// Collection of un-named arguments supplied to command line.
        /// e.g. If "-config:prod.xml -date:T-1 MyApplicationId" is supplied to command line.
        /// 
        /// ArgsList[0] = "MyApplicationId"
        /// </summary>
        public List<string> ArgsList = new List<string>();


        /// <summary>
        /// Default construction.
        /// </summary>
        public Args() { }


        /// <summary>
        /// Initialize all members.
        /// </summary>
        /// <param name="namedArgs"></param>
        /// <param name="args"></param>
        /// <param name="success"></param>
        /// <param name="errors"></param>
        /// <param name="errorMessage"></param>
        public Args(Dictionary<string, string> namedArgs, List<string> args)
        {
            NamedArgs = namedArgs;
            ArgsList = args;
        }
    }
}
