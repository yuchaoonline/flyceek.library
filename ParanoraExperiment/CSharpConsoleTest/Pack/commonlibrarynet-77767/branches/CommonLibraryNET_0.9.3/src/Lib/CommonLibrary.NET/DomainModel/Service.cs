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
using System.Security.Principal;
using System.Security.Authentication;
using System.Linq;
using ComLib;
using ComLib.Paging;
using ComLib.Errors;
using ComLib.Authentication;
using ComLib.ValidationSupport;


namespace ComLib.Entities
{
    
    /// <summary>
    /// Service class used for handling entity actions.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public class EntityService<T> : IEntityService<T> where T : IEntity
    {
        private IEntityRepository<T> _repository;
        private IEntitySettings<T> _settings;
        private IEntityValidator<T> _validator;
        private IEntityResources _resources;


        /// <summary>
        /// Initializes a new instance of the <see cref="Service&lt;TId, T&gt;"/> class.
        /// </summary>
        public EntityService() 
        {
            Init();
        }


        /// <summary>
        /// Initialize with Repository and validator.
        /// </summary>
        /// <param name="repository">PesistantStorage for the entity.</param>
        /// <param name="settings">Settings for the entity.</param>
        /// <param name="validator">Validator for the entity</param>
        public EntityService(IEntityRepository<T> repository, IEntityValidator<T> validator, IEntitySettings<T> settings)
        {
            _repository = repository;
            if (validator != null) 
                _validator = validator;

            if (settings != null)  
                _settings  = settings;

            Init();
        }

        
        #region IEntityService<T> Members
        /// <summary>
        /// This method is useful for derived classes to implement 
        /// their own initialization behaviour.
        /// </summary>
        public virtual void Init() { }


        /// <summary>
        /// The resposity for the model.
        /// </summary>
        /// <value></value>
        public virtual IEntityRepository<T> Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }


