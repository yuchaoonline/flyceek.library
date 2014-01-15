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

using ComLib.Entities;


namespace ComLib.LocationSupport
{

    /// <summary>
    /// Country dao
    /// </summary>
    public interface ICountryDao : IRepository<Country>
    {
        /// <summary>
        /// Get country look up component to find countries 
        /// easily by id or name.
        /// </summary>
        /// <returns></returns>
        CountryLookUp GetLookUp();


        /// <summary>
        /// Retrieves all the active countries.
        /// </summary>
        /// <returns></returns>
        IList<Country> RetrieveAllActive();
    }



    /// <summary>
    /// State dao
    /// </summary>
    public interface IStateDao : IRepository<State>
    {
        /// <summary>
        /// Get the states by country id
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        IList<State> FindByCountry(int countryId);


        /// <summary>
        /// Get state lookup
        /// </summary>
        /// <returns></returns>
        StatesLookUp GetLookUp();


        /// <summary>
        /// Get the state by name or abbreviation
        /// </summary>
        /// <param name="stateFullNameOrAbbreviation"></param>
        /// <returns></returns>
        //State Retrieve(string stateFullNameOrAbbreviation);
    }



    /// <summary>
    /// Country dao
    /// </summary>
    public interface ICityDao : IRepository<City>
    {
        /// <summary>
        /// Get country look up component to find countries 
        /// easily by id or name.
        /// </summary>
        /// <returns></returns>
        CityLookUp GetLookUp();


        /// <summary>
        /// Retrieve by country
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        IList<City> FindByCountry(int countryId);


        /// <summary>
        /// Retrieve by country and state
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        IList<City> FindByCountryState(int countryId, int stateId);
    }



    /// <summary>
    /// Interface for getting location short names.
    /// </summary>
    public interface ILocationShortNameDao : IRepository<LocationShortName>
    {
        /// <summary>
        /// Retrieves the list of location short names.
        /// </summary>
        /// <returns></returns>
        ShortNameLookUp GetLookUp();
    }



    /// <summary>
    /// Zip code dao.
    /// </summary>
    public interface IZipCodeDao
    {
        ZipCodeLookUpResult GetZipCodes(int countryId, string zipCode);

        /// <summary>
        /// Validate a zip code based on a numeric state abbreviation.
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        bool IsValidZipCode(int countryId, string stateAbbreviation, string zipCode);


        /// <summary>
        /// Validate a zipcode based on country, stateid,
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="stateId"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        bool IsValidZipCode(int countryId, int stateId, string zipCode);
    }    
}
