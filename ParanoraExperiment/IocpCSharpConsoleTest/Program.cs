using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IOCP.lib;

namespace IOCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Win32IocpApiTest.TestIOCPApi();
        }
    }
}
