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


namespace ComLib.Entities
{
    /// <summary>
    /// Persistant object that is auditable.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class DomainObject<T> : Entity, IDomainObject<T> where T : IEntity
    {
        protected IEntityValidator<T> _validator = null;
        

        #region IDomainObject<TId,T> Members
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>The validator.</value>
        public IEntityValidator<T> Validator
        {
            get { return _validator; }
            set { _validator = value; }
        }
        #endregion


        /// <summary>
        /// Validate this entity.
        /// </summary>
        /// <returns></returns>
        public IValidationResults Validate()
        {
            return _validator.ValidateTarget(this);
        }

        /// <summary>
        /// Settings for the entity.
        /// </summary>
        public IEntitySettings<T> Settings { get; set; }       
    }



    /// <summary>
    /// Domain object that can be rated/bookmarked.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainObjectRateable<T> : DomainObject<T> where T : IEntity
    {
        /// <summary>
        /// Average rating for the entity.
        /// </summary>
        public double AverageRating { get; set; }


        /// <summary>
        /// Total number of people that liked the entity.
        /// </summary>
        public int TotalLiked { get; set; }


        /// <summary>
        /// Total number of people that disliked this entity.
        /// </summary>
        public int TotalDisLiked { get; set; }


        /// <summary>
        /// Total number of people that bookmarked this post.
        /// </summary>
        public int TotalBookMarked { get; set; }


        /// <summary>
        /// Total number of reports against this entity.
        /// </summary>
        public int TotalAbuseReports { get; set; }
    }
}
