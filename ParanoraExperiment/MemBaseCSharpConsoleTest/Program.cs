using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Enyim.Caching;

namespace MemBase
{
    class Program
    {
        static void Main(string[] args)
        {

            MemcachedClient mc = new MemcachedClient();

            //string k="AABBCCDDEE";
            //string v = "1";
            //mc.Store(Enyim.Caching.Memcached.StoreMode.Add, k, v, DateTime.Now.AddSeconds(1));

            //Thread.Sleep(30000);

            //string getV = mc.Get(k) as string ;


            mc.FlushAll();

            Console.Read();
        }

    }
}
