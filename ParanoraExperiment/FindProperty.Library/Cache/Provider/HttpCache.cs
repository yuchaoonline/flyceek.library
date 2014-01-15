using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;

namespace FindProperty.Lib.Cache.Provider
{
    public class HttpCache : ICache
    {
        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public void Set(string key, object objObject)
        {
            HttpRuntime.Cache.Insert(key, objObject);
        }

        public void Set(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, slidingExpiration);
        }

        public void Set(string key, object value, DateTime expirationTime)
        {
            throw new NotImplementedException();
        }
    }
}
