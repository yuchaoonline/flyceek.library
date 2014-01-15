using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using IOCP.Lib;
using Microsoft.Win32.SafeHandles;

namespace IOCP.lib
{
    public class Win32IocpApiTest
    {
        public static unsafe void TestIOCPApi()
        {
            var CompletionPort = Win32IocpApi.CreateIoCompletionPort(new IntPtr(-1), IntPtr.Zero, IntPtr.Zero, 2000);

            if (CompletionPort.IsInvalid)
            {
                Console.WriteLine("CreateIoCompletionPort 出错:{0}", Marshal.GetLastWin32Error());
            }

            for (var i = 0; i < 10; i++)
            {
                new Thread(ThreadProc).Start(CompletionPort);
            }


            for (var i = 0; i < 2000; i++)
            {
                new Thread(() =>
                {
                    var PerIOData = new PER_IO_DATA();
                    var gch = GCHandle.Alloc(PerIOData);
                    PerIOData.Data = "发送的数据" + i.ToString() ;

                    Win32IocpApi.PostQueuedCompletionStatus(CompletionPort, (uint)sizeof(IntPtr), IntPtr.Zero, (IntPtr)gch);

                    Console.WriteLine("{0}-工作线程发送数据：{1}", Thread.CurrentThread.GetHashCode(), PerIOData.Data);

                }).Start();
            }

            Console.ReadKey();

        }

        static void ThreadProc(object CompletionPortID)
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