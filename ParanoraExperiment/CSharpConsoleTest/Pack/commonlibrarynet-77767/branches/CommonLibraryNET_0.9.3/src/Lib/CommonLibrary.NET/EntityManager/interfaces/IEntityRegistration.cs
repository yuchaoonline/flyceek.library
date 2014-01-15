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
using System.Collections.ObjectModel;

using ComLib;
using ComLib.Entities;


namespace CommonLibrary
{
    public interface IEntityRegistration
    {        
        /// <summary>
        /// Gets the context associated with an Entity service.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        IActionContext GetContext(string typeFullName);
        
        
        /// <summary>
        /// Gets the repository associated with the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetRepository(Type type);
        
        
        /// <summary>
        /// Gets the repository associated with the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        object GetRepository(string typeFullName);
        
        
        /// <summary>
        /// Gets the service for the entity given the types full name.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        object GetService(string typeFullName);
        
        
        /// <summary>
        /// Gets the IService associated with the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEntityService<T> GetService<T>();
        
        
        /// <summary>
        /// Gets the DomainModel's service given the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetService(Type type);


        /// <summary>
        /// Get an new entity of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        object GetEntity(string typeFullName);


        /// <summary>
        /// Get an new entity of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        object GetValidator(string typeFullName);
    }
}
