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

using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;

using ComLib;
using ComLib.Logging;
using ComLib.Locale;



namespace ComLib.LocationSupport
{
    
    /// <summary>
    /// LocationService parses location data that can be various formats.
    /// The formats can range from city, city/state, city/country, state, country.
    /// </summary>    
    public class LocationService : ILocationService
    {
        //private IZipCodeDao _zipCodeDao;
        private IStateDao _stateDao;
        private ILocationShortNameDao _shortNameDao;
        private ICityDao _cityDao;
        private ICountryDao _countryDao;
        private ILocalizationResourceProvider _resources;


        /// <summary>
        /// Constuctor that also takes in the short name dao.
        /// </summary>
        /// <param name="shortNameDao"></param>
        /// <param name="zipCodeDao"></param>
        /// <param name="stateDao"></param>
        public LocationService(ICountryDao countryDao, IStateDao stateDao, ICityDao cityDao, ILocationShortNameDao shortNameDao)
        {
            Init(shortNameDao, stateDao, cityDao, countryDao);
        }


        #region ILocationService Members
        /// <summary>
        /// Get the localized resources.
        /// </summary>
        public ILocalizationResourceProvider Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }


        /// <summary>
        /// Does a high-level check of the format supplied and determines what type
        /// of location input was supplied.
        /// 
        /// Formats are:
        /// 1. city                         - "Bronx"
        /// 2. city,state                   - "Bronx , New York"
        /// 3. city,state( abbreviation )   - "Bronx , NY"
        /// 4. city,country                 - "HomeTown , USA"
        /// 5. state                        - "New Jersey"
        /// 6. state abbreviation           - "NJ"
        /// 7. country                      - "Italy"
        /// the actuall parsing 
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        public LocationLookUpResult Parse(string locationData)
        {
            // Validate.
            if (IsEmptyLocation(locationData))
            {
                return new LocationLookUpResult(LocationLookUpType.None, false, "Location was not supplied.");
            }

            // Trim any spaces.
            locationData = locationData.Trim();

            // Possible short name : "bronx".
            // So get full name : "bronx,NY"
            //if (_shortNameDao != null)
            //{
            //    locationData = MapPossibleAlias(locationData);
            //}

            return InternalParse(locationData);
        }
        #endregion      


        /// <summary>
        /// Get the possible formal name associated with the 
        /// alias / abbreviation entered.
        /// e.g. 
        /// USA -> United States
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        private string MapPossibleAlias(string locationData)
        {
            // Determine if this is a shortname.
            ShortNameLookUp lookup = _shortNameDao.GetLookUp();
            if (lookup[locationData] == null)
            {
                return locationData;
            }

            return lookup[locationData].Name;
        }


        /// <summary>
        /// Does a high-level check of the format supplied and determines what type
        /// of location input was supplied.
        /// 
        /// Formats are:
        /// 1. city                         - "Bronx"
        /// 2. city,state                   - "Bronx , New York"
        /// 3. city,state( abbreviation )   - "Bronx , NY"
        /// 4. city,country                 - "HomeTown , USA"
        /// 5. state                        - "New Jersey"
        /// 6. state abbreviation           - "NJ"
        /// 7. country                      - "Italy"
        /// the actuall parsing 
        /// </summary>
        /// <param name="locationData">The location to parse. Can be any combination of
        /// inputs, check the summary above.</param>
        /// <returns></returns>
        private LocationLookUpResult InternalParse(string locationData)
        {
            CityLookUp cityLookUp = _cityDao.GetLookUp();
            StatesLookUp stateLookUp = _stateDao.GetLookUp();
            CountryLookUp countryLookUp = _countryDao.GetLookUp();

            try
            {
                bool isValidUSZipCode = IsUnitedStatesZipCode(locationData);
                bool containsComma = isValidUSZipCode ? false : locationData.Contains(",");

                // United states Zip code format
                if ( isValidUSZipCode )
                {
                    return ParseUnitedStatesZipCode(locationData);
                }

                // City, ( State or Country )
                // Comma indicates search by city, <state> or <country>
                if ( containsComma )
                {
                    return LocationParser.ParseCityWithStateOrCountry(cityLookUp, stateLookUp, countryLookUp, locationData);
                }
                
                // Check for city, state, or country. 
                // Start with narrowest search.
                // Check city.
                LocationLookUpResult result = LocationParser.ParseCity(cityLookUp, stateLookUp, countryLookUp, locationData);
                if ( result != LocationLookUpResult.Empty && result.IsValid )
                {
                    return result;
                }

                // Check State - 2nd most restrictive search.
                result = LocationParser.ParseState(stateLookUp, countryLookUp, locationData);
                if (result != LocationLookUpResult.Empty && result.IsValid)
                {
                    return result;                        
                }

                // Check country - 3rd and final search criteria                 
                result = LocationParser.ParseCountry(countryLookUp, locationData);
                if (result != LocationLookUpResult.Empty && result.IsValid)
                {
                    return result;                        
                }                                
            }
            catch (Exception ex)
            {
                // Some error during parsing.
                // Log the sucker into our system.
                Logger.Error("Error verifying search location", ex);
            }

            return new LocationLookUpResult(LocationLookUpType.None, false, "Unable to determine location");
        }


        /// <summary>
        /// Checks whether the location text supplied is empty.
        /// This trims any white space from both sides of the text before checking
        /// for empty string.
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        private bool IsEmptyLocation(string locationData)
        {
            // Validate.
            if (string.IsNullOrEmpty(locationData))
            {
                return true;
            }
            locationData = locationData.Trim();
            if (locationData == string.Empty)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines if the location text supplied is a U.S. zipcode.
        /// This is true if text is 5 characters that are all numbers.
        /// </summary>
        /// <param name="locationText">e.g. "10465"</param>
        /// <returns></returns>
        private bool IsUnitedStatesZipCode(string locationText)
        {
            if (locationText.Length == LocationConstants.ZipCodeLength)
            {
                int zipcodeParsed = 0;
                if (int.TryParse(locationText, out zipcodeParsed))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Parse the zipcode representing a United States Zipcode.
        /// This must be a 5 digit zipcode.
        /// e.g. "10465"
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        private LocationLookUpResult ParseUnitedStatesZipCode(string zip)
        {
            LocationLookUpResult result;
            if (zip.Length == 5)
            {
                for(int ndx = 0; ndx < zip.Length; ndx++)
                {
                    if (!Char.IsDigit(zip, ndx))
                    {
                        result = new LocationLookUpResult(LocationLookUpType.Zip, false, "U.S. Zip code format is not valid.");
                        result.Zip = zip;
                        result.CountryId = LocationConstants.CountryId_USA;
                        return result;
                    }
                }
            }

            result = new LocationLookUpResult(LocationLookUpType.Zip, true, string.Empty);
            result.Zip = zip;
            result.CountryId = LocationConstants.CountryId_USA;
            return result;
        }


        private void Init(ILocationShortNameDao shortNameDao, IStateDao stateDao, ICityDao cityDao, ICountryDao countryDao)
        {
            _shortNameDao = shortNameDao;
            //_zipCodeDao = zipCodeDao;
            _stateDao = stateDao;
            _cityDao = cityDao;
            _countryDao = countryDao;
        }
    }
}