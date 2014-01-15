using FindProperty.Lib.Cache.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component
{
    public abstract class CacheComponetFundation
    {
        private static object _lockObj = new object();

        public ICache CacheProvider
        {
            get;
            set;
        }

        public CacheComponetFundation(ICache cachrProvider)
        {
            CacheProvider = cachrProvider;
        }

        public CacheComponetFundation(): this(CacheFactory.Create())
        {

        }

        public virtual object GetValue(string key)
        {
            object result = null;
            lock (_lockObj)
            {
                result = CacheProvider.Get(key);
            }
            return result;
        }

        public virtual void SetValue(string key, object value, int cacheSeconde)
        {
            lock (_lockObj)
            {
                if (cacheSeconde > 0)
                {
                    CacheProvider.Set(key, value, DateTime.Now.AddSeconds(cacheSeconde));
                }
                else
                {
                    CacheProvider.Set(key, value);
                }
            }
        }
    }
}
