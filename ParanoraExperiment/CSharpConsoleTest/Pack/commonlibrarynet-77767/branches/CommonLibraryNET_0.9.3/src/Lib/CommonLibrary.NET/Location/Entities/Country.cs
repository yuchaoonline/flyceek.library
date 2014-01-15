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


namespace ComLib.LocationSupport
{
    /// <summary>
    /// Summary description for CountryDetails
    /// </summary>
    public class Country : LocationDataBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="countryCode">The country code.</param>
        public Country(int id, string name, string countryCode)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            IsActive = true;
        }

        
        /// <summary>
        /// Same as abbreviation. This is more descriptive.
        /// </summary>
        public string CountryCode
        {
            get { return Abbreviation; }
            set { Abbreviation = value; }
        }
    }



    /// <summary>
    /// Lookup countries by name or id.
    /// </summary>
    public class CountryLookUp : LocationLookUp<Country>
    {
        /// <summary>
        /// Initialize the lookup
        /// </summary>
        /// <param name="countries"></param>
        public CountryLookUp(IList<Country> countries)
            : base(countries)
        {
        }


        /// <summary>
        /// Returns the country by id.
        /// Always returns the real country if the id supplied refers to an alias.
        /// </summary>
        /// <param name="abbreviationOrName"></param>
        /// <returns></returns>        
        public override Country this[int id]
        {
            get
            {
                Country country = base[id];
                if (country == null) return null;

                // If this is not alias just return it.
                if (!country.IsAlias) { return country; }

                // Return the real one this alias refers to.
                return this[country.AliasRefId];
            }
        }


        /// <summary>
        /// Returns the country by abbreviation or name.
        /// Always returns the real country if abbreviation or name
        /// supplied is an alias.
        /// </summary>
        /// <param name="abbreviationOrName"></param>
        /// <returns></returns>
        public override Country this[string abbreviationOrName]
        {
            get
            {
                Country country = base[abbreviationOrName];
                if (country == null) return null;

                // If this is not alias just return it.
                if (!country.IsAlias) { return country; }

                // Return the real one this alias refers to.
                return this[country.AliasRefId];
            }
        }


        /// <summary>
        /// Initializes the indexes with the items.
        /// </summary>
        /// <param name="allItems"></param>
        protected override void Initialize(IList<Country> allItems)
        {
            foreach (Country item in allItems)
            {
                // Only stores the id's of countries that are not alias's
                if (!item.IsAlias)
                    _allItemsById[item.Id] = item;

                string lowerCaseAbbr = item.Abbreviation.Trim().ToLower();
                string lowerCaseFullName = item.Name.Trim().ToLower();

                // Associate with first one.
                if (!_allItemsByAbrrOrName.ContainsKey(lowerCaseFullName))
                    _allItemsByAbrrOrName[lowerCaseFullName] = item;

                if (!_allItemsByAbrrOrName.ContainsKey(lowerCaseAbbr))
                    _allItemsByAbrrOrName[lowerCaseAbbr] = item;
            }
        }
    }
}