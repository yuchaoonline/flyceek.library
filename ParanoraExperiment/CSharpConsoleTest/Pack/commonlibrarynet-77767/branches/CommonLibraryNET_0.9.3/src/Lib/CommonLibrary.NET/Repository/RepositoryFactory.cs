using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComLib;
using ComLib.Database;
using ComLib.Entities;


namespace ComLib.Entities
{
    /// <summary>
    /// Repository factory for building/initializing a entity specific repository.
    /// </summary>
    public class RepositoryConfigurator
    {
        private static Action<IRepositoryConfigurable> _configurator;
        private static RepositoryConfiguratorDefault _defaultConfigurator;


        /// <summary>
        /// Initialize the repository configurator.
        /// </summary>
        /// <param name="configurator"></param>
        public static void Init(Action<IRepositoryConfigurable> configurator)
        {
            _configurator = configurator;
        }
        

        /// <summary>
        /// Initialize the repository configurator using only the connection provided.
        /// This results in using the RepositoryConfiguratorDefault.
        /// </summary>
        /// <param name="configurator"></param>
        public static void Init(ConnectionInfo connection)
        {
            _defaultConfigurator = new RepositoryConfiguratorDefault(connection);
            _configurator = new Action<IRepositoryConfigurable>(_defaultConfigurator.Configure);
        }


        /// <summary>
        /// Initialize the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static IEntityRepository<T> Create<T>(Func<IEntityRepository<T>> action) where T: IEntity
        {
            // Allow Entity to create repository but initialize it here.
            IEntityRepository<T> repository = action();
            _configurator(repository);
            return repository;
        }


        /// <summary>
        /// Initialize the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static void Configure(IRepositoryConfigurable repository)
        {
            // Configure the repository but initialize it here.
            _configurator(repository);
        }
    }



    /// <summary>
    /// Default repository configurator to set the connection connection string and dbhelper.
    /// </summary>
    public class RepositoryConfiguratorDefault
    {
        /// <summary>
        /// The connection object.
        /// </summary>
        public ConnectionInfo Connection { get; set; }


        /// <summary>
        /// Initialize the connection.
        /// </summary>
        /// <param name="connection"></param>
        public RepositoryConfiguratorDefault(ConnectionInfo connection)
        {
            Connection = connection;
        }


        /// <summary>
        /// Configure the repository with the connection and dbhelper.
        /// </summary>
        /// <param name="repository"></param>
        public void Configure(IRepositoryConfigurable repository)
        {
            repository.Connection = Connection;
            repository.DbHelper = new DBHelper(Connection);
        }
    }
}
