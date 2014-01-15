using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using ComLib;
using ComLib.Database;

using ComLib.Entities;


namespace ComLib.Entities
{
    /// <summary>
    /// Repository base class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T>  where T : IEntity
    {
        protected ConnectionInfo _connectionInfo;
        protected IEntityRowMapper<T> _rowMapper;
        protected IDBHelper _db;


        /// <summary>
        /// The database helper
        /// </summary>
        public IDBHelper DbHelper
        {
            get { return _db; }
            set { _db = value; }
        }

        
        /// <summary>
        /// The connection info object
        /// </summary>
        public ConnectionInfo Connection
        {
            get { return _connectionInfo; }
            set { _connectionInfo = value; }
        }
        

        /// <summary>
        /// Connection info object.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionInfo.ConnectionString; }
        }


        /// <summary>
        /// Entity row mapper.
        /// </summary>
        public IEntityRowMapper<T> RowMapper
        {
            get { return _rowMapper; }
            set { _rowMapper = value; }
        }


        /// <summary>
        /// Create the entity in the datastore.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract T Create(T entity);


        /// <summary>
        /// Update the entity in the datastore.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract T Update(T entity);


        /// <summary>
        /// Save the entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Save(T entity)
        {
            if (entity.IsPersistant())
                return Update(entity);
            else
                return Create(entity);
        }


        /// <summary>
        /// Delete by the entity.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void DeleteByEntity(T entity)
        {
            int id = entity.Id;
            Delete(id);
        }


        /// <summary>
        /// Delete by the id.
        /// </summary>
        /// <param name="id"></param>
        public abstract void Delete(int id);      
    }
}
