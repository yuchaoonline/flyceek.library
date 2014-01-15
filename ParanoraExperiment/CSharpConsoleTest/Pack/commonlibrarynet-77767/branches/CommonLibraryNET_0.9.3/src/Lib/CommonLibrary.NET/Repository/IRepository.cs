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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using ComLib;
using ComLib.Database;


namespace ComLib.Entities
{
    /// <summary>
    /// Interface for Dao with default primary key as int.
    /// <see cref="IDaoWithTypedId" />.
    /// </summary>
    public interface IRepository<T> : IRepositoryWithId<int, T>
    { } 



    /// <summary>
    /// 
    /// </summary>
    public interface IRepositoryConfigurable
    {
        /// <summary>
        /// Gets or sets the db helper.
        /// </summary>
        /// <value>The helper.</value>
        IDBHelper DbHelper { get; set; }


        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        ConnectionInfo Connection { get; set; }


        /// <summary>
        /// Gets the connection STR.
        /// </summary>
        /// <value>The connection STR.</value>
        string ConnectionString { get; }
    }    



    /// <summary>
    /// Interface for a DAO(Data Access Object) to support CRUD operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="IdT"></typeparam>
    public interface IRepositoryWithId<TId, T> 
    {

        #region Retrieve methods.
        /// <summary>
        /// Retrieve the entity by it's key/id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(TId id);


        /// <summary>
        /// Retrieve all the entities.
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();


        /// <summary>
        /// Retrieve all the entities into a non-generic list.
        /// </summary>
        /// <returns></returns>
        IList GetAllItems();


        /// <summary>
        /// Get items by page
        /// </summary>
        /// <param name="table">"Blogposts"</param>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">15 ( records per page )</param>
        /// <param name="totalPages"> Total number of pages found</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        IList<T> GetByFilter(string filter, string table, int pageNumber, int pageSize, ref int totalRecords);


        /// <summary>
        /// Get items by page
        /// </summary>
        /// <param name="table">"Blogposts"</param>
        /// <param name="pageNumber">1</param>
        /// <param name="pageSize">15 ( records per page )</param>
        /// <param name="totalPages"> Total number of pages found</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        IList<T> GetRecent(string table, int pageNumber, int pageSize, ref int totalRecords);        
        #endregion


        #region Find methods
        /// <summary>
        /// Find by query
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IList<T> Find(string queryString);


        /// <summary>
        /// Find by query string and single parameter value.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IList<T> Find(string queryString, object value);
        
        
        /// <summary>
        /// Find by query and multiple parameter values.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IList<T> Find(string queryString, object[] values);


        /// <summary>
        /// Finds the entities by the query.
        /// </summary>
        /// <param name="queryString">Query text.</param>
        /// <param name="isFullSql">Indicate if text supplied is a full sql or just 
        /// clause after the where.</param>
        /// <returns></returns>
        IList<T> FindByQuery(string queryString, bool isFullSql);
        #endregion


        #region Save/Delete methods
        /// <summary>
        /// Create an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Create(T entity);


        /// <summary>
        /// UPdate the entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);


        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity"></param>
        void DeleteByEntity(T entity);


        /// <summary>
        /// Delete the entitiy by it's key/id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(TId id);

        
        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);
        #endregion
    }
}
