using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using mongoDb.lib;

namespace mongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            string cns = System.Configuration.ConfigurationManager.AppSettings["MongoDatabaseConnectionString"];
            string db = "test";
            string tb="user";
            var mc = new MongoContext<BsonDocument>(cns, db);
            mc[tb].Drop();
            mc[tb].Insert(new BsonDocument(){{"id",Guid.NewGuid().ToString()},{"name","flyceek"},{"age",1} });
        }
    }
}
