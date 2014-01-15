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
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ComLib;
using ComLib.Entities;
using ComLib.Database;

using ComLib.Entities;



namespace ComLib.Membership
{
    /// <summary>
    /// Generic repository for persisting Account.
    /// </summary>
    public partial class AccountRepository : RepositorySql<Account>  //
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedQueryRepository"/> class.
        /// </summary>
        public AccountRepository() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TId, T&gt;"/> class.
        /// </summary>
        /// <param name="connectionInfo">The connection info.</param>
        /// <param name="helper">The helper.</param>
        public AccountRepository(ConnectionInfo connectionInfo) : base(connectionInfo)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;TId, T&gt;"/> class.
        /// </summary>
        /// <param name="connectionInfo">The connection info.</param>
        /// <param name="helper">The helper.</param>
        public AccountRepository(ConnectionInfo connectionInfo, IDBHelper helper)
            : base(connectionInfo, helper)
        {
        }
    }


    
    /// <summary>
    /// RowMapper for Account.
    /// </summary>
    /// <typeparam name="?"></typeparam>
    public partial class AccountRowMapper : EntityRowMapper<Account>, IEntityRowMapper<Account>
    {
        public override Account MapRow(IDataReader reader, int rowNumber)
        {
            Account entity = Accounts.New();
            entity.Id = reader["Id"] == DBNull.Value ? 0 : (int)reader["Id"];
            entity.CreateDate = reader["CreateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["CreateDate"];
            entity.UpdateDate = reader["UpdateDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["UpdateDate"];
            entity.CreateUser = reader["CreateUser"] == DBNull.Value ? string.Empty : reader["CreateUser"].ToString();
            entity.UpdateUser = reader["UpdateUser"] == DBNull.Value ? string.Empty : reader["UpdateUser"].ToString();
            entity.UpdateComment = reader["UpdateComment"] == DBNull.Value ? string.Empty : reader["UpdateComment"].ToString();
            entity.Version = reader["Version"] == DBNull.Value ? 0 : (int)reader["Version"];
            entity.IsActive = reader["IsActive"] == DBNull.Value ? false : (bool)reader["IsActive"];
            entity.UserName = reader["UserName"] == DBNull.Value ? string.Empty : reader["UserName"].ToString();
            entity.UserNameLowered = reader["UserNameLowered"] == DBNull.Value ? string.Empty : reader["UserNameLowered"].ToString();
            entity.Password = reader["Password"] == DBNull.Value ? string.Empty : reader["Password"].ToString();
            entity.Email = reader["Email"] == DBNull.Value ? string.Empty : reader["Email"].ToString();
            entity.EmailLowered = reader["EmailLowered"] == DBNull.Value ? string.Empty : reader["EmailLowered"].ToString();
            entity.SecurityQuestion = reader["SecurityQuestion"] == DBNull.Value ? string.Empty : reader["SecurityQuestion"].ToString();
            entity.SecurityAnswer = reader["SecurityAnswer"] == DBNull.Value ? string.Empty : reader["SecurityAnswer"].ToString();
            entity.IsApproved = reader["IsApproved"] == DBNull.Value ? false : (bool)reader["IsApproved"];
            entity.IsLockedOut = reader["IsLockedOut"] == DBNull.Value ? false : (bool)reader["IsLockedOut"];
            entity.Comment = reader["Comment"] == DBNull.Value ? string.Empty : reader["Comment"].ToString();
            entity.LockOutReason = reader["LockOutReason"] == DBNull.Value ? string.Empty : reader["LockOutReason"].ToString();
            entity.LastLoginDate = reader["LastLoginDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["LastLoginDate"];
            entity.LastPasswordChangedDate = reader["LastPasswordChangedDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["LastPasswordChangedDate"];
            entity.LastPasswordResetDate = reader["LastPasswordResetDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["LastPasswordResetDate"];
            entity.LastLockOutDate = reader["LastLockOutDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["LastLockOutDate"];

            return entity;
        }
    }
}