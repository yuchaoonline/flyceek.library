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


namespace CommonLibrary
{
    /// <summary>
    /// Service locator interface used for getting any service instance.
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// Get a named service  associated with the type.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        T GetObject<T>(string objectName);


        /// <summary>
        /// Add a named service.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="serviceName"></param>
        /// <param name="obj"></param>
        void AddObject(string objectName, object obj);
    }
}
