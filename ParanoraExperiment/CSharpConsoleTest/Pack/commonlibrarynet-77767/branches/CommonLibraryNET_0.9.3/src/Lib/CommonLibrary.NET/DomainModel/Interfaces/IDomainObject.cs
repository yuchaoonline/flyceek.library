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
    /// Interface for any domain object.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IDomainObject<T> : IEntity where T : IEntity
    {       
        /// <summary>
        /// Gets the validator.
        /// </summary>
        /// <value>The validator.</value>
        IEntityValidator<T> Validator { get; set; }
    }



    /// <summary>
    /// Hybrid of DomainObject with Active record support.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IActiveRecordDomainObject<T> : IDomainObject<T>, IActiveRecordBase<T> where T : IEntity
    {
    }
}
