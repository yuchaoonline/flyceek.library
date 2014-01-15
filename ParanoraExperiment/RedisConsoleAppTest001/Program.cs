using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisConsoleAppTest001
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisClient client = new RedisClient("127.0.0.1", 6379);

            //存储用户名和密码  
            client.Set<string>("username", "paranora");
            client.Set<int>("pwd", 123456);
            string username = client.Get<string>("username");
            int pwd = client.Get<int>("pwd");

            Console.ReadKey();
        }
    }
}
