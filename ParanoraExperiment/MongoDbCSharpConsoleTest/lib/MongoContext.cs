using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace mongoDb.lib
{
    public class MongoContext<T> : IDisposable where T : class
    {
        private static MongoServer _server;
        private MongoDatabase _db;
        private MongoCollection<T> _collection;

        public static object sycnobj = new object();

        public MongoServer Server { get { return _server; } }
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

        public MongoContext(string connectionString, string dataBaseName)
        {
            lock (sycnobj)
            {
                var mc = new MongoClient(connectionString);
                if (_server == null)
                {
                    _server = mc.GetServer(); ;
                }
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
