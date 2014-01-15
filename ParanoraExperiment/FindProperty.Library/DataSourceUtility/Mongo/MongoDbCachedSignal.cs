using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Cache;
using FindProperty.Lib.Cache.Provider;

namespace FindProperty.Lib.DBUtility.Mongo
{
    public class MongoDbCachedSignal:IMongoDbSignal
    {
        public int GetSignal()
        {
            object v = CacheFactory.Create().Get("ChangeMongoDb");
            int signal = 0;

            if(v!=null&&!string.IsNullOrEmpty(v.ToString()))
            {
                int signalInt = 0;
                if (!int.TryParse(v.ToString(), out signalInt))
                {
                    signal = 0;
                }
                else
                {
                    signal = signalInt;
                }
            }
            return signal;
        }
    }
}
