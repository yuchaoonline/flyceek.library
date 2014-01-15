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
using System.Collections;
using System.Collections.Specialized;

using ComLib;
using ComLib.Entities;
using ComLib.ValidationSupport;


namespace ComLib.LocationSupport
{
    /// <summary>
    /// Validates the state/zip code combinations.
    /// </summary>
    public class LocationRule : ValidatorBase
    {
        StatesLookUp _stateLookup;
        CountryLookUp _countryLookup;

        private string _state;
        private int _stateId;
        private int _countryId;
        private string _country;
        private string _city;
        private string _zip;
        private bool _isOnline;


        /// <summary>
        /// Location rule inputs.
        /// </summary>
        /// <param name="zipcodeDao"></param>
        /// <param name="stateId"></param>
        /// <param name="city"></param>
        /// <param name="zip"></param>
        /// <param name="isOnline"></param>
        public LocationRule(StatesLookUp statesLookup, CountryLookUp countryLookup,
            int countryId, int stateId, string city, string zip, bool isOnline)
        {
            _isOnline = isOnline;
            _stateId = stateId;
            _state = string.Empty;
            _countryId = countryId;
            _country = string.Empty;
            _city = city;
            _zip = zip;
            _stateLookup = statesLookup;
            _countryLookup = countryLookup;
        }


        /// <summary>
        /// Initalize using the Address object.
        /// </summary>
        /// <param name="statesLookup"></param>
        /// <param name="countryLookup"></param>
        /// <param name="address"></param>
        /// <param name="isOnline"></param>
        public LocationRule(StatesLookUp statesLookup, CountryLookUp countryLookup, Address address, bool isOnline)
        {
            Init(statesLookup, countryLookup, address, isOnline);
        }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="statesLookup"></param>
        /// <param name="countryLookup"></param>
        /// <param name="address"></param>
        /// <param name="isOnline"></param>
        public void Init(StatesLookUp statesLookup, CountryLookUp countryLookup, Address address, bool isOnline)
        {
            _isOnline = isOnline;
            _stateId = address.StateId;
            _state = address.State;
            _countryId = address.CountryId;
            _country = address.Country;
            _city = address.City;
            _zip = address.Zip;
            _stateLookup = statesLookup;
            _countryLookup = countryLookup;
        }


        /// <summary>
        /// Location rule
        /// </summary>
        /// <param name="address"></param>
        /// <param name="isOnline"></param>
        public LocationRule(Address address)
        {
            IStateDao stateDao = Ioc.GetObject<IStateDao>("stateDao");
            ICountryDao countryDao = Ioc.GetObject<ICountryDao>("countryDao");
            Init(stateDao.GetLookUp(), countryDao.GetLookUp(), address, address.IsOnline);
        }


        /// <summary>
        /// Validate the rule against the data.
        /// </summary>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            object target = validationEvent.Target;
            IValidationResults results = validationEvent.Results; 

            // No need to validate location if online.
            if (_isOnline) { return true; }

            // Clear out errors and keep track of initial error count.
            int initialErrorCount = results.Count;

            // Check for names instead of ids.
            CheckCountryStateNames();

            bool isCountryUSA = _countryId == LocationConstants.CountryId_USA;

            // Check fields, city, stateid, countryId, zip
            ValidationUtils.Validate(string.IsNullOrEmpty(_city), results, "City", "City is required.", null);
            ValidationUtils.Validate(string.IsNullOrEmpty(_zip), results, "Zip", "Zip code is invalid", null);
            ValidationUtils.Validate(_countryId <= 0, results, "Country", "Country is required.", null);
            ValidationUtils.Validate((isCountryUSA && _stateId <= 0), results, "State", "State is not selected", null);

            // Any errors ?
            if (results.Count > initialErrorCount) { return false; }

            // Check Country - State combination.
            bool validStateCountryCombo = LocationUtils.IsValidStateCountryRelation(_stateLookup, _countryLookup, _stateId, _countryId);
            ValidationUtils.Validate(!validStateCountryCombo, results, string.Empty, "State and country not a valid combination.", null);

            // We found the city. Now compare that 
            return results.Count == initialErrorCount;
           
        }


        private void CheckCountryStateNames()
        {
            // Check to see if country name is supplied instead of the id.
            if (_countryId <= 0 && !string.IsNullOrEmpty(_country))
            {
                Country country = _countryLookup[_country];
                if (country != null) _countryId = country.Id;
            }

            // Check to see if the state name was supplied instead of the state id.
            if (_stateId <= 0 && !string.IsNullOrEmpty(_state))
            {
                State state = _stateLookup.FindByCountry(_state, _countryId);
                if (state != null) _stateId = state.Id;
            }
        }
    }
}
