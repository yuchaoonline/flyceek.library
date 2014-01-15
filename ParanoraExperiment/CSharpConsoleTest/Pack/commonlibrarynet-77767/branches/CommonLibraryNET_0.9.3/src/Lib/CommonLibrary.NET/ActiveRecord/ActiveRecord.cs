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
using System.Collections.ObjectModel;
using System.Text;

using ComLib;


namespace ComLib.Entities
{

    /// <summary>
    /// Hybrid of Domain object with active record support.
    /// </summary>
    /// <remarks>
    /// If .NET supported multiple inheritance, this class would extend from
    /// both DomainObject, and ActiveRecord, however the IActiveRecord interface
    /// has to be implemented in this class.
    /// 1. Possible alternatives are extension methods 
    /// 2. delegation.
    /// </remarks>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class ActiveRecord<T> where T : IDomainObject<T>, new()
    {
        #region Initialization
        /// <summary>
        /// Initialize the behaviour of creating the service and repository.
        /// </summary>
        /// <param name="useSingletonService"></param>
        /// <param name="useSingletonRepository"></param>
        public static void Init(IEntityService<T> service)
        {
            Init(service, false);
        }


        /// <summary>
        /// Singleton service and repository with optional flag to indicate 
        /// whether or not to configure the repository.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configureRepository"></param>
        public static void Init(IEntityService<T> service, bool configureRepository)
        {
            EntityRegistration.Register<T>(service, configureRepository);
        }


        /// <summary>
        /// Initialize the service, repository creators.
        /// </summary>
        /// <param name="serviceCreator">The service creator.</param>
        /// <param name="repoCreator">The repository creator.</param>
        /// <param name="configureRepository">Whether or not to configure the reposiory.</param>
        public static void Init(Func<IEntityService<T>> serviceCreator, bool configureRepository)
        {
            EntityRegistration.Register<T>(serviceCreator, null, null, configureRepository);
        }


        /// <summary>
        /// Initialize the service, repository creators.
        /// </summary>
        /// <param name="serviceCreator">The service creator.</param>
        /// <param name="repoCreator">The repository creator.</param>
        /// <param name="configureRepository">Whether or not to configure the reposiory.</param>
        public static void Init(Func<IEntityService<T>> serviceCreator, Func<IEntityRepository<T>> repoCreator, bool configureRepository)
        {
            EntityRegistration.Register<T>(serviceCreator, repoCreator, null, configureRepository);
        }


        /// <summary>
        /// Initialize the service, repository creators.
        /// </summary>
        /// <param name="serviceCreator">The service creator.</param>
        /// <param name="repoCreator">The repository creator.</param>
        /// <param name="configureRepository">Whether or not to configure the reposiory.</param>
        public static void Init(Func<IEntityService<T>> serviceCreator, 
            Func<IEntityRepository<T>> repoCreator, Func<IEntityValidator<T>> validatorCreator, bool configureRepository)
        {
            EntityRegistration.Register<T>(serviceCreator, repoCreator, validatorCreator, configureRepository);
        }
        #endregion


        #region Static CRUD Methods
        /// <summary>
        /// Creates the entity
        /// </summary>
        /// <returns></returns>
        public static BoolResult<T> Create(T entity)
        {
            return DoEntityAction(entity, (context, service) => service.Create(context));
        }


        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <returns></returns>
        public static BoolResult<T> Update(T entity)
        {
            return DoEntityAction(entity, (context, service) => service.Update(context));
        }


        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <returns></returns>
        public static BoolResult<T> Save(T entity)
        {
            return DoEntityAction(entity, (context, service) => service.Save(context));
        }


        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <returns></returns>
        public static BoolResult<T> Delete(T entity)
        {
            return DoEntityAction(entity, (context, service) => service.Delete(context));
        }


        /// <summary>
        /// Delete the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolResult<T> Delete(int id)
        {
            IEntityService<T> service = EntityRegistration.GetService<T>();
            IActionContext context = EntityRegistration.GetContext<T>();
            context.Id = id;
            return service.Delete(context);
        }


        /// <summary>
        /// Retrieve the model associated with the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BoolResult<T> Get(int id)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            IActionContext context = ActiveRecordRegistration.GetContext<T>();
            context.Id = id;
            BoolResult<T> result = service.Get(context);
            ActiveRecordRegistration.Init<T>(result.Item, service);
            return result;
        }


        /// <summary>
        /// Retrieve all instances of model.
        /// </summary>
        /// <returns></returns>
        public static BoolResult<IList<T>> GetAll()
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            IActionContext ctx = ActiveRecordRegistration.GetContext<T>();
            BoolResult<IList<T>> result = service.GetAll(ctx);
            foreach (T item in result.Item)
            {
                ActiveRecordRegistration.Init<T>(item, service);
            }
            return result;
        }
        

        /// <summary>
        /// Get items by page.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public static BoolResult<PagedList<T>> Get(int pageNumber, int pageSize)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            BoolResult<PagedList<T>> result = service.Get(pageNumber, pageSize);
            return result;
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public static BoolResult<PagedList<T>> GetByFilter(string filter, int pageNumber, int pageSize)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            BoolResult<PagedList<T>> result = service.GetByFilter(filter, pageNumber, pageSize);
            return result;
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="filter">e.g. "UserNameLowered = 'kishore'"</param>
        /// <returns></returns>
        public static BoolResult<IList<T>> GetByFilter(string filter)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            BoolResult<IList<T>> result = service.GetByFilter(filter);
            return result;
        }


        /// <summary>
        /// Get items by page.
        /// </summary>
        /// <param name="table">"BlogPosts"</param>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public static BoolResult<PagedList<T>> GetRecent(int pageNumber, int pageSize)
        {
            IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
            BoolResult<PagedList<T>> result = service.GetRecent(pageNumber, pageSize);
            return result;
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// Comma delimited string of roles that can moderate instances of this entity.
        /// </summary>
        public static string Moderators
        {
            get
            {
                IEntityService<T> service = ActiveRecordRegistration.GetService<T>();
                return service.Settings.EditRoles;
            }
        }
        #endregion


        #region Protected methods
        /// <summary>
        /// Performs the actual entity action specified by the delegate <paramref name="executor"/>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        protected static BoolResult<T> DoEntityAction(T entity, Func<IActionContext, IEntityService<T>, BoolResult<T>> executor)
        {
            IActionContext ctx = ActiveRecordRegistration.GetContext<T>();
            ctx.CombineMessageErrors = true;
            ctx.Item = entity;
            IEntityService<T> service = EntityRegistration.GetService<T>();
            return executor(ctx, service);
        }
        #endregion
    }
}
