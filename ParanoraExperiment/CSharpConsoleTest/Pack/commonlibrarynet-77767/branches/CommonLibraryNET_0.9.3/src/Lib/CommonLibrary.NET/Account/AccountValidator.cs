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
using ComLib;
using ComLib.Entities;
using ComLib.ValidationSupport;


namespace ComLib.Membership
{
    /// <summary>
    /// Validator for Account
    /// </summary>
    public partial class AccountValidator : EntityValidator<Account>
    {
        /// <summary>
        /// Validation method for the entity.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="useTarget">if set to <c>true</c> [use target].</param>
        /// <param name="results">The results.</param>
        /// <returns></returns>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results; 

            int initialErrorCount = results.Count;
            Account entity = (Account)target;
			Validation.IsStringLengthMatch(entity.SecurityQuestion, true, true, true, 0, 100, results, "SecurityQuestion" );
			Validation.IsStringLengthMatch(entity.SecurityAnswer, true, true, true, 0, 100, results, "SecurityAnswer" );
			Validation.IsStringLengthMatch(entity.Comment, true, true, true, 0, 100, results, "Comment" );
			Validation.IsStringLengthMatch(entity.LockOutReason, true, true, true, 0, 100, results, "LockOutReason" );
			Validation.IsDateWithinRange(entity.LastLoginDate, false, false, DateTime.MinValue, DateTime.MaxValue, results, "LastLoginDate" );
			Validation.IsDateWithinRange(entity.LastPasswordChangedDate, false, false, DateTime.MinValue, DateTime.MaxValue, results, "LastPasswordChangedDate" );
			Validation.IsDateWithinRange(entity.LastPasswordResetDate, false, false, DateTime.MinValue, DateTime.MaxValue, results, "LastPasswordResetDate" );
			Validation.IsDateWithinRange(entity.LastLockOutDate, false, false, DateTime.MinValue, DateTime.MaxValue, results, "LastLockOutDate" );

            return initialErrorCount == results.Count;
        }
    }
}
