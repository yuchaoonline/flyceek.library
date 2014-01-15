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


namespace ComLib.LocationSupport
{

    /// <summary>
    /// Interface for looking up data by id or name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILocationLookUp<T>
    {
        T this[int id] { get; }
        T this[string name] { get; }
    }



    /// <summary>
    /// Storage and quick lookup class for location data.
    /// Provides 2 types of indexes.
    /// 
    /// 1. Index by location name.
    /// 2. Index by location id
    /// </summary>
    public abstract class LocationLookUp<T> : ILocationLookUp<T> where T : LocationDataBase
    {
        protected IDictionary<int, T> _allItemsById;
        protected IDictionary<string, T> _allItemsByAbrrOrName;


        /// <summary>
        /// Generic based lookup.
        /// </summary>
        /// <param name="allItems"></param>
        public LocationLookUp(IList<T> allItems)
        {
            _allItemsById = new Dictionary<int, T>();
            _allItemsByAbrrOrName = new Dictionary<string, T>();
            Initialize(allItems);
        }


        /// <summary>
        /// Returns the location item given the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T this[int id]
        {
            get 
            {
                // Check.
                if (!_allItemsById.ContainsKey(id))
                    return null;

                return _allItemsById[id]; 
            }
        }


        /// <summary>
        /// Returns the location item given the full name ("New York") or abbr ( "NY" )
        /// </summary>
        /// <param name="abbreviationOrName"></param>
        /// <returns></returns>
        public virtual T this[string abbreviationOrName]
        {
            get 
            {
                // Check.
                if (string.IsNullOrEmpty(abbreviationOrName))
                    return null;

                // Keys always stored in lowercase.
                string lowerCaseName = abbreviationOrName.Trim().ToLower();

                // Check.
                if (!_allItemsByAbrrOrName.ContainsKey(lowerCaseName))
                    return null;

                return _allItemsByAbrrOrName[lowerCaseName];
            }
        }


        /// <summary>
        /// Initialize the internal lookup tables with the items.
        /// Store them by id and name.
        /// </summary>
        /// <param name="allItems"></param>
        protected virtual void Initialize(IList<T> allItems)
        {
            foreach (T item in allItems)
            {
                // Only allows the first item ( if alias exists ) to 
                // be stored.
                if (!_allItemsById.ContainsKey(item.Id))
                    _allItemsById[item.Id] = item;

                string lowerCaseAbbr = item.Abbreviation.Trim().ToLower();
                string lowerCaseFullName = item.Name.Trim().ToLower();

                // Associate with first one.
                // TO_DO:Kishore: There is a potential bug here that should be fixed.
                // It's possible that the abbreviation could be the Full Name of the location.
                // And vice versa.
                if (!_allItemsByAbrrOrName.ContainsKey(lowerCaseFullName))
                    _allItemsByAbrrOrName[lowerCaseFullName] = item;

                if (!_allItemsByAbrrOrName.ContainsKey(lowerCaseAbbr))
                    _allItemsByAbrrOrName[lowerCaseAbbr] = item;
            }
        }
    }



    /// <summary>
    /// Class to provide fast lookup for location components (cities and states)
    /// that have a country id.
    /// The base class provides lookup by 
    /// 1. id.
    /// 2. name.
    /// 3. name and countryid.
    /// 
    /// This class extends the lookup by also being able to lookup
    /// a city by country id.
    /// </summary>
    /// <remarks>
    /// Instead of storing another set of indexes for cityname, countryId
    /// This only stores the cityname, countryId
    /// for duplicate city names.
    /// </remarks>
    public class LocationLookUpWithCountry<T> : LocationLookUp<T> where T : LocationDataCountryBase
    {
        private IDictionary<string, T> _itemsByCountryId;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allStates"></param>
        public LocationLookUpWithCountry(IList<T> allItems)
            : base(allItems)
        {
        }


        /// <summary>
        /// Initialize the lookup by :
        /// 1. searching by id
        /// 2. searching by name
        /// 3. searching by name and countryid.
        /// </summary>
        /// <param name="allItems"></param>
        protected override void Initialize(IList<T> allItems)
        {
            _itemsByCountryId = new Dictionary<string, T>();

            // Now store by name and countryId.
            foreach (T item in allItems)
            {
                // Store items by id.
                if (!_allItemsById.ContainsKey(item.Id))
                    _allItemsById[item.Id] = item;

                // Get item abbr and name.
                string lowerCaseAbbr = string.IsNullOrEmpty( item.Abbreviation) ? string.Empty : item.Abbreviation.Trim().ToLower();
                string lowerCaseFullName = string.IsNullOrEmpty( item.Name ) ? string.Empty : item.Name.Trim().ToLower();
                                
                // Add name if it doesn't exist.
                bool containsName = _allItemsByAbrrOrName.ContainsKey(lowerCaseFullName);                
                if ( !containsName )
                {
                    _allItemsByAbrrOrName[lowerCaseFullName] = item;
                }

                // Add abbr if it doesn't exist
                bool containsAbbr = _allItemsByAbrrOrName.ContainsKey(lowerCaseAbbr);
                if (!containsAbbr)
                {
                    _allItemsByAbrrOrName[lowerCaseAbbr] = item;
                }

                // Only store if the existing index ( by name ) is already used.
                // This is a duplicate.
                // E.g. Same city name but different country id. 
                // so store it.
                if ( containsName )
                {
                    string fullnameWithCountryKey = BuildKey(item.Name, item.CountryId);
                    _itemsByCountryId[fullnameWithCountryKey] = item;
                }
                if (containsAbbr)
                {
                    string abbrWithCountryKey = BuildKey(item.Abbreviation, item.CountryId);
                    _itemsByCountryId[abbrWithCountryKey] = item;
                }
            }
        }


        /// <summary>
        /// Finds the city based on the country id.
        /// This is because there can be two countries with the same city name.
        /// e.g. City, Country
        ///      1. GeorgeTown, USA
        ///      2. GeorgeTown, Guyana
        /// </summary>
        /// <param name="name"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public T FindByCountry(string name, int countryId)
        {
            T item = this[name];

            // Check if the city stored just by the name
            // is the same one being searched.
            if (item != null && item.CountryId == countryId)
            {
                return item;
            }

            // Now check the cityname_countryId indexes stored.
            string key = BuildKey(name, countryId);
            if (!_itemsByCountryId.ContainsKey(key)) return null;

            return _itemsByCountryId[key];
        }      


        protected string BuildKey(string name, int id)
        {
            return name.Trim().ToLower() + "_" + id;
        }
    }
}
