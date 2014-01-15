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
using System.Web;
using System.Security.Principal;
using System.Web.Security;




namespace ComLib.Authentication
{
    /// <summary>
    /// Class to provide simplified access to the web based User(principal) object
    /// exposed in the context.Current.User object.
    /// </summary>
    public class AuthWeb : IAuth
    {
        private string _adminRoleName = "Administrators";
        private Func<string, IPrincipal> _userAuthenticationService = null;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public AuthWeb() { }


        /// <summary>
        /// Initialize with the admin role name.
        /// </summary>
        /// <param name="adminRoleName"></param>
        public AuthWeb(string adminRoleName)
        {
            _adminRoleName = adminRoleName;
        }


        /// <summary>
        /// Initialize with the admin role name.
        /// </summary>
        /// <param name="adminRoleName"></param>
        public AuthWeb(string adminRoleName, Func<string, IPrincipal> userAuth)
        {
            _adminRoleName = adminRoleName;
            _userAuthenticationService = userAuth;
        }


        /// <summary>
        /// Get the current user.
        /// </summary>
        public IPrincipal User
        {
            get { return HttpContext.Current.User; }
        }


        /// <summary>
        /// Get the user data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userName"></param>
        /// <returns></returns>
        public T GetUser<T>(string userName) where T : class, IPrincipal
        {
            if (_userAuthenticationService == null)
            {
               throw new ArgumentException("UserAuthenticationService is not initialized.");
            }
            
            return _userAuthenticationService(userName) as T;
        }


        /// <summary>
        /// The name of the current user.
        /// </summary>
        public string UserName
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }


        /// <summary>
        /// Provides just the username if the username contains
        /// the domain.
        /// e.g. returns "john" if username is "mydomain\john"
        /// </summary>
        public string UserShortName 
        {
            get
            {
                string fullName = HttpContext.Current.User.Identity.Name;
                int ndxSlash = fullName.LastIndexOf(@"\");
                if (ndxSlash == -1)
                    ndxSlash = fullName.LastIndexOf("/");

                if (ndxSlash == -1)
                    return fullName;

                return fullName.Substring(ndxSlash + 1);
            }
        }


        /// <summary>
        /// Determine if the current user is authenticated.
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }


        /// <summary>
        /// Return whether or not the current user is authenticated.
        /// </summary>
        /// <returns></returns>
        public bool IsGuest()
        {
            return !HttpContext.Current.User.Identity.IsAuthenticated;
        }        


        /// <summary>
        /// Determine if currently logged in user is an administrator.
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            if (IsGuest()) return false;

            return Roles.IsUserInRole("Administrators");
        }      


        /// <summary>
        /// Is User in the selected roles.
        /// </summary>
        /// <param name="rolesDelimited"></param>
        /// <returns></returns>
        public bool IsUserInRoles(string rolesDelimited)
        {
            return RoleHelper.IsUserInRoles(rolesDelimited);
        }


        /// <summary>
        /// Sign the user in.
        /// </summary>
        /// <param name="user"></param>
        public void SignIn(IPrincipal user)
        {
            FormsAuthentication.SetAuthCookie(user.Identity.Name, true);
        }


        /// <summary>
        /// Sign the user in.
        /// </summary>
        /// <param name="user"></param>
        public void SignIn(string user)
        {
            FormsAuthentication.SetAuthCookie(user, true);
        }


        /// <summary>
        /// Sign the user in via username.
        /// </summary>
        /// <param name="user"></param>
        public void SignIn(string user, bool rememberUser)
        {
            FormsAuthentication.SetAuthCookie(user, rememberUser);
        }


        /// <summary>
        /// Signout the user.
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
