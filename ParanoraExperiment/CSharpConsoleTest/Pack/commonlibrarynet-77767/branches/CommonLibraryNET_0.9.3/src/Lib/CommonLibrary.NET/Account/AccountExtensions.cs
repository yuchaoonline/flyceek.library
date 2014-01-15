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
using ComLib;
using ComLib.Cryptography;
using ComLib.Authentication;

using ComLib.Entities;
using ComLib.ValidationSupport;


namespace ComLib.Membership
{
    /// <summary>
    /// Account entity extensions.
    /// 1. Setup the password encryption 
    /// 2. Handle lowering the case for username, email etc.
    /// </summary>
    public partial class Account : DomainObject<Account>
    {
        protected string _password;
        protected string _username;
        protected string _usernameLowered;
        protected string _email;
        protected string _emailLowered;
        protected bool _isPasswordEncrypted = false;


        /// <summary>
        /// Default constructor
        /// </summary>
        public Account() { }

        
        /// <summary>
        /// Initialize using username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        public Account(string username, string password, string email, bool isPlainPassword)
        {
            UserName = username;
            if (isPlainPassword)
                SetPassword(password);
            else
                Password = password;
            Email = email;
        }


        /// <summary>
        /// Sets a plain text password as encrypted.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }        


        /// <summary>
        /// Sets a plain text password as encrypted.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        public string UserName
        {
            get { return _username; }
            set { _username = value; _usernameLowered = _username.ToLower(); }
        }


        /// <summary>
        /// Sets a plain text password as encrypted.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        public string UserNameLowered
        {
            get { return _usernameLowered; }
            set
            {
                if (string.Compare(_username, value, true) == 0)
                    _usernameLowered = value; 
            }
        }


        /// <summary>
        /// Sets a plain text password as encrypted.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        public string Email
        {
            get { return _email; }
            set { _email = value; _emailLowered = _email.ToLower(); }
        }


        /// <summary>
        /// Sets a plain text password as encrypted.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        public string EmailLowered
        {
            get { return _emailLowered; }
            set 
            {
                if (string.Compare(_email, value, true) != 0)
                    _emailLowered = value;
            }
        }


        /// <summary>
        /// Sets the password directly, this assumes that the password provided
        /// is already encrypted.
        /// </summary>
        /// <param name="encryptedPassword"></param>
        public void SetEncryptedPassword(string encryptedPassword)
        {
            Password = encryptedPassword;
        }


        /// <summary>
        /// Converts the plain text password to an encrypted password.
        /// </summary>
        /// <param name="plainText"></param>
        public void SetPassword(string plainText)
        {
            Password = Crypto.Encrypt(plainText);
        }
    }



    /// <summary>
    /// Account service extension to handle data massaging for the dates.
    /// </summary>
    public partial class AccountService
    {
        #region VerifyUser, Logon, ChangePassword, etc.
        /// <summary>
        /// Verify that this is a valid user.
        /// </summary>
        /// <param name="userName">"kishore"</param>
        /// <param name="password">"password"</param>
        /// <returns></returns>
        public BoolMessageItem<Account> VerifyUser(string userName, string password)
        {
            // Check inputs.
            BoolMessage inputCheck = Validation.AreNoneNull(new string[] { userName, password }, new string[] { "UserName", "Password" }, null, null);
            if (!inputCheck.Success) return new BoolMessageItem<Account>(null, false, inputCheck.Message);

            IList<Account> accounts = GetByFilter("UserNameLowered = '" + userName.ToLower() + "'").Item;

            // Check for matching records.
            if (accounts == null || accounts.Count == 0)
                return new BoolMessageItem<Account>(null, false, "No accounts were found with username: " + userName);

            // Check password.
            string encryptedPassword = Crypto.Encrypt(password);
            bool isPasswordMatch = string.Compare(encryptedPassword, accounts[0].Password, false) == 0;
            string message = isPasswordMatch ? string.Empty : "Password supplied does not match";
            
            return new BoolMessageItem<Account>(accounts[0], isPasswordMatch, message);
        }


        /// <summary>
        /// Try logging in to server.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public BoolMessage LogOn(string userName, string password, bool rememberUser)
        {
            // Check inputs.
            BoolMessage inputCheck = Validation.AreNoneNull(new string[] { userName, password }, new string[] { "UserName", "Password" }, null, null);
            if (!inputCheck.Success) return inputCheck;

            BoolMessageItem<Account> result = VerifyUser(userName, password);
            if (result.Success) Auth.SignIn(result.Item.UserName, rememberUser);

            return result;
        }


