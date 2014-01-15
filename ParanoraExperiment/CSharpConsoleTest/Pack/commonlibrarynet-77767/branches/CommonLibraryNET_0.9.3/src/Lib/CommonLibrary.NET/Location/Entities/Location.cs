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

using ComLib.Entities;


namespace ComLib.LocationSupport
{
       
    /// <summary>
    /// Location data base class for state, country, location short name.
    /// </summary>
    public class LocationDataBase : EntityPersistant<int>
    {
        /// <summary>
        /// This is the id of the location component.
        /// This actually represents
        /// </summary>        
        public int Id { get; set; }
       

        /// <summary>
        /// Full / Formal name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Short name or abbreviation
        /// </summary>
        public virtual string Abbreviation { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is alias.
        /// </summary>
        /// <value><c>true</c> if this instance is alias; otherwise, <c>false</c>.</value>
        public bool IsAlias { get; set; }


        /// <summary>
        /// Gets or sets the alias ref id.
        /// </summary>
        /// <value>The alias ref id.</value>
        public int AliasRefId { get; set; }
    }



    /// <summary>
    /// Location data with the country id.
    /// </summary>
    public class LocationDataCountryBase : LocationDataBase
    {
        /// <summary>
        /// Country Id
        /// </summary>
        public int CountryId { get; set; }
    }

}
