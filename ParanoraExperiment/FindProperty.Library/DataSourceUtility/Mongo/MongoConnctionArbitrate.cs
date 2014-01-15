using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.DBUtility.Mongo
{
    public class MongoConnctionArbitrate:IMongoConnctionArbitrate
    {
        private IMongoDbSignal _mongoDbSignal;

        public MongoConnctionArbitrate(IMongoDbSignal signal)
        {
            _mongoDbSignal = signal;
        }

        public MongoDbSignalValue GetConnction(IMongoDbSignal signal)
        {
            MongoDbSignalValue signalValue = new MongoDbSignalValue();
            int v = signal.GetSignal();
            string connectionStr = string.Empty;

            switch (v)
            {
                case 1:
                    connectionStr = ConfigInfo.MongoDatabasePrimaryConnectionString;
                    break;
                case 0:
                    connectionStr = ConfigInfo.MongoDatabaseConnectionString;
                    break;
            }

            signalValue.Id = v.ToString();
            signalValue.ConnectionString = connectionStr;

            return signalValue;
        }

        public MongoDbSignalValue GetConnction()
        {
            return GetConnction(this._mongoDbSignal);
        }
    }
}
