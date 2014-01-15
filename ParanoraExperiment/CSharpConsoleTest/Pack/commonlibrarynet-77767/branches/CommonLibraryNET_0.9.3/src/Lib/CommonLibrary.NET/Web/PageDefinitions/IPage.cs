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
using System.Web.UI;

using ComLib;
using ComLib.Modules;


namespace ComLib.Web
{
    /// <summary>
    /// Interface for a page.
    /// </summary>
    public interface IPageModuleView
    {
        /// <summary>
        /// The name of the view.
        /// "home.aspx"
        /// </summary>
        string Name { get; }


        /// <summary>
        /// List of errors that occurring during handling of the page load.
        /// </summary>
        IStatusResults Errors { get; }

                
        /// <summary>
        /// Loads a control into the specified container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="control"></param>
        Control LoadModule(Module module);


        /// <summary>
        /// The containers that hold the modules.
        /// </summary>
        IDictionary<string, Control> ModuleContainers { get; }


        /// <summary>
        /// Reference to the main module container.
        /// This is typically the body container as in
        /// Header, Left, Right, Body, Footer.
        /// </summary>
        Control ModuleContainerMain { get; }
    }
}
