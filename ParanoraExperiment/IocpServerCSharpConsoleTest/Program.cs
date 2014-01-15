using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using IocpServer.Lib;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;
using System.Net;
namespace IocpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8899));
            socket.Listen(128);

            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            var CompletionPort = Win32IocpApi.CreateIoCompletionPort(new IntPtr(-1), IntPtr.Zero, IntPtr.Zero, 0);
            CompletionPort = Win32IocpApi.CreateIoCompletionPort(socket.Handle, CompletionPort.DangerousGetHandle(), IntPtr.Zero, 0);

            Thread thread = new Thread(ServerProc);
            thread.Start(CompletionPort);

            Console.WriteLine("服务器开始监听。");

        }

        static void Win32IocpInit()
        {
            
        }

        static void ServerProc(object CompletionPortID)
        {
            IntPtr PerHandleData;
            IntPtr lpOverlapped;
            uint BytesTransferred;

            var CompletionPort = (SafeFileHandle)CompletionPortID;

            while (Win32IocpApi.GetQueuedCompletionStatus(CompletionPort, out BytesTransferred,
                                          out PerHandleData,
                                          out lpOverlapped,
                                         0xffffffff))
            {
                if (lpOverlapped != IntPtr.Zero)
                {
                    GCHandle gch = GCHandle.FromIntPtr(lpOverlapped);

                    var per_HANDLE_DATA = (PER_IO_DATA)gch.Target;

                    Console.WriteLine("{0}-工作线程收到数据：{1}", Thread.CurrentThread.GetHashCode(), per_HANDLE_DATA.Data);

                    gch.Free();
                }
            }
        }
    }
}
