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
using System.Data;
using ComLib;
using ComLib.Database;
using ComLib.Locale;


namespace ComLib.Entities
{

    /// <summary>
    /// Reuse the existing interface for the IValidator, but modify it 
    /// slightly to incorporate the entityaction being performed.
    /// This allows for a singleton validator using the it's non-stateful
    /// method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityValidator<T> : IValidator
    {
        /// <summary>
        /// Validates <paramref name="target"/> for the <paramref name="action"/> specified
        /// and adds <see cref="ValidationResult"/> entires representing
        /// failures to the supplied <paramref name="validationResults"/>.
        /// </summary>
        /// <param name="target">The object to validate.</param>
        /// <param name="validationResults">The <see cref="ValidationResults"/> where the validation failures
        /// should be collected.</param>
        bool Validate(object target, IValidationResults results, EntityAction action);
    }



    /// <summary>
    /// Interface for an entity data massager.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityMassager
    {
        /// <summary>
        /// Massage
        /// </summary>
        /// <param name="entity"></param>
        void Massage(object entity, EntityAction action);
    }



    /// <summary>
    /// The model service handles all actions on the model.
    /// This includes CRUD operations which are delegated to the 
    /// ModelRespository after a ModelService performs any appropriate
    /// security checks among other things.
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IEntityService<T> 
    {
        /// <summary> 
        /// The resposity for the model. 
        /// </summary> 
        IEntityRepository<T> Repository { get; set; }

                
        /// <summary> 
        /// The validator for the model. 
        /// </summary> 
        IEntityValidator<T> Validator { get; set; }


        /// <summary>
        /// Configuration settings for the service.
        /// </summary>
        IEntitySettings<T> Settings { get; set; }


        /// <summary>
        /// Localized resource manager.
        /// </summary>
        IEntityResources Resources { get; set; }


        /// <summary>
        /// Get the settings as a typed object.
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <returns></returns>
        TSettings GetSettings<TSettings>() where TSettings : class;


        /// <summary>
        /// Get the validator.
        /// </summary>
        /// <returns></returns>
        IEntityValidator<T> GetValidator();


        /// <summary>
        /// This method is useful for derived classes to implement 
        /// their own initialization behaviour.
        /// </summary>
        void Init();


        /// <summary> 
        /// Creates the entity. 
        /// </summary> 
        /// <param name="ctx"></param> 
        /// <returns></returns> 
        BoolResult<T> Create(IActionContext ctx);


        /// <summary> 
        /// Retrieves the model from the repository.
        /// You can specify either the id or object.
        /// </summary> 
        /// <param name="id"></param> 
        /// <returns></returns> 
        BoolResult<T> Get(IActionContext ctx);


        /// <summary>
        /// Retrieves all the instances of the model from the repository
        /// </summary>
        /// <returns></returns>
        BoolResult<IList<T>> GetAll(IActionContext ctx);


        /// <summary>
        /// Retrieve all items as a non-generic list.
        /// This is to allow retrieving all items across different types using reflection.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        BoolResult<IList> GetAllItems(IActionContext ctx);


        /// <summary>
        /// Get items by page.
        /// </summary>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">15</param>
        /// <param name="totalPages">4</param>
        /// <param name="totalRecords">60</param>
        /// <returns></returns>
        BoolResult<PagedList<T>> Get(int pageNumber, int pageSize);


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">15</param>
        /// <param name="totalPages">4</param>
        /// <param name="totalRecords">60</param>
        /// <returns></returns>
        BoolResult<PagedList<T>> GetByFilter(string filter, int pageNumber, int pageSize);


        /// <summary>
        /// Get all items at once(without paging records) using filter.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        BoolResult<IList<T>> GetByFilter(string filter);


        /// <summary>
        /// Get items by page and by the user who created them.
        /// </summary>
        /// <param name="userName">Name of user who created the entities.</param>
        /// <param name="pageNumber">Page number to get. e.g. 1</param>
        /// <param name="pageSize">Number of records per page. e.g. 15</param>
        /// <param name="totalPages">Total number of pages with matching records.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        BoolResult<PagedList<T>> GetByUser(string userName, int pageNumber, int pageSize);


        /// <summary>
        /// Get recent items by page.
        /// </summary>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">15</param>
        /// <param name="totalPages">4</param>
        /// <param name="totalRecords">60</param>
        /// <returns></returns>
        BoolResult<PagedList<T>> GetRecent(int pageNumber, int pageSize);


        /// <summary>
        /// Updates the model in the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        BoolResult<T> Update(IActionContext ctx);


        /// <summary>
        /// Updates the model in the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        BoolResult<T> Save(IActionContext ctx);


        /// <summary>
        /// Deletes the model from the repository.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        BoolResult<T> Delete(IActionContext ctx);
    }



    /// <summary>
    /// Interface for the models service settings that ares used to 
    /// configuration settings for the model service.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntitySettings<T>
    {
        /// <summary>
        /// Whether authentication is required to edit the entity.
        /// </summary>
        bool EnableAuthentication { get; set; }


        /// <summary>
        /// Whether or not to validate the entity.
        /// </summary>
        bool EnableValidation { get; set; }


        /// <summary>
        /// Roles allowed to edit the entity.
        /// </summary>
        string EditRoles { get; set; }
    }



    /// <summary>
    /// Localization resource provider for entity.
    /// </summary>
    public interface IEntityResources : ILocalizationResourceProvider
    {
        /// <summary>
        /// Get the name of the entity.
        /// </summary>
        string EntityName { get; }
    }    



    /// <summary>
    /// Interface for a database based repository(storage) of a model.
    /// This also supports non-ORM related behavior and is therefore
    /// supplied the connectionInfo and database helper.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IEntityRepository<T> : IRepository<T>, IRepositoryConfigurable
    {
        /// <summary>
        /// Row Mapper
        /// </summary>
        IEntityRowMapper<T> RowMapper { get; set; }
    }



    /// <summary>
    /// Row mapper for a model to map database rows to model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityRowMapper<T> : IRowMapper<IDataReader, T>
    {
    }



    /// <summary>
    /// Interface for the view for a model.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IEntityView<T>
    {
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>The controller.</value>
        IEntityController<T> Controller { get; set; }


        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        IValidationResults Errors { get; }


        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>The messages.</value>
        IStatusResults Messages { get; }
    }



    /// <summary>
    /// Interface for a controller than mediates actions done on the Model
    /// Mediates the view with the service.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="T">The model</typeparam>
    public interface IEntityController<T>
    {
        /// <summary> 
        /// The view. 
        /// </summary> 
        IEntityView<T> View { get; set; }


        /// <summary>
        /// The service.
        /// </summary>
        /// <value>The service.</value>
        IEntityService<T> Service { get; set; }
    }
}
