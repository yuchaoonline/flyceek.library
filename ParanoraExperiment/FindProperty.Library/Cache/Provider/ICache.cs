using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindProperty.Lib.Cache.Provider
{
    
    public interface ICache
    {
        object Get(string key);

        void Set(string key, object value);

        void Set(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration);

        void Set(string key, object value, DateTime expirationTime);
    }

}
