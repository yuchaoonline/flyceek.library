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
using System.Collections;
using System.Text;
using System.Data;
using System.Reflection;

using ComLib;
using ComLib.Reflection;
using ComLib.Entities;
using ComLib.Database;


namespace ComLib.Entities
{
    /// <summary>
    /// UNIT - Test  Implementation.
    /// 
    /// NOTE: This is only used for UNIT-TESTS:
    /// The real repository is RepositorySql which actually connects to a database.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class RepositoryInMemory<T> : IEntityRepository<T> where T : IEntity
    {
        #region Private data
        private static int _nextId;
        private DataTable _table;
        private string[] _columnsToIndex;
        private const string ColumnEntity = "Entity";
        #endregion


        #region Constructors and Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository&lt;int, T&gt;"/> class.
        /// </summary>
        public RepositoryInMemory(string columnsToIndexDelimited)
        {
            string[] colsToIndex = columnsToIndexDelimited.Split(',');
            Init(colsToIndex);
        }


        /// <summary>
        /// Initialize with the names of the entity properties to index.
        /// </summary>
        public void Init(string[] colsToIndex)
        {
            _columnsToIndex = colsToIndex;
            _table = new DataTable();
            IList<PropertyInfo> props = ReflectionUtils.GetProperties(typeof(T), colsToIndex);

            foreach(PropertyInfo prop in props)
            {
                _table.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
            }
            // Add the object itself.
            _table.Columns.Add(new DataColumn(ColumnEntity, typeof(object)));
        }
        #endregion


        #region IRepository<T> CRUD Methods
        /// <summary>
        /// Create an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Create(T entity)
        {
            // Create id.
            entity.Id = GetNextId();            
            DataRow row = _table.NewRow();
            TransferData(row, entity);
            _table.Rows.Add(row);
            return entity;
        }


        /// <summary>
        /// Retrieve the entity by it's key/id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            DataRow[] rows = _table.Select("Id = " + id);
            if (rows == null || rows.Length == 0)
                return default(T);

            T entity = (T)rows[0][ColumnEntity];
            return entity;
        }


        /// <summary>
        /// Retrieve all the entities.
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAll()
        {
            List<T> entities = new List<T>();
            foreach (DataRow row in _table.Rows)
            {
                entities.Add((T)row[ColumnEntity]);
            }
            return entities;
        }


        /// <summary>
        /// Retrieve all the entities into a non-generic list.
        /// </summary>
        /// <returns></returns>
        public System.Collections.IList GetAllItems()
        {
            ArrayList entities = new ArrayList();
            foreach (DataRow row in _table.Rows)
            {
                entities.Add((T)row[ColumnEntity]);
            }
            return entities;
        }


        /// <summary>
        /// UPdate the entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            DataRow row = GetRow(entity.Id);
            TransferData(row, entity);
            return entity;
        }


        /// <summary>
        /// Delete the entity.
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteByEntity(T entity)
        {
            Delete(entity.Id);
        }


        /// <summary>
        /// Delete the entitiy by it's key/id.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            DataRow row = GetRow(id);
            _table.Rows.Remove(row);
        }


        /// <summary>
        /// Create or update an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Save(T entity)
        {
            if (entity.IsPersistant())
                Update(entity);
            else
                Create(entity);
            return entity;
        }


        /// <summary>
        /// Retrieve the entity by it's key/id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow GetRow(int id)
        {
            DataRow[] rows = _table.Select("Id = " + id);
            return rows[0];
        }
        #endregion


        #region IRepository<T> Find Methods
        /// <summary>
        /// Gets or sets the db helper.
        /// </summary>
        /// <value>The helper.</value>
        public IDBHelper DbHelper { get; set; }


        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public ConnectionInfo Connection { get; set; }


        /// <summary>
        /// Gets the connection STR.
        /// </summary>
        /// <value>The connection STR.</value>
        public string ConnectionString
        {
            get { return "not implemented for InMemory storage"; }
        }


        public IList<T> Find(string queryString)
        {
            DataRow[] rows = _table.Select(queryString);
            return Map(rows);
        }


        public IList<T> Find(string queryString, object value)
        {
            throw new NotImplementedException();
        }


        public IList<T> Find(string queryString, object[] values)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Find entities by the query. 
        /// </summary>
        /// <param name="queryString">"Id = 23"</param>
        /// <param name="isFullSql">Whether or not the query contains "select from {table} "
        /// This shuold be removed from this datatable implementation.</param>
        /// <returns></returns>
        public IList<T> FindByQuery(string queryString, bool isFullSql)
        {
            DataRow[] rows = _table.Select(queryString);
            return Map(rows);
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <param name="pageNumber">1 The page number to get.</param>
        /// <param name="pageSize">15 Number of records per page.</param>
        /// <param name="totalPages">Total number of pages found.</param>
        /// <param name="totalRecords">Total number of records found.</param>
        /// <returns></returns>
        public IList<T> GetByFilter(string filter, string table, int pageNumber, int pageSize, ref int totalRecords)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get items by page using filter.
        /// </summary>
        /// <returns></returns>
        public IList<T> GetByFilter(string filter)
        {
            return FindByQuery(filter, false);
        }


        public IList<T> GetRecent(string table, int pageNumber, int pageSize, ref int totalRecords)
        {
            throw new NotImplementedException();
        }


        public IEntityRowMapper<T> RowMapper { get; set; }

        #endregion
        

        #region Helper methods
        /// <summary>
        /// Gets the next id.
        /// </summary>
        /// <returns></returns>
        private static int GetNextId()
        {
            int id = 0;
            if (_nextId.GetType() == typeof(int))
            {
                id = Convert.ToInt32(_nextId);
                id++;
                object obj = id;
                _nextId = (int)obj;
            }
            return _nextId;
        }


        private void TransferData(DataRow row, T entity)
        {
            IList<PropertyInfo> props = ReflectionUtils.GetProperties(entity.GetType(), _columnsToIndex);
            foreach (PropertyInfo prop in props)
            {
                object val = ReflectionUtils.GetPropertyValueSafely(entity, prop);
                row[prop.Name] = val;
            }
            row["Entity"] = entity;
        }


        private List<T> Map(DataRow[] rows)
        {
            List<T> entities = new List<T>();
            foreach (DataRow row in rows)
            {
                entities.Add((T)row[ColumnEntity]);
            }
            return entities;
        }
        #endregion
    }
}
