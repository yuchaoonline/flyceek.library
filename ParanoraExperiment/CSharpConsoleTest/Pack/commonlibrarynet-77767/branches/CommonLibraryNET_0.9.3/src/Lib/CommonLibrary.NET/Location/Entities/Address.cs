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
    public class Address
    {
        private string _street;
        private string _city;
        private int _cityId;
        private string _state;
        private string _zip;
        private string _stateAbbr;
        private int _stateId;
        private int _countryId;
        private string _countryName;
        private static readonly Address _empty;
        private bool _isOnline;


        static Address()
        {
            Address empty = new Address();
            empty.DefaultToEmpty();
            _empty = empty;
        }


        /// <summary>
        /// Empty address.
        /// </summary>
        public static Address Empty
        {
            get { return _empty; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        {
        }


        /// <summary>
        /// Initalize with data.
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="stateAbbr"></param>
        /// <param name="zip"></param>
        public Address(string street, string city, string state, string stateAbbr, string zip)
        {
            Set(street, city, state, stateAbbr, zip, string.Empty);
        }


        /// <summary>
        /// Set the various fields of the address.
        /// </summary>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="stateAbbr"></param>
        /// <param name="zip"></param>
        public void Set(string street, string city, string state, string stateAbbr, string zip, string country)
        {
            _street = street;
            _city = city;
            _stateAbbr = stateAbbr;
            _state = state;
            _zip = zip;
            _countryName = country;
        }


        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        /// <value>The city id.</value>
        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }


        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        /// <value>The state id.</value>
        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }


        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        /// <value>The country id.</value>
        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }


        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }


        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }


        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }


        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country
        {
            get { return _countryName; }
            set { _countryName = value; }
        }


        /// <summary>
        /// Gets or sets the state abbr.
        /// </summary>
        /// <value>The state abbr.</value>
        public string StateAbbr
        {
            get { return _stateAbbr; }
            set { _stateAbbr = value; }
        }


        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }


        /// <summary>
        /// Is Online.
        /// </summary>
        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline = value; }
        }


        /// <summary>
        /// Default to not applicable for on-line classes.
        /// </summary>
        public void DefaultToNotApplicable()
        {
            _street = string.Empty;
            _city = string.Empty;
            _countryId = LocationConstants.CountryId_NA_Online;
            _zip = LocationConstants.ZipCode_NA_Online;
            _stateId = LocationConstants.StateId_NA_Online;
            _cityId = LocationConstants.CityId_NA;
            _state = string.Empty;
            _stateAbbr = string.Empty;           
        }


        /// <summary>
        /// Default to empty
        /// </summary>
        public void DefaultToEmpty()
        {
            _street = string.Empty;
            _city = string.Empty;
            _countryId = LocationConstants.CountryId_USA;
            _zip = string.Empty;
            _stateId = LocationConstants.StateId_NA_Online;
            _cityId = LocationConstants.CityId_NA;
            _state = string.Empty;
            _stateAbbr = string.Empty;
        }


        /// <summary>
        /// Single line format of address.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _street + " " + _city + ", " + _stateAbbr + " " + _zip;
        }
    }
}
