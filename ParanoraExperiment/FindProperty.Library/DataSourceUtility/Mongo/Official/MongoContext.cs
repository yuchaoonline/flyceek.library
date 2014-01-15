using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace FindProperty.Lib.DBUtility.Mongo.Official
{
    public class MongoServerGroup
    {
        private static MongoServer _mainServer;
        private static MongoServer _primaryServer;
        private static MongoServer _infoServer;

        public MongoServer this[string id]
        {
            get
            {
                MongoServer server = null;
                switch (id)
                {
                    case "0":
                        server= _mainServer;
                        break;
                    case "1":
                        server= _primaryServer;
                        break;
                    case "2":
                        server = _infoServer;
                        break;
                }
                return server;
            }
            set
            {
                switch (id)
                {
                    case "0":
                        if (_mainServer == null)
                        {
                            _mainServer = value;
                        }
                        break;
                    case "1":
                        if (_primaryServer == null)
                        {
                            _primaryServer = value;
                        }
                        break;
                    case "2":
                        if (_infoServer == null)
                        {
                            _infoServer = value;
                        }
                        break;
                }
            }
        }
    }

    public class MongoContext<T> : IDisposable where T : class
    {
        private static MongoServerGroup _serverGroup = new MongoServerGroup();
        //private static MongoServer _server;
        private MongoDatabase _db;
        private MongoCollection<T> _collection;
        //private MongoClient _mc;

        private string _id = string.Empty;

        public static object sycnobj = new object();

        public MongoServer Server { get { return _serverGroup[_id]; } }
        
        public MongoDatabase DB { get { return _db; } }
        public MongoCollection<T> Collection { get { return _collection; } }

        public MongoCollection<T> this[string collectionName]
        {
            get
            {
                _collection = _db.GetCollection<T>(collectionName);
                return _collection;
            }
        }

        public MongoContext(string connectionString, string dataBaseName, string id)
        {
            _id = id;
            lock (sycnobj)
            {
                var mc = new MongoClient(connectionString);
                _serverGroup[_id] = mc.GetServer();
                //_serverGroup[_id] = MongoServer.Create(connectionString);
            }
            _db = Server.GetDatabase(dataBaseName);
        }
       


        public void Dispose()
        {
            if (Server != null)
            {
                //_server.Disconnect();
                //_server = null;
            }
        }
    }
}
