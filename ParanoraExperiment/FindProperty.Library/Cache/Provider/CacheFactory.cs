using System;
using FindProperty.Lib.Ioc;
using Microsoft.Practices.Unity;

namespace FindProperty.Lib.Cache.Provider
{
    public class CacheFactory
    {
        private static ICache _cache = null;
        private static object _lock = new object();

        public static ICache Create()
        {
            try
            {
                lock (_lock)
                {
                    if (_cache == null)
                    {
                        _cache = IoCManager.Resolve<ICache>();
                    }
                }
                return _cache;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