        /// <summary>
        /// Register the user.
        /// </summary>
        /// <param name="userName">"kishore"</param>
        /// <param name="email">"kishore@abc.com"</param>
        /// <param name="password">password</param>
        /// <param name="confirmPassword">Must be same as password, to confirm password</param>
        /// <returns></returns>
        public BoolResult<Account> Create(string userName, string email, string password, string confirmPassword)
        {
            // Check inputs.
            BoolMessage inputCheck = Validation.AreNoneNull(
                    new string[] { userName, email, password, confirmPassword },
                    new string[] { "UserName", "Email", "Password", "ConfirmPassword" }, null, null);
            if (!inputCheck.Success) return new BoolResult<Account>(null, false, inputCheck.Message, null, null);

            // Check password.
            BoolMessage passwordCheck = ValidatePasswords(password, confirmPassword);
            if (!passwordCheck.Success) return new BoolResult<Account>(null, false, passwordCheck.Message, null, null);

            Account account = Accounts.New();
            account.UserName = userName;
            account.Email = email;
            account.Password = password;

            // Create the user.
            BoolResult<Account> result = Accounts.Create(account);
            return result;
        }


        /// <summary>
        /// Change the current password.
        /// </summary>
        /// <param name="userName">username of the account for which the password is being changed.</param>
        /// <param name="currentPassword">Existing password on file.</param>
        /// <param name="newPassword">New password</param>
        /// <param name="confirmPassword">Confirm the password. must be same as new password.</param>
        /// <returns></returns>
        public BoolResult<Account> ChangePassword(string userName, string currentPassword, string newPassword, string confirmPassword)
        {
            // Check inputs.
            BoolMessage inputCheck = Validation.AreNoneNull(
                    new string[] { userName, currentPassword, newPassword, confirmPassword },
                    new string[] { "UserName", "Current Password", "New Password", "Confirm Password" }, null, null);
            if (!inputCheck.Success) return new BoolResult<Account>(null, false, inputCheck.Message, null, null);

            // Check that new password and password confirmation are same.
            BoolMessage passwordCheck = ValidatePasswords(newPassword, confirmPassword);
            if (!passwordCheck.Success) return new BoolResult<Account>(null, false, passwordCheck.Message, null, null);

            // Get existing account.
            Account account = Accounts.GetByFilter("UserNameLowered = '" + userName.ToLower() + "'").Item[0];
            if (account == null) return new BoolResult<Account>(null, false, "Unable to find account with matching username : " + userName, null, null);

            // Check that password supplied matches whats on file.
            string encryptedPassword = Crypto.Encrypt(currentPassword);
            passwordCheck = ValidatePasswords(encryptedPassword, account.Password);
            if (!passwordCheck.Success) return new BoolResult<Account>(null, false, passwordCheck.Message, null, null);
           
            // Now change password.
            account.SetPassword(newPassword);
            return Accounts.Update(account);
        }
        #endregion


        #region Validation methods
        /// <summary>
        /// Return a new instance of a validator. This is neccessary.
        /// </summary>
        /// <returns></returns>
        public override IEntityValidator<Account> GetValidator()
        {
            return new AccountValidator();
        }


        /// <summary>
        /// Get list of data massagers for the entity.
        /// </summary>
        /// <returns></returns>
        protected override void PerformBeforeValidation(IActionContext ctx, EntityAction entityAction)
        {
            List<IEntityMassager> massagers = new List<IEntityMassager>() { new AccountMassager() };
            MassageData(ctx, entityAction, massagers);
        }


