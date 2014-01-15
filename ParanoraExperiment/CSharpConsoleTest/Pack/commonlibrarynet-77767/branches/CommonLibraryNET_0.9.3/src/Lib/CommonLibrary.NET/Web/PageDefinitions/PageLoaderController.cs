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
using System.Web.UI;
using System.Text;
using System.Web.Security;
using System.Collections.Specialized;

using ComLib.Authentication;
using ComLib.ValidationSupport;
using ComLib.Modules;


namespace ComLib.Web
{
    /// <summary>
    /// Controller class for loading modules into the page.
    /// </summary>
    public class PageModuleViewController
    {
        private IPageModuleView _view;
        private IPageModuleDao _dao;


        /// <summary>
        /// Initialize with reference to page.
        /// </summary>
        /// <param name="page"></param>
        public PageModuleViewController(IPageModuleDao dao, IPageModuleView view)
        {
            _view = view;
            _dao = dao;
        }


        /// <summary>
        /// The Pagedefinition for the current page being loaded.
        /// </summary>
        public PageDefinition CurrentPage { get; set; }


        /// <summary>
        /// Process the current request.
        /// </summary>
        public void Process()
        {
            // Check.
            if (CurrentPage == null) return;

            // Check authorization.
            if (!EnsureAccess(CurrentPage)) return;

            // Now load the modules into the page.
            LoadModules(CurrentPage.Modules);
        }


        /// <summary>
        /// Ensures access to the page.
        /// </summary>
        /// <returns></returns>
        private bool EnsureAccess(PageDefinition pageDef)
        {
            // Check for no roles first.
            if (string.IsNullOrEmpty(pageDef.Roles))
                return true;

            if (!Auth.IsUserInRoles(pageDef.Roles))
            {
                _view.Errors.Add("Unauthorized access to page.");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Loads the modules into the view if authorization passes.
        /// </summary>
        /// <param name="modules"></param>
        private void LoadModules(IList<Module> modules)
        {
            // Get reference to view's module containers.
            IDictionary<string, Control> moduleContainers = _view.ModuleContainers;

            // Now go through all the modules and determine if ok to add.
            foreach (Module module in modules)
            {
                // Ensure security.
                string roles = module.Instance.Roles;
                if (string.IsNullOrEmpty(roles) || Auth.IsUserInRoles(module.Instance.Roles))
                {
                    // Have the view load instance of module.
                    Control control = _view.LoadModule(module);
                    IModuleView moduleView = control as IModuleView;

                    if (moduleView != null)
                    {
                        // Set moduleview(UI component) reference to the module.
                        moduleView.Module = module;

                        // Now get it's container name.
                        string containerName = module.Instance.ContainerName.ToLower().Trim();
                        if (moduleContainers.ContainsKey(containerName))
                        {
                            // Add to container.
                            Control container = moduleContainers[containerName];
                            container.Controls.Add(control);
                        }
                    } 
                }
            }
        }

        /*
        /// <summary>
        /// Sorts modules using the <see cref="ModuleOrderComparer"/>
        /// </summary>
        /// <param name="pageDef"></param>
        /// <returns></returns>
        private IList<Module> SortModules(PageDefinition pageDef)
        {
            ((List<Module>)pageDef.Modules).Sort(new ModuleOrderComparer());
            return pageDef.Modules;
        }


        /// <summary>
        /// Comparer used to sort the modules.
        /// </summary>
        class ModuleOrderComparer : IComparer<Module>
        {
            public int Compare(Module mod1, Module mod2)
            {
                int returnValue = 1;
                if (mod1 != null && mod2 != null)
                {
                    returnValue = mod1.Instance.SortOrder.CompareTo(mod2.Instance.SortOrder);
                }

                return returnValue;
            }
        }
        */
    }



    /// <summary>
    /// Controller class for loading modules into the page.
    /// </summary>
    public class PageModuleEditController
    {
        private IPageModuleView _view;
        private IModuleDao _dao;
        private NameValueCollection _params;
        private string _moduleId;


        /// <summary>
        /// Initialize with reference to page.
        /// </summary>
        /// <param name="page"></param>
        public PageModuleEditController(IModuleDao dao, IPageModuleView view, NameValueCollection urlParams)
        {
            _view = view;
            _dao = dao;
            _params = urlParams;
        }


        /// <summary>
        /// Process the current request.
        /// </summary>
        public void Process()
        {
            if (!ValidateParams()) return;

            Module module = _dao.Modules[_moduleId];

            if (!EnsureAccess(module)) return;
                        
            LoadModule(module);
        }


        /// <summary>
        /// Ensures access to the page.
        /// </summary>
        /// <returns></returns>
        private bool EnsureAccess(Module module)
        {
            if (RoleHelper.IsUserInRoles(module.Instance.Roles))
            {
                _view.Errors.Add("Unauthorized access to page.");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Loads the modules into the view if authorization passes.
        /// </summary>
        /// <param name="modules"></param>
        private void LoadModule(Module module)
        {
            string roles = module.Instance.Roles;
            if (string.IsNullOrEmpty(roles) || RoleHelper.IsUserInRoles(module.Instance.Roles))
            {
                Control control = _view.LoadModule(module);
                IModuleView moduleView = control as IModuleView;

                if (moduleView != null)
                {
                    moduleView.Module = module;

                    // Always load to the body container.
                    _view.ModuleContainerMain.Controls.Add(control);
                }
            }
        }


        private bool ValidateParams()
        {
            // Check for module id.
            _moduleId = _params["moduleId"];

            // Ensure module id provided.
            if(!ValidationUtils.Validate(string.IsNullOrEmpty(_moduleId), _view.Errors, "Unable to load module, module id not present."))
                return false;

            if (!ValidationUtils.Validate(!_dao.Modules.ContainsKey(_moduleId), _view.Errors, "Invalid module id"))
                return false;

            return true;
        }
    }

}
