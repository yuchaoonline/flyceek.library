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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Security.Authentication;

using ComLib.Entities;



namespace ComLib.Membership
{
    /// <summary>
    /// Service class for Account.
    /// </summary>
    public partial class AccountService : EntityService<Account>
    {
        /// <summary>
        /// default construction
        /// </summary>
        public AccountService()
        {
        }


        /// <summary>
        /// Initialize model with only the repository.
        /// </summary>
        /// <param name="repository">Repository for entity.</param>
        public AccountService(IEntityRepository<Account> repository) : base(repository, null, null)
        {
        }


        /// <summary>
        /// Initialize model with repository and settings.
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="settings">Settings</param>
        public AccountService(IEntityRepository<Account> repository, IEntitySettings<Account> settings)
            : base(repository, null, settings)
        {
        }


        /// <summary>
        /// Initialize the model w/ repository, validator, and it's settings.
        /// </summary>
        /// <param name="repository">Repository for the model.</param>
        /// <param name="validator">Validator for model.</param>
        /// <param name="settings">Settings for the model.</param>
        public AccountService(IEntityRepository<Account> repository, IEntityValidator<Account> validator,
                IEntitySettings<Account> settings ) : base(repository, validator, settings)
        {
        }
    }



    /// <summary>
    /// Settings class for Account.
    /// </summary>
    /// <typeparam name="?"></typeparam>
    public partial class AccountSettings : EntitySettings<Account>, IEntitySettings<Account>
    {
        public AccountSettings()
        {
            Init();
        }


        /// <summary>
        /// Initalize settings.
        /// </summary>
        public override void Init()
        {
            EditRoles = "";
            EnableValidation = true;
        }
    }



    /// <summary>
    /// Settings class for Account.
    /// </summary>
    /// <typeparam name="?"></typeparam>
    public partial class AccountResources : EntityResources
    {
        public AccountResources()
        {
            _entityName = "Account";
        }
    }
}
