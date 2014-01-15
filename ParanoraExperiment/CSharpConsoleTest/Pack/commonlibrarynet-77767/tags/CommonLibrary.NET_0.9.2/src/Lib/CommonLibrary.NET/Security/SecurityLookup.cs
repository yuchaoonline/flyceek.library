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


namespace CommonLibrary.Security
{
    /// <summary>
    /// Static security lookup class.
    /// </summary>
    public class SecurityLookup
    {
        private static ISecurityLookup _provider;


        /// <summary>
        /// Initialize the provider.
        /// </summary>
        /// <param name="provider"></param>
        public static void Init(ISecurityLookup provider)
        {
            _provider = provider;
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
        /// Is User in the selected roles.
        /// </summary>
        /// <param name="rolesDelimited"></param>
        /// <returns></returns>
        public static bool IsUserInRoles(string rolesDelimited)
        {
            return _provider.IsUserInRoles(rolesDelimited);
        }


        /// <summary>
        /// Determine whether or not the currently logged in user is an admin.
        /// </summary>
        public static bool IsAdmin()
        {
            return _provider.IsAdmin();
        }
    }
}
