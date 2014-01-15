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
using System.Data;
using System.Collections.Generic;


namespace ComLib.Entities
{
    /// <summary>
    /// Persistant entity 
    /// </summary>
    public interface IEntityPersistant<TId>
    {
        /// <summary>
        /// Get the id of a persistant entity.
        /// </summary>
        TId Id { get; set; }


        /// <summary>
        /// Determines whether this instance is persistant.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is persistant; otherwise, <c>false</c>.
        /// </returns>
        bool IsPersistant();
    }



    /// <summary>
    /// Auditable entity.
    /// This interface is meant to provide auditing features to
    /// any entity/domain object.
    /// When changing the data model, at times it important to know.
    /// 1. who made a change.
    /// 2. when the change was made.
    /// 3. who created it.
    /// 4. what version it is.
    /// </summary>
    public interface IEntityAuditable
    {
        /// <summary>
        /// Date the entity was created.
        /// </summary>
        DateTime CreateDate { get; set; }


        /// <summary>
        /// User who first created this entity.
        /// </summary>
        string CreateUser { get; set; }


        /// <summary>
        /// Date the entitye was updated.
        /// </summary>
        DateTime UpdateDate { get; set; }


        /// <summary>
        /// User who last updated the entity.
        /// </summary>
        string UpdateUser { get; set; }


        /// <summary>
        /// Comment used for updates.
        /// </summary>
        string UpdateComment { get; set; }
    }



    /// <summary>
    /// Entity interface.
    /// </summary>
    public interface IEntity : IEntityPersistant<int>, IEntityAuditable
    {
    }
}
