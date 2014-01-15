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
using System.Text;

using ComLib;


namespace ComLib.Entities
{

    /// <summary> 
    /// Active record interface for any model. 
    /// </summary> 
    /// <typeparam name="TId"></typeparam> 
    /// <typeparam name="T"></typeparam> 
    public interface IActiveRecordBase<T> where T : IEntityPersistant<int>
    {
        /// <summary> 
        /// The underlying service that handles model actions. 
        /// </summary> 
        IEntityService<T> Service { get; set; }


        /// <summary> 
        /// Creates the model 
        /// </summary> 
        /// <returns></returns> 
        BoolMessage Create();


        /// <summary> 
        /// Updates the model. 
        /// </summary> 
        /// <returns></returns> 
        BoolMessage Update();


        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns></returns>
        BoolMessage Save();


        /// <summary> 
        /// Deletes the model. 
        /// </summary> 
        /// <returns></returns> 
        BoolMessage Delete();
    }
}
