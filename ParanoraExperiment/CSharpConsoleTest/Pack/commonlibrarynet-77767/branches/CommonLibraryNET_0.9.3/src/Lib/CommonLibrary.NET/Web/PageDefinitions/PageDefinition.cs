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
using System.Text;

using ComLib.Modules;


namespace ComLib.Web
{

    /// <summary>
    /// Class that describes what modules are associated w/ this page.
    /// </summary>
    public class PageDefinition
    {
        /// <summary>
        /// The name of the page.
        /// e.g. "Home.aspx"
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The id of the page.
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// The authorization roles for this page.
        /// "Admins;PowerUsers"
        /// </summary>
        public string Roles { get; set; }


        /// <summary>
        /// The list of modules associated with the page.
        /// </summary>
        public IList<Module> Modules { get; set; }


        /// <summary>
        /// Allow default constructor.
        /// </summary>
        public PageDefinition()
        {
            Modules = new List<Module>();
        }
    }
}
