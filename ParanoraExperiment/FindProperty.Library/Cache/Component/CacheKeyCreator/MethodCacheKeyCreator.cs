using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Cache.Component.CacheKeyCreator
{
    public class MethodCacheKeyCreator : ICacheKeyCreator
    {
        public string CreateKey(params object[] appendArgs)
        {
            string cacheKey = appendArgs[0].ToString();
            ICollection methodArgs = appendArgs[1] as ICollection;
            string parameStrs = string.Empty;

            foreach (var item in methodArgs)
            {
                if (item != null)
                {
                    parameStrs += item.ToString().ToUpper().Trim() + "_";
                }
            }
            parameStrs = parameStrs.TrimEnd('_');
            if (parameStrs.Length > 150)
            {
                byte[] buffer = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(parameStrs));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    builder.Append(buffer[i].ToString("X"));
                }
                parameStrs = builder.ToString();
            }
            if (parameStrs != string.Empty)
            {
                parameStrs = Convert.ToBase64String(Encoding.UTF8.GetBytes(parameStrs));
                cacheKey += "[" + parameStrs + "]";
            }

            return cacheKey;
        }
    }
}
