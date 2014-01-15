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
using System.Security.Principal;
using CommonLibrary.Security;


namespace CommonLibrary.DomainModel
{
    internal class ServiceSecurityUtils
    {
        /// <summary>
        /// Do security check.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static BoolMessage Check(IActionContext ctx, ILocalizedResourceManager resources)
        {
            // Principal object supplied.
            if (ctx.AuthenticationData != null)
                return CheckAuthorization(ctx, resources);
            
            // Authentication data not supplied and username is empty.
            // Can't do anything.
            if (string.IsNullOrEmpty(ctx.UserName))
            {
                string error = resources.GetString("Authentication_Required", "User name not provided for authentication");
                ctx.Errors.Add(error);
                return new BoolMessage(false, error);
            }

            // Username supplied, return true if roles are not applicable.
            if(string.IsNullOrEmpty(ctx.AuthenticationRoles))
                return BoolMessage.True;

            // Get the auth data from username and do check.
            BoolMessageItem<IPrincipal> result = CheckAuthorizationUsingName(ctx, resources);
            
            // If auth was successful, apply the auth data on the context.
            if (result.Success)
                ctx.AuthenticationData = result.Item;

            return result;
        }


        /// <summary>
        /// Perform the authorization check using the Iprincipal on the action context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static BoolMessage CheckAuthorization(IActionContext ctx, ILocalizedResourceManager resources)
        {
            // Principal object supplied.
            if (ctx.AuthenticationData != null && ctx.AuthenticationData.Identity.IsAuthenticated)
            {
                ctx.UserName = ctx.AuthenticationData.Identity.Name;

                // No roles required to perform this action.
                if (string.IsNullOrEmpty(ctx.AuthenticationRoles))
                    return BoolMessage.True;

                bool isAllowedToPerformAction = RoleUtils.IsInRoles(ctx.AuthenticationRoles, ctx.AuthenticationData);
                string error = isAllowedToPerformAction ? string.Empty : resources.GetString("Authorization_Failed", 
                    "Authorizaion failed, not allowed to perform action per role.");
                
                return new BoolMessage(isAllowedToPerformAction, error);
            }
            return BoolMessage.False;
        }


        /// <summary>
        /// Get the authentication data using using the username provided and perform the 
        /// authenctication.
        /// </summary>
        /// <param name="ctx">The action context.</param>
        /// <param name="resources">Resource manager for errors message.
        /// Can provide the default Resource Manager. 
        /// <see cref="CommonLibrary.LocalizationResourceManagerDefault"/></param>
        /// <returns></returns>
        public static BoolMessageItem<IPrincipal> CheckAuthorizationUsingName(IActionContext ctx, ILocalizedResourceManager resources)
        {            
            // Get the authentication service.
            ISecurityService securityService = IocContainer.GetObject<ISecurityService>("authenticationService");
            if(securityService == null)
                return new BoolMessageItem<IPrincipal>(null, false, resources.GetString("Authentication_NotWorking", "Authorization service is unavailable."));

            // Get the authentication data by username.
            IPrincipal authData = securityService.GetAuthData(ctx.UserName).Item;
            string authMessage = authData == null ? resources.GetString("Authentication_Failed", "Unable to authenticate user : " + ctx.UserName) : string.Empty;
            
            // Authentication failure.
            if( authData == null)
                return new BoolMessageItem<IPrincipal>(null, false, authMessage);

            // Set the auth data on the context.
            BoolMessage result = CheckAuthorization(ctx, resources);
            return new BoolMessageItem<IPrincipal>(authData, result.Success, result.Message);
        }
    }
}
