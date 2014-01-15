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
    /// Interface for parsing components of some location.
    /// e.g. City, city/state, country, zip.
    /// </summary>
    public interface ILocationLookUpParser
    {
        /// <summary>
        /// Parses string for either the zip or city/state.
        /// e.g.
        ///     City:  "Bronx", "Stamford"
        ///     State: "NY", "NJ", "California"
        ///     CityState: "Queens,New York"
        ///     Country: "USA"
        /// </summary>
        /// <param name="locationData"></param>
        /// <returns></returns>
        LocationLookUpResult Parse(string text);
    }


    /// <summary>
    /// Location service to parse locatiion data.
    /// </summary>
    public interface ILocationService : ILocationLookUpParser
    {
    }
}
