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
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections;

using ComLib;


namespace ComLib.Entities
{
    /// <summary>
    /// class used to register the creation of the components of 
    /// the domain model.
    /// </summary>
    /// <remarks>
    /// NOTE: Currently, the entity registration and creation works using
    /// an IocContainer.
    /// </remarks>
    public class EntityRegistrationIoC : IEntityRegistration
    {
        /// <summary>
        /// Initializes the <see cref="ActiveRecordRegistration"/> class.
        /// </summary>
        public EntityRegistrationIoC()
        {            
        }


        #region Service, Repository access methods
        /// <summary>
        /// Get the Domain service associated with the model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEntityService<T> GetService<T>()
        {
            object obj = GetService(typeof(T).FullName);
            return obj as IEntityService<T>;
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetService(Type type)
        {
            return GetService(type.FullName);
        }


        /// <summary>
        /// Get the entity Service(supporting CRUD operations).
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public object GetService(string typeFullName)
        {
            EntityRegistrationContext ctx = EntityRegistration.GetRegistrationContext(typeFullName);
            string serviceName = ctx.Name + EntityRegistrationConstants.SuffixService;
            object obj = Ioc.GetObject<object>(serviceName);
            return obj;
        }


        /// <summary>
        /// Get a new entity of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public object GetEntity(string typeFullName)
        {
            EntityRegistrationContext ctx = EntityRegistration.GetRegistrationContext(typeFullName); 
            object entity = Activator.CreateInstance(ctx.EntityType);
            return entity;
        }


        /// <summary>
        /// Get context object associated with the service of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public IActionContext GetContext(string typeFullName)
        {
            EntityRegistrationContext ctx = EntityRegistration.GetRegistrationContext(typeFullName);
            IActionContext actionContext = Activator.CreateInstance(ctx.ActionContextType) as IActionContext;
            return actionContext;
        }


        /// <summary>
        /// Get repository from IocContainer using {entityName}+ suffix.
        /// <see cref="EntityRegistrationConstants.SuffixRepository"/>
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public object GetRepository(string typeFullName)
        {
            return GetObject(typeFullName, EntityRegistrationConstants.SuffixRepository);
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetRepository(Type type)
        {
            return GetRepository(type.FullName);
        }


        /// <summary>
        /// Get the validator ffrom IocContainer using {entityName}+ suffix.
        /// <see cref="EntityRegistrationConstants.SuffixRepository"/>
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public object GetValidator(string typeFullName)
        {
            return GetObject(typeFullName, EntityRegistrationConstants.SuffixValidator);
        }


        /// <summary>
        /// Get an object from the IocContainer using the type's full name and a suffix.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public object GetObject(string typeFullName, string suffix)
        {
            EntityRegistrationContext ctx = EntityRegistration.GetRegistrationContext(typeFullName);
            string entryName = ctx.Name + suffix;
            object obj = Ioc.GetObject<object>(entryName);
            return obj;
        }
        #endregion
    }
}
