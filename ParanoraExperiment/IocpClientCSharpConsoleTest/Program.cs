using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IocpClient.Lib;
using Microsoft.Win32.SafeHandles;

namespace IocpClient
{
    class Program
    {
        static unsafe void Main(string[] args)
        {


            IPEndPoint ipendPoint=new IPEndPoint(IPAddress.Parse("127.0.0.1"),8899);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);     

            while (true)
            {
                var k = Console.ReadLine();
                if (string.IsNullOrEmpty(k))
                {
                    continue;
                }
                try
                {
                    if (!socket.Connected)
                    {
                        socket.Connect(ipendPoint);
                    }
                    socket.Send(System.Text.Encoding.Unicode.GetBytes(k));
                }
                catch (Exception err)
                {                    
                    if (socket != null)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                    throw err;
                }
            }
        }

    }
}
