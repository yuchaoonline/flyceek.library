using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace IOCP.Lib
{
    public class Win32IocpApi
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeFileHandle CreateIoCompletionPort(IntPtr FileHandle,
            IntPtr ExistingCompletionPort,
            IntPtr CompletionKey,
            uint NumberOfConcurrentThreads);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]

        public static extern bool GetQueuedCompletionStatus(SafeFileHandle CompletionPort,
            out uint lpNumberOfBytesTransferred,
            out IntPtr lpCompletionKey,
            out IntPtr lpOverlapped,
            uint dwMilliseconds);

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode,SetLastError=true)]
        public static extern bool PostQueuedCompletionStatus(SafeFileHandle CompletionPort,
            uint dwNumberOfBytesTransferred,
            IntPtr dwCompletionKey,
            IntPtr lpOverlapped);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class PER_IO_DATA
    {
        public string Data;
    }    
}
