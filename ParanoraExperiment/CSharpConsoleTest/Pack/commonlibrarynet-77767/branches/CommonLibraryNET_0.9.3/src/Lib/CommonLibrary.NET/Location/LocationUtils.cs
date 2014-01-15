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


namespace ComLib.LocationSupport
{
    public class LocationUtils
    {
        /// <summary>
        /// Checks that the state belongs to the country.
        /// </summary>
        /// <param name="stateLookUp"></param>
        /// <param name="countryLookUp"></param>
        /// <param name="stateId"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public static bool IsValidStateCountryRelation(StatesLookUp stateLookUp, CountryLookUp countryLookUp, int stateId, int countryId)
        {
            // indicates online.
            bool isUSA = countryId == LocationConstants.CountryId_USA;

            // If country id is online, disregard check.
            if (countryId == LocationConstants.CountryId_NA_Online) return true;

            // Do not have to select a state for Non-USA countries.
            if (stateId == LocationConstants.StateId_NA_Online && !isUSA) return true;

            // Must select a state for USA.
            if (stateId == LocationConstants.StateId_NA_Online && isUSA) return false;

            State state = stateLookUp[stateId];
            Country country = countryLookUp[countryId];

            // Check combination
            if (state.CountryId == country.Id) { return true; }

            return false;
        }


        /// <summary>
        /// Applies the city id to the address if the city is listed in the system
        /// and has a matching state and country id compared to what was supplied.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cityLookup"></param>
        /// <param name="stateLookup"></param>
        /// <param name="countryLookup"></param>
        public static void ApplyCity(Address address, CityLookUp cityLookup, StatesLookUp stateLookup, CountryLookUp countryLookup)
        {
            address.CityId = LocationConstants.CityId_NA;

            // online( country id = online ) or city is null.
            // So don't change anything.
            if (address.CountryId == LocationConstants.CountryId_NA_Online) { return; }

            // Get the city.
            City city = cityLookup.FindByCountry(address.City, address.CountryId);
                        
            // CASE 1: Unknown city.
            if (city == null) { return; }

            // CASE 2: Matching country and state id's with city.
            // Most like this is for U.S.A cities. Where user has to specify state id.            
            if (city.CountryId == address.CountryId && city.StateId == address.StateId)
            {
                address.CityId = city.Id;
                address.City = city.Name;
                return;
            }

            // CASE 3: NON-U.S. country.
            // State not specified but matched the country specified with the city.
            if (address.StateId == LocationConstants.StateId_NA_Online && city.CountryId == address.CountryId)
            {
                address.StateId = city.StateId;
                address.CityId = city.Id;
                address.City = city.Name;
            }
        }


        /// <summary>
        /// Applies the city id to the address if the city is listed in the system
        /// and has a matching state and country id compared to what was supplied.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="stateLookup"></param>
        /// <param name="countryLookup"></param>
        /// <param name="errors">list of errors to populate if any validation fails.</param>
        public static bool ApplyState(Address address, StatesLookUp stateLookup, IList<string> errors)
        {
            if (address.StateId == LocationConstants.StateId_NA_Online) return true;
            if (address.StateId > 0) return true;

            // Can't determine state by name if it's not supplied.
            if (string.IsNullOrEmpty(address.State)) return false;
            
            int initialErrorCount = errors.Count;

            // Check the state.
            bool isCountryIdEmpty = (address.CountryId <= 0);
            State state = isCountryIdEmpty ? stateLookup[address.State] : stateLookup.FindByCountry(address.State, address.CountryId);
            if( ValidationUtils.Validate(state == null, errors, "Invalid state supplied.") )
            {
                address.StateId = state.Id;
                address.StateAbbr = state.Abbreviation;
                address.State = state.Name;
            }
            
            return initialErrorCount == errors.Count;
        }


        /// <summary>
        /// Applies the city id to the address if the city is listed in the system
        /// and has a matching state and country id compared to what was supplied.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="stateLookup"></param>
        /// <param name="countryLookup"></param>
        /// <param name="errors">list of errors to populate if any validation fails.</param>
        public static bool ApplyCountry(Address address, CountryLookUp countryLookup, IList<string> errors)
        {
            bool isCountryIdEmpty = address.CountryId == LocationConstants.CountryId_NA_Online || address.CountryId == 0;

            if (!isCountryIdEmpty) return true;
            if (string.IsNullOrEmpty(address.Country)) return false;

            int initialErrorCount = errors.Count;
            
            // Check the country.
            Country country = countryLookup[address.Country];
            if (ValidationUtils.Validate(country == null, errors, "Invalid country specified : " + address.Country))
            {
                address.CountryId = country.Id;
            }            

            return initialErrorCount == errors.Count;
        }
    }
}
