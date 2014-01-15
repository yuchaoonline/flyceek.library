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
    public class EntityRegistration 
    {
        private static IEntityRegistration _entityRegistrarIoc;
        private static IDictionary<string, EntityRegistrationContext> _managableEntities;
        private static IList<string> _managableEntitiesList;


        /// <summary>
        /// Default provider to Ioc
        /// </summary>
        static EntityRegistration()
        {
            _entityRegistrarIoc = new EntityRegistrationIoC();
            _managableEntities = new Dictionary<string, EntityRegistrationContext>();
            _managableEntitiesList = new List<string>();
        }


        #region Registration related members
        /// <summary>
        /// Registers the specified CTX for creating the components
        /// necessary for the DomainModel ActiveRecord.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public static void Register(EntityRegistrationContext ctx)
        {
            if (!_managableEntities.ContainsKey(ctx.EntityType.FullName))
            {
                _managableEntities.Add(ctx.EntityType.FullName, ctx);
                _managableEntitiesList.Add(ctx.EntityType.FullName);
            }
            // Overwrite
            else
            {
                _managableEntities[ctx.EntityType.FullName] = ctx;
            }
            // Register.
            if (ctx.IsRepositoryConfigurationRequired && ctx.IsSingletonRepository)
                RepositoryConfigurator.Configure((IRepositoryConfigurable)ctx.Repository);
        }


        /// <summary>
        /// Register a singleton service/repository for the entity specified by T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="configureRepository"></param>
        public static void Register<T>(IEntityService<T> service, bool configureRepository)
        {
            EntityRegistrationContext ctx = new EntityRegistrationContext();
            ctx.EntityType = typeof(T);
            ctx.Name = typeof(T).FullName;
            ctx.IsSingletonService = true;
            ctx.IsSingletonRepository = true;
            ctx.Service = service;
            ctx.Repository = service.Repository;
            ctx.IsRepositoryConfigurationRequired = configureRepository;
            ctx.CreationMethod = EntityCreationType.Factory;
            ctx.ActionContextType = typeof(ActionContext<T>);

            if (ctx.IsRepositoryConfigurationRequired)
                RepositoryConfigurator.Configure(service.Repository);

            Register(ctx);
        }


        /// <summary>
        /// Initialize the service, repository creators.
        /// </summary>
        /// <param name="serviceCreator">The service creator.</param>
        /// <param name="repoCreator">The repository creator.</param>
        /// <param name="configureRepository">Whether or not to configure the reposiory.</param>
        public static void Register<T>(Func<IEntityService<T>> serviceCreator, Func<IEntityRepository<T>> repoCreator, Func<IEntityValidator<T>> validatorCreator, bool configureRepository)
        {
            EntityRegistrationContext ctx = new EntityRegistrationContext();
            ctx.EntityType = typeof(T);
            ctx.Name = typeof(T).FullName;
            ctx.IsSingletonService = false;
            ctx.IsSingletonRepository = false;
            ctx.IsRepositoryConfigurationRequired = configureRepository;
            ctx.FactoryMethodForService = new Func<object>(() => serviceCreator());
            ctx.CreationMethod = EntityCreationType.Factory;
            ctx.ActionContextType = typeof(ActionContext<T>);
                        
            if(repoCreator != null) ctx.FactoryMethodForRepository = new Func<object>(() => repoCreator());
            if(validatorCreator != null) ctx.FactoryMethodForValidator = new Func<object>(() => validatorCreator());

            EntityRegistration.Register(ctx);
        }


        /// <summary>
        /// Registers the specified entity type to wire up ActiveRecord functionality.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public static void Register(Type entityType, Func<object> serviceCreator, Func<object> repositoryCreator)
        {
            EntityRegistrationContext ctx = null;
            if (_managableEntities.ContainsKey(entityType.FullName))
            {
                ctx = _managableEntities[entityType.FullName];                
            }
            else
            {
                ctx = new EntityRegistrationContext(entityType.Name, entityType, typeof(ActionContext));
            }

            // Setup the properties.
            ctx.CreationMethod = EntityCreationType.Factory;
            ctx.FactoryMethodForService = serviceCreator;
            ctx.FactoryMethodForRepository = repositoryCreator;
            Register(ctx);
        }


        /// <summary>
        /// Registers the specified entity type to wire up ActiveRecord functionality.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public static void Register(Type entityType, Func<object> repositoryCreator)
        {
            Register(entityType, null, repositoryCreator);
        }


        /// <summary>
        /// Determine if the entity specified by the type is registered
        /// for being managable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ContainsEntity(Type type)
        {
            return _managableEntities.ContainsKey(type.FullName);
        }


        /// <summary>
        /// Determine if the entity specified by the type is registered
        /// for being managable.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ContainsEntity(string typeFullName)
        {
            return _managableEntities.ContainsKey(typeFullName);
        }


        /// <summary>
        /// Returns a list of strings representing the names of the 
        /// managable entities.
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<string> GetManagableEntities()
        {
            return new ReadOnlyCollection<string>(_managableEntitiesList);
        }


        /// <summary>
        /// Get the registration context for the entity full name.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static EntityRegistrationContext GetRegistrationContext(string typeFullName)
        {
            return _managableEntities[typeFullName];
        }
        #endregion


        #region Service, Repository, Validator access methods
        /// <summary>
        /// Get the Domain service associated with the model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEntityService<T> GetService<T>()
        {
            return (IEntityService<T>)GetService(typeof(T).FullName);
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return GetService(type.FullName);
        }


        /// <summary>
        /// Get the entity Service(supporting CRUD operations).
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static object GetService(string typeFullName)
        {
            EntityRegistrationContext ctx = _managableEntities[typeFullName];

            // Singleton.
            if (ctx.IsSingletonService)
                return ctx.Service;

            object service = null;
            object repository = null;

            if (ctx.CreationMethod == EntityCreationType.Factory)
            {
                service = ctx.FactoryMethodForService();
                if (ctx.FactoryMethodForRepository != null)
                {
                    repository = ctx.FactoryMethodForRepository();
                }
                if (repository != null && ctx.IsRepositoryConfigurationRequired)
                    RepositoryConfigurator.Configure((IRepositoryConfigurable)repository);
            }
            else
                service = _entityRegistrarIoc.GetService(typeFullName);

            return service;
        }


        /// <summary>
        /// Get context object associated with the service of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static IActionContext GetContext(string typeFullName)
        {
            return _entityRegistrarIoc.GetContext(typeFullName);
        }


        /// <summary>
        /// Get context object associated with the service of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static IActionContext GetContext<T>()
        {
            return _entityRegistrarIoc.GetContext(typeof(T).FullName);
        }


        /// <summary>
        /// Get context with id set on the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IActionContext GetContext<T>(int id)
        {
            IActionContext ctx = GetContext(typeof(T).FullName);
            ctx.CombineMessageErrors = true;
            ctx.Id = id;
            return ctx;
        }


        /// <summary>
        /// Get context with the entity set on the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IActionContext GetContext<T>(IEntity entity)
        {
            IActionContext ctx = GetContext(typeof(T).FullName);
            ctx.CombineMessageErrors = true;
            ctx.Item = entity;
            return ctx;
        }


        /// <summary>
        /// Get a new entity of the specified type.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        public static object GetEntity(string typeFullName)
        {
            return _entityRegistrarIoc.GetEntity(typeFullName);
        }


        /// <summary>
        /// Get repository with the specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEntityRepository<T> GetRepository<T>()
        {
            object repository = GetRepository(typeof(T));
            return (IEntityRepository<T>)repository;
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRepository(Type type)
        {
            return GetRepository(type.FullName);
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRepository(string typeFullName)
        {
            EntityRegistrationContext ctx = _managableEntities[typeFullName];

            // If factory method was supplied.
            if (ctx.FactoryMethodForRepository != null)
                return ctx.FactoryMethodForRepository();

            return _entityRegistrarIoc.GetRepository(typeFullName);
        }


        /// <summary>
        /// Get the Domain entity Service object that supports(CRUD) operations.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetValdiator(Type type)
        {
            EntityRegistrationContext ctx = _managableEntities[type.FullName];
            if (ctx.FactoryMethodForValidator != null)
                return ctx.FactoryMethodForValidator();

            return _entityRegistrarIoc.GetValidator(type.FullName);
        }
        #endregion

        
        #region Factory helpers
        private static T Create<T>(ref T objectRef, object syncRoot, Func<T> creator)
        {
            if (objectRef == null)
            {
                lock (syncRoot)
                {
                    if (objectRef == null)
                    {
                        objectRef = creator();
                    }
                }
            }
            return objectRef;
        }
        #endregion
    }
}
