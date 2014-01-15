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
using System.Web;
using System.Security.Principal;
using System.Web.Security;


namespace ComLib.Authentication
{
    /// <summary>
    /// Static security service provider class.
    /// </summary>
    public class Auth
    {
        private static IAuth _provider = new AuthWin();


        /// <summary>
        /// Default to windows.
        /// </summary>
        static Auth()
        {
            Init(new AuthWin());
        }


        /// <summary>
        /// Initialize the provider.
        /// </summary>
        /// <param name="provider"></param>
        public static void Init(IAuth provider)
        {
            _provider = provider;
        }


        /// <summary>
        /// Return whether or not the current user is authenticated.
        /// </summary>
        /// <returns></returns>
        public static bool IsAuthenticated()
        {
            return _provider.IsAuthenticated();
        }


        /// <summary>
        /// Return whether or not the current user is authenticated.
        /// </summary>
        /// <returns></returns>
        public static bool IsGuest()
        {
            return _provider.IsGuest();
        }


        /// <summary>
        /// The name of the current user.
        /// </summary>
        public static string UserName
        {
            get { return _provider.UserName; }
        }


        /// <summary>
        /// Provides just the username if the username contains
        /// the domain.
        /// e.g. returns "john" if username is "mydomain\john"
        /// </summary>
        public static string UserShortName
        {
            get { return _provider.UserShortName; }
        }


        /// <summary>
        /// Get the current user.
        /// </summary>
        public static IPrincipal User
        {
            get { return _provider.User; }
        }


        /// <summary>
        /// Get the user data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static T GetUser<T>(string userName) where T : class, IPrincipal
        {
            if (_provider == null)
                throw new ArgumentException("AuthenticationService is not initialized.");

            return _provider.GetUser<T>(userName);
        }


        /// <summary>
        /// Get the user data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static T GetUser<T>() where T : class, IPrincipal
        {
            if (_provider == null)
                throw new ArgumentException("AuthenticationService is not initialized.");
            if (_provider.IsGuest())
                throw new ArgumentException("Must be authenticated in order to get login data with name.");

            return _provider.GetUser<T>(User.Identity.Name);
        }


        /// <summary>
        /// Is User in the selected roles.
        /// </summary>
        /// <param name="rolesDelimited"></param>
        /// <returns></returns>
        public static bool IsUserInRoles(string rolesDelimited)
        {
            if (rolesDelimited == null)
                return true;
            return false;
            //return _provider.IsUserInRoles(rolesDelimited);
        }


        /// <summary>
        /// Is user in the list of roles specified.
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsUserInRoles(ReadOnlyCollection<string> roles)
        {
            if (roles == null || roles.Count == 0)
                return false;

            IPrincipal user = _provider.User;

            foreach (string role in roles)
            {
                if (user.IsInRole(role))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Determine whether or not the currently logged in user is an admin.
        /// </summary>
        public static bool IsAdmin()
        {
            return _provider.IsAdmin();
        }


        /// <summary>
        /// Sign the user in.
        /// </summary>
        /// <param name="user"></param>
        public static void SignIn(IPrincipal user)
        {
            _provider.SignIn(user);
        }


        /// <summary>
        /// Sign the user in via username.
        /// </summary>
        /// <param name="user"></param>
        public static void SignIn(string user)
        {
            _provider.SignIn(user);
        }


        /// <summary>
        /// Sign the user in via username.
        /// </summary>
        /// <param name="user"></param>
        public static void SignIn(string user, bool rememberUser)
        {
            _provider.SignIn(user, rememberUser);
        }


        /// <summary>
        /// Signout the user.
        /// </summary>
        public static void SignOut()
        {
            _provider.SignOut();
        }
    }
}
