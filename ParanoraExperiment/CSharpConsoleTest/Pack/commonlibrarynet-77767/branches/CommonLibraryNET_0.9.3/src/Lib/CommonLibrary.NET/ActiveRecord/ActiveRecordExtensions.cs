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
using CommonLibrary.DomainModel;


namespace CommonLibrary
{
    /// <summary>
    /// Active record extensions.
    /// TO_DO: Provide extensions based approach to active record.
    /// May have to consider non-generic base class for IService to accomplish this.
    /// </summary>
    public static class ActiveRecordExtensions
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        public static BoolMessage Save(this IEntity entity)
        {
            return BoolMessage.False;
        }
    }
}