        /// <summary>
        /// Resources used for the service.
        /// </summary>
        public virtual IEntityResources Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }


        /// <summary>
        /// The validator for the model.
        /// </summary>
        /// <value></value>
        public virtual IEntityValidator<T> Validator
        {
            get { return GetValidator(); }
            set { _validator = value; }
        }


        /// <summary>
        /// Configuration settings for the service.
        /// </summary>
        /// <value></value>
        public virtual IEntitySettings<T> Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }


        /// <summary>
        /// Get the settings as a typed object.
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <returns></returns>
        public virtual TSettings GetSettings<TSettings>() where TSettings : class
        {
            return _settings as TSettings;
        }

        
        /// <summary>
        /// Get the validator.
        /// </summary>
        /// <returns></returns>
        public virtual IEntityValidator<T> GetValidator()
        {
            return _validator;
        }


        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual BoolResult<T> Create(IActionContext ctx)
        {
            return PerformAction(ctx, delegate(IActionContext context) { _repository.Create((T)context.Item); }, EntityAction.Create);
        }


        /// <summary>
        /// Updates the model in the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual BoolResult<T> Update(IActionContext ctx)
        {
            return PerformAction(ctx, delegate(IActionContext context) { _repository.Update((T)context.Item); }, EntityAction.Update);
        }


        /// <summary>
        /// Updates the model in the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual BoolResult<T> Save(IActionContext ctx)
        {
            T item = (T)ctx.Item;
            EntityAction entityAction = item.IsPersistant() ? EntityAction.Update : EntityAction.Create;

            return PerformAction(ctx, delegate(IActionContext context)
            {
                if (entityAction == EntityAction.Update)
                    _repository.Update(item);
                else
                    _repository.Create(item);
            }, 
            entityAction);
        }


        /// <summary>
        /// Deletes the model from the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual BoolResult<T> Delete(IActionContext ctx)
        {
            return PerformAction(ctx, delegate(IActionContext context)
            {
                if (context.Item == null)
                    _repository.Delete(context.Id);
                else
                    _repository.DeleteByEntity((T)context.Item);
            }, 
            EntityAction.Delete);
        }


        /// <summary>
        /// Retrieves the model from the repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual BoolResult<T> Get(IActionContext ctx)
        {
            try
            {
                T item = _repository.Get(ctx.Id);
                return new BoolResult<T>(item, true, string.Empty, ctx.Errors, ctx.Messages);
            }
            catch (Exception ex)
            {
                string error = "Unable to retrieve " + typeof(T).Name + " with : " + ctx.Id;
                ErrorManager.Handle(error, ex, ctx.Errors);
                return new BoolResult<T>(default(T), false, error, ctx.Errors, ctx.Messages);
            }
        }


        /// <summary>
        /// Retrieves all the instances of the model from the repository
        /// </summary>
        /// <returns></returns>
        public virtual BoolResult<IList<T>> GetAll(IActionContext ctx)
        {
            try
            {
                IList<T> items = _repository.GetAll();
                return new BoolResult<IList<T>>(items, true, string.Empty, ctx.Errors, ctx.Messages);
            }
            catch (Exception ex)
            {
                string error = "Unable to retrieve all items of type " + typeof(T).Name;
                ErrorManager.Handle(error, ex, ctx.Errors);
                return new BoolResult<IList<T>>(new List<T>(), false, error, ctx.Errors, ctx.Messages);
            }
        }


        /// <summary>
        /// Retrieves all the instances of the model from the repository
        /// </summary>
        /// <returns></returns>
        public virtual BoolResult<IList> GetAllItems(IActionContext ctx)
        {
            try
            {
                IList items = _repository.GetAllItems();
                return new BoolResult<IList>(items, true, string.Empty, ValidationResults.Empty, StatusResults.Empty);
            }
            catch (Exception ex)
            {
                string error = "Unable to retrieve all items of type " + typeof(T).Name;
                ErrorManager.Handle(error, ex, ctx.Errors);
                return new BoolResult<IList>(new ArrayList(), false, error, ValidationResults.Empty, StatusResults.Empty);
            }
        }


        /// <summary>
        /// Get items by page.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public BoolResult<PagedList<T>> Get(int pageNumber, int pageSize)
        {
            int totalRecords = 0;
            IList<T> items = _repository.GetByFilter(string.Empty, typeof(T).Name + "s", pageNumber, pageSize, ref totalRecords);
            PagedList<T> pagedList = new PagedList<T>(pageNumber, pageSize, totalRecords, items);
            return new BoolResult<PagedList<T>>(pagedList, true, string.Empty, ValidationResults.Empty, StatusResults.Empty);
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="pageNumber">Page number to get. e.g. 1</param>
        /// <param name="pageSize">Number of records per page. e.g. 15</param>
        /// <param name="totalPages">Total number of pages with matching records.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public BoolResult<PagedList<T>> GetByFilter(string filter, int pageNumber, int pageSize)
        {
            string tableName = typeof(T).Name + "s";
            int totalRecords = 0;
            IList<T> items = _repository.GetByFilter(filter, tableName, pageNumber, pageSize, ref totalRecords);
            PagedList<T> pagedList = new PagedList<T>(pageNumber, pageSize, totalRecords, items);
            return new BoolResult<PagedList<T>>(pagedList, true, string.Empty, ValidationResults.Empty, StatusResults.Empty);
        }


        /// <summary>
        /// Get items by page and by the user who created them.
        /// </summary>
        /// <param name="userName">Name of user who created the entities.</param>
        /// <param name="pageNumber">Page number to get. e.g. 1</param>
        /// <param name="pageSize">Number of records per page. e.g. 15</param>
        /// <param name="totalPages">Total number of pages with matching records.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public BoolResult<PagedList<T>> GetByUser(string userName, int pageNumber, int pageSize)
        {
            string filter = "CreateUser = '" + userName + "'";
            return GetByFilter(filter, pageNumber, pageSize);
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public BoolResult<IList<T>> GetByFilter(string filter)
        {
            IList<T> items = _repository.FindByQuery(filter, false);
            return new BoolResult<IList<T>>(items, true, string.Empty, ValidationResults.Empty, StatusResults.Empty);
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
        public BoolResult<PagedList<T>> GetRecent(int pageNumber, int pageSize)
        {
            int totalRecords = 0;
            IList<T> items = _repository.GetRecent(typeof(T).Name + "s", pageNumber, pageSize, ref totalRecords);
            PagedList<T> pagedList = new PagedList<T>(pageNumber, pageSize, totalRecords, items);
            return new BoolResult<PagedList<T>>(pagedList, true, string.Empty, ValidationResults.Empty, StatusResults.Empty);
        }


        /// <summary>
        /// Find by query
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual BoolResult<IList<T>> Find(string queryString)
        {
            return null;
        }


        /// <summary>
        /// Find by query string and single parameter value.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual BoolMessageItem<IList<T>> Find(string queryString, object value)
        {
            return null;
        }


        /// <summary>
        /// Find by query and multiple parameter values.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual BoolMessageItem<IList<T>> Find(string queryString, object[] values)
        {
            return null;
        }
        #endregion


        /// <summary>
        /// Performs the action.
        /// </summary>
        /// <param name="ctx">The actioncontext.</param>
        /// <param name="action">Delegate to call to perform the action on the model.</param>
        /// <param name="actionName">Name of the action.</param>
        protected virtual BoolResult<T> PerformAction(IActionContext ctx, Action<IActionContext> action, EntityAction entityAction)
        {
            string entityName = ctx.Item == null ? " with id : " + ctx.Id : ctx.Item.GetType().FullName;                
            try
            {
                // Step 1: Authenticate and check security.                
                BoolResult<T> result = PerformAuthentication(ctx);
                if (!result.Success) return new BoolResult<T>(default(T), false, result.Message, ctx.Errors, ctx.Messages);


                // Step 2: Massage Data before validation.
                ApplyMassageOnAuditData(ctx, entityAction);
                PerformBeforeValidation(ctx, entityAction);

                // Step 3: Validate the entity.
                result = PerformValidation(ctx, entityAction);
                if (!result.Success) return result;

                // Step 4: Massage data after validation.
                PerformAfterValidation(ctx, entityAction);

                // Step 5: Now persist the entity.
                action(ctx);

                // Build the message.
                string message = "Successfully " + entityAction.ToString() + entityName;
                return new BoolResult<T>(default(T), true, message, ctx.Errors, ctx.Messages);
            }
            catch (Exception ex)
            {
                string error = "Unable to : " + entityAction.ToString() + " entity : " + entityName;
                ErrorManager.Handle(error, ex, ctx.Errors);
                return new BoolResult<T>(default(T), false, error, ctx.Errors, ctx.Messages);
            }
        }


        /// <summary>
        /// Performs the validation on the model.
        /// </summary>
        /// <param name="ctx">The action context.</param>
        /// <returns></returns>
        protected virtual BoolResult<T> PerformValidation(IActionContext ctx, EntityAction entityAction)
        {
            if (!_settings.EnableValidation)
                return BoolResult<T>.True;

            IEntityValidator<T> validator = GetValidator();
            bool result = validator.Validate(ctx.Item, ctx.Errors);
            if (!result)
            {
                if (ctx.CombineMessageErrors)
                {
                    string errorMessage = ValidationUtils.BuildSingleErrorMessage(ctx.Errors, Environment.NewLine);
                    return new BoolResult<T>(default(T), false, errorMessage, ValidationResults.Empty, StatusResults.Empty);
                }
            }
            return BoolResult<T>.True;
        }


        /// <summary>
        /// Performs authentication to determine if the current use is allowed to edit
        /// this entity.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        protected virtual BoolResult<T> PerformAuthentication(IActionContext ctx)
        {
            if (!_settings.EnableAuthentication)
                return BoolResult<T>.True;

            if (string.IsNullOrEmpty(_settings.EditRoles))
                return BoolResult<T>.True;

            bool isAuthorized = Auth.IsUserInRoles(_settings.EditRoles);
            string message = isAuthorized ? string.Empty : "User : " + Auth.UserName 
                + " is not authorized to edit this entity";

            BoolResult<T> result = new BoolResult<T>(default(T), isAuthorized, message, ctx.Errors, ctx.Messages);
            if (!result.Success)
                ctx.Errors.Add(result.Message);

            return result;
        }


        /// <summary>
        /// Massage the data before validation.
        /// e.g. Convert cityname, county name to CountryId for persistance.
        /// </summary>
        /// <param name="ctx"></param>
        protected virtual void ApplyMassageOnAuditData(IActionContext ctx, EntityAction entityAction)
        {
            // Nothing here for now.
            if(ctx.Item == null) return;

            IDomainObject<T> entity = ctx.Item as IDomainObject<T>;
            
            // Set create date.
            if (entityAction == EntityAction.Create || (string.IsNullOrEmpty(entity.CreateUser) && entity.CreateDate == DateTime.MinValue))
            {
                entity.CreateDate = DateTime.Now;
                entity.CreateUser = Auth.UserName;
            }

            // Now set update fields.
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUser = Auth.UserName;
            string timeStamp = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();
            entity.UpdateComment = "updated " + timeStamp + " by " + Auth.UserName + " : " + entity.UpdateComment;
        }        


        /// <summary>
        /// Data massage execution
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="entityAction"></param>
        protected void MassageData(IActionContext ctx, EntityAction entityAction, List<IEntityMassager> massagers)
        {
            // Nothing here for now.
            if (ctx.Item == null) return;

            IDomainObject<T> entity = ctx.Item as IDomainObject<T>;
            
            // Run all the massagers.
            if (massagers != null && massagers.Count > 0)
            {
                foreach (IEntityMassager massager in massagers)
                    massager.Massage(entity, entityAction);
            }
        }


        /// <summary>
        /// Get list of data massagers for the entity.
        /// </summary>
        /// <returns></returns>
        protected virtual void PerformBeforeValidation(IActionContext ctx, EntityAction entityAction)
        {
        }


        /// <summary>
        /// Get list of data massagers for the entity.
        /// </summary>
        /// <returns></returns>
        protected virtual void PerformAfterValidation(IActionContext ctx, EntityAction entityAction)
        {
        }
    }



    public class QueryContext : ActionContext
    {
        public string QueryString;
        public bool IsSql = false;
        public object ParamValue;
        public object[] ParamValues;
    }
}
