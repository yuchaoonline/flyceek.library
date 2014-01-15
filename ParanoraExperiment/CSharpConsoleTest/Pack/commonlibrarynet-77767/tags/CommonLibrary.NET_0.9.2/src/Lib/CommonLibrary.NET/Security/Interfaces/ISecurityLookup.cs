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
using System.Security.Principal;


namespace CommonLibrary.Security
{

    /// <summary>
    /// Class for security related functionality.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Determines whether the user is authenticted.
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();


        /// <summary>
        /// Return whether or not the current user is authenticated.
        /// </summary>
        /// <returns></returns>
        bool IsGuest();


        /// <summary>
        /// The name of the current user.
        /// </summary>
        string UserName { get; }


        /// <summary>
        /// Get the current user.
        /// </summary>
        IPrincipal User { get; }


        /// <summary>
        /// Is User in the selected roles.
        /// </summary>
        /// <param name="rolesDelimited"></param>
        /// <returns></returns>
        bool IsUserInRoles(string rolesDelimited);


        /// <summary>
        /// Determine whether or not user is an admin.
        /// </summary>
        /// <returns></returns>
        bool IsAdmin();
    }
}