        /// <summary>
        /// Override the validation to handle check for existing accounts with same 
        /// username or email address.
        /// </summary>
        /// <param name="ctx">The action context.</param>
        /// <returns></returns>
        protected override BoolResult<Account> PerformValidation(IActionContext ctx, EntityAction entityAction)
        {
            if (!Settings.EnableValidation)
                return BoolResult<Account>.True;

            bool validationResult = true;
            if (entityAction == EntityAction.Delete)
            {
                // No logic for now.
            }
            if (entityAction == EntityAction.Create || entityAction == EntityAction.Update)
            {
                // Validate the actual entity data members.
                IEntityValidator<Account> validator = this.GetValidator();
                validationResult = validator.Validate(ctx.Item, ctx.Errors);
                Account entity = ctx.Item as Account;

                // Check for username / email duplicates.
                if (entityAction == EntityAction.Create)
                {
                    // Check for duplicate usernames.
                    IList<Account> userNameDups = this.GetByFilter("UserNameLowered = '" + entity.UserNameLowered + "'").Item;
                    IList<Account> emailDups = this.GetByFilter("EmailLowered = '" + entity.EmailLowered + "'").Item;
                    if (userNameDups != null && userNameDups.Count > 0)
                    {
                        ctx.Errors.Add("Username : " + entity.UserName + " is already taken.");
                        validationResult = false;
                    }
                    if (emailDups != null && emailDups.Count > 0)
                    {
                        ctx.Errors.Add("Email : " + entity.EmailLowered + " is already taken.");
                        validationResult = false;
                    }
                }
                else if (entityAction == EntityAction.Update)
                {
                    ctx.Id = entity.Id;
                    Account entityBeforeUpdate = Get(ctx).Item;
                    if (string.Compare(entityBeforeUpdate.UserName, entity.UserName, true) != 0)
                        ctx.Errors.Add("Can not change username");
                }

                // Make sure password is encrypted.
                try { string decrypted = Crypto.Decrypt(entity.Password); }
                catch (Exception)
                {
                    ctx.Errors.Add("Password was not encrypted. Encrypt using method SetPassword(plainText);");
                    validationResult = false;
                }
            }

            // Now append all the errors.
            if (!validationResult)
            {
                if (ctx.CombineMessageErrors)
                {
                    string errorMessage = ValidationUtils.BuildSingleErrorMessage(ctx.Errors, Environment.NewLine);
                    return new BoolResult<Account>(null, false, errorMessage, ValidationResults.Empty, StatusResults.Empty);
                }
            }
            return BoolResult<Account>.True;
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// Validate passwords are same.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordConfirmation"></param>
        /// <returns></returns>
        public static BoolMessage ValidatePasswords(string password, string passwordConfirmation)
        {
            // Check password.
            if (string.Compare(password, passwordConfirmation, false) != 0)
            {
                string error = "Password and Password Confirmation do not match.";
                return new BoolMessage(false, error);
            }
            return BoolMessage.True;
        }
        #endregion
    }



    /// <summary>
    /// Data massager for the account entity.
    /// Set all the dates so they are saved correctly to database during persistance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class AccountMassager : EntityMassager
    {
        #region IEntityMassager<T> Members
        /// <summary>
        /// Massage the entity data given the entity action.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public override void Massage(object entity, EntityAction action)
        {
            Account account = entity as Account;
            if ( action == EntityAction.Create )
            {
                account.LastLockOutDate = DateTime.Today;
                account.LastLoginDate = DateTime.Today;
                account.LastPasswordChangedDate = DateTime.Today;
                account.LastPasswordResetDate = DateTime.Today;
            }
        }
        #endregion
    }



    /// <summary>
    /// Extends the functionality for Accounts, simplies use of service
    /// via static access to methods.
    /// </summary>
    public partial class Accounts
    {
        /// <summary>
        /// Provide a static method for verifying a user is registerd/exists.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BoolMessage VerifyUser(string username, string password)
        {
            AccountService service = ActiveRecordRegistration.GetService<Account>() as AccountService;
            return service.VerifyUser(username, password);            
        }


        /// <summary>
        /// Try logging in to server.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static BoolMessage LogOn(string userName, string password, bool rememberUser)
        {
            AccountService service = ActiveRecordRegistration.GetService<Account>() as AccountService;
            return service.LogOn(userName, password, rememberUser);
        }


        /// <summary>
        /// Register the user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public static BoolResult<Account> Create(string userName, string email, string password, string confirmPassword)
        {
            AccountService service = ActiveRecordRegistration.GetService<Account>() as AccountService;
            return service.Create(userName, email, password, confirmPassword);
        }


        /// <summary>
        /// Change the current password.
        /// </summary>
        /// <param name="userName">username of the account for which the password is being changed.</param>
        /// <param name="currentPassword">Existing password on file.</param>
        /// <param name="newPassword">New password</param>
        /// <param name="confirmPassword">Confirm the password. must be same as new password.</param>
        /// <returns></returns>
        public static BoolResult<Account> ChangePassword(string userName, string currentPassword, string newPassword, string confirmPassword)
        {
            AccountService service = ActiveRecordRegistration.GetService<Account>() as AccountService;
            return service.ChangePassword(userName, currentPassword, newPassword, confirmPassword);
        }
    }



    /// <summary>
    /// Create other settings for account services.
    /// </summary>
    public partial class AccountSettings
    {
        /// <summary>
        /// Minimum password length.
        /// </summary>
        public int MinimumPasswordLength { get; set; }
    }
}
