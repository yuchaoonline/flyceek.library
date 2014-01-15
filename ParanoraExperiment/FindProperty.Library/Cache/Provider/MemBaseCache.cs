using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;

namespace FindProperty.Lib.Cache.Provider
{
    public class MemBaseCache : ICache
    {
        private MemcachedClient _mc = new MemcachedClient();

        public object Get(string key)
        {
            return _mc.Get(key);
        }

        public void Set(string key, object value)
        {
            _mc.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value);
        }

        public void Set(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, DateTime expirationTime)
        {
            _mc.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, expirationTime);
        }
    }
}
