using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.DBUtility.Mongo
{
    public static class MongoConnctionArbitrateFactory
    {
        private static MongoConnctionArbitrate _mongoConnctionArbitrate = null;

        public static MongoConnctionArbitrate CreateDefault()
        {
            if (_mongoConnctionArbitrate == null)
            {
                _mongoConnctionArbitrate = new MongoConnctionArbitrate(new MongoDbCachedSignal());
            }

            return _mongoConnctionArbitrate;
        }
    }
}
