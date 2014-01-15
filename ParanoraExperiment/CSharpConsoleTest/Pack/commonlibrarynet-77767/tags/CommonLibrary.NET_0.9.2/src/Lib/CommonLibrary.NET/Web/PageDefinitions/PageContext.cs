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

using CommonLibrary.Modules;


namespace CommonLibrary.Web
{
    /// <summary>
    /// PageDefinition Contextual information.
    /// </summary>
    public class PageContext
    {
        /// <summary>
        /// The current page definition that specifies what modules are associated with the page.
        /// </summary>
        public PageDefinition CurrentPage;


        /// <summary>
        /// The lookup for module definitions.
        /// </summary>
        public IDictionary<string, ModuleDefinition> ModuleDefinitions { get; set; }
    }
}
