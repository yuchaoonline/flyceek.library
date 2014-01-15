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
    /// State 
    /// </summary>
    public class State : LocationDataCountryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        public State()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="countryId">The country id.</param>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="stateAbbr">The state abbr.</param>
        public State(int id, string name, int countryId, string countryName, string stateAbbr)
        {
            this.Id = id;
            this.Name = name;
            this.Abbreviation = stateAbbr;
            this.CountryId = countryId;
            this.CountryName = countryName;
            this.IsActive = true;
        }


        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>The name of the country.</value>
        public string CountryName { get; set; }       
    }



    /// <summary>
    /// Class to lookup the states
    /// </summary>
    public class StatesLookUp : LocationLookUpWithCountry<State>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allStates"></param>
        public StatesLookUp(IList<State> allStates) : base( allStates )
        {
        }
    }
}
