using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEFCSharpConsoleTest.Lib;

namespace MEFCSharpConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = new MEFTestClass();
            o.Test();
            o.SayOperate.SayHello();

            Console.ReadKey();
        }
    }
}
