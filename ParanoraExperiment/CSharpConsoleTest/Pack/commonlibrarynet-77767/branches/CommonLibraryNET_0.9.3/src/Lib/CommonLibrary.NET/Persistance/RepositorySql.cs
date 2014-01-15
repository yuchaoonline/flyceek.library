using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;

using CommonLibrary.DomainModel;


namespace CommonLibrary
{
    /// <summary>
    /// Repository for a relational database, base class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositorySql<T> : RepositoryBase<T>, IEntityRepository<T> where T : IEntity
    {
        private string _tableName;


        /// <summary>
        /// Initialize
        /// </summary>
        public RepositorySql()
        {
            Init(null, null);
        }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="connectionInfo"></param>
        /// <param name="helper"></param>
        public RepositorySql(ConnectionInfo connectionInfo, IDBHelper helper)
        {
            Init(connectionInfo, helper);
        }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="connectionInfo"></param>
        /// <param name="helper"></param>
        public void Init(ConnectionInfo connectionInfo, IDBHelper helper)
        {
            _connectionInfo = connectionInfo;
            _db = helper;
            _tableName = typeof(T).Name + "s";
        }


        /// <summary>
        /// Get / Set the table name.
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }


        #region IDaoWithKey<int,T> Members
        /// <summary>
        /// Create the entity in the datastore.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override T Create(T entity)
        {
            return entity;
        }


        /// <summary>
        /// Update the entity in the datastore.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override T Update(T entity)
        {
            return entity;
        }


        /// <summary>
        /// Get item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            string sql = string.Format("select * from {0} where Id = {1}", TableName, id);
            IList<T> result = _db.QueryNoParams<T>(sql, CommandType.Text, RowMapper);
            if (result == null || result.Count == 0)
                return default(T);

            return result[0];
        }


        /// <summary>
        /// Get all the entities from the datastore.
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetAll()
        {
            string sql = string.Format("select * from {0} ", TableName);
            IList<T> result = _db.QueryNoParams<T>(sql, CommandType.Text, RowMapper);
            return result;
        }


        /// <summary>
        /// Get all the items.
        /// </summary>
        /// <returns></returns>
        public virtual System.Collections.IList GetAllItems()
        {
            ArrayList list = new ArrayList();
            IList<T> allItems = GetAll();
            foreach (T item in allItems)
                list.Add(item);

            return list;
        }


        /// <summary>
        /// Get page of records using filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="table"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public virtual IList<T> GetByFilter(string filter, string table, int pageNumber, int pageSize, ref int totalRecords)
        {
            string procName = table + "_GetByFilter";
            List<DbParameter> dbParams = new List<DbParameter>();
            dbParams.Add(DbHelper.BuildInParam("@Filter", System.Data.DbType.String, filter));
            dbParams.Add(DbHelper.BuildInParam("@PageIndex", System.Data.DbType.Int32, pageNumber));
            dbParams.Add(DbHelper.BuildInParam("@PageSize", System.Data.DbType.Int32, pageSize));
            dbParams.Add(DbHelper.BuildOutParam("@TotalRows", System.Data.DbType.Int32));

            Tuple2<IList<T>, IDictionary<string, object>> result = DbHelper.Query<T>(
                procName, System.Data.CommandType.StoredProcedure, dbParams.ToArray(), _rowMapper, new string[] { "@TotalRows" });

            // Set the total records.
            totalRecords = (int)result.Second["@TotalRows"];
            return result.First;
        }


        /// <summary>
        /// Get recents posts by page
        /// </summary>
        /// <param name="table"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public virtual IList<T> GetRecent(string table, int pageNumber, int pageSize, ref int totalRecords)
        {
            string procName = table + "_GetRecent";
            List<DbParameter> dbParams = new List<DbParameter>();

            // Build input params to procedure.
            dbParams.Add(DbHelper.BuildInParam("@PageIndex", System.Data.DbType.Int32, pageNumber));
            dbParams.Add(DbHelper.BuildInParam("@PageSize", System.Data.DbType.Int32, pageSize));
            dbParams.Add(DbHelper.BuildOutParam("@TotalRows", System.Data.DbType.Int32));

            Tuple2<IList<T>, IDictionary<string, object>> result = DbHelper.Query<T>(
                procName, System.Data.CommandType.StoredProcedure, dbParams.ToArray(), _rowMapper, new string[] { "@TotalRows" });

            // Set the total records.
            totalRecords = (int)result.Second["@TotalRows"];
            return result.First;
        }


        /// <summary>
        /// Find all records matching the query string.
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public virtual IList<T> Find(string queryString)
        {
            return FindByQuery(queryString, true);
        }


        /// <summary>
        /// Find by the sql using the single parameter value.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IList<T> Find(string queryString, object value)
        {
            string filter = string.Format(queryString, value);
            return FindByQuery(filter, true);
        }


        /// <summary>
        /// Find by sql text using the parameters supplied.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IList<T> Find(string queryString, object[] values)
        {
            string filter = string.Format(queryString, values);
            return FindByQuery(filter, true);
        }


        /// <summary>
        /// Find by filter.
        /// </summary>
        /// <param name="queryString">The query, this can be either just a filter
        /// after the where clause or the entire sql</param>
        /// <returns></returns>
        public virtual IList<T> FindByQuery(string queryString, bool isFullSql)
        {
            string sql = queryString;
            if (!isFullSql)
            {
                string tableName = this.TableName;
                queryString = string.IsNullOrEmpty(queryString) ? string.Empty : " where " + queryString;
                sql = "select * from " + tableName + " " + queryString;
            }
            IList<T> results = _db.QueryNoParams<T>(sql, CommandType.Text, RowMapper);
            return results;
        }


        /// <summary>
        /// Delete by the entity id.
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            string sql = string.Format("delete from {0} where Id = {1}", TableName, id);
            _db.ExecuteNonQueryText(sql, null);
        }
        #endregion
    }
}
