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


namespace CommonLibrary
{
    
    /// <summary>
    /// Persistant object that is auditable.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public abstract class EntityPersistantAudtiable<TId> : EntityPersistant<TId>, IEntityAuditable
    {
        #region IEntityAuditable Members
        /// <summary>
        /// Create datetime.
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        /// User who first created this entity.
        /// </summary>
        public string CreateUser { get; set; }


        /// <summary>
        /// Update date time
        /// </summary>
        public DateTime UpdateDate { get; set; }


        /// <summary>
        /// User updating the value
        /// </summary>
        public string UpdateUser { get; set; }


        /// <summary>
        /// Indicates whether or not this entity is active,
        /// meaning that it is just stored in the database for historic
        /// record keeping/versioning.
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// The version number of the persistant entity.
        /// </summary>
        public int Version { get; set; }


        /// <summary>
        /// Comment to describe any updates to entity.
        /// </summary>
        public string UpdateComment { get; set; }
        #endregion
    }
}
