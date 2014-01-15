using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace IOCPThreading
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

    public sealed class IOCPThreadPool
    {
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern UInt32 CreateIoCompletionPort(UInt32 hFile, UInt32 hExistingCompletionPort, UInt32* puiCompletionKey, UInt32 uiNumberOfConcurrentThreads);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean CloseHandle(UInt32 hObject);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean PostQueuedCompletionStatus(UInt32 hCompletionPort, UInt32 uiSizeOfArgument, UInt32* puiUserArg, System.Threading.NativeOverlapped* pOverlapped);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        private unsafe static extern Boolean GetQueuedCompletionStatus(UInt32 hCompletionPort, UInt32* pSizeOfArgument, UInt32* puiUserArg, System.Threading.NativeOverlapped** ppOverlapped, UInt32 uiMilliseconds);

        private const UInt32 INVALID_HANDLE_VALUE = 0xffffffff;
        private const UInt32 INIFINITE = 0xffffffff;
        private const Int32 SHUTDOWN_IOCPTHREAD = 0x7fffffff;
        public delegate void USER_FUNCTION(int iValue);
        private UInt32 m_hHandle;
        private UInt32 GetHandle { get { return m_hHandle; } set { m_hHandle = value; } }

        private Int32 m_uiMaxConcurrency;

        private Int32 GetMaxConcurrency { get { return m_uiMaxConcurrency; } set { m_uiMaxConcurrency = value; } }


        private Int32 m_iMinThreadsInPool;

        private Int32 GetMinThreadsInPool { get { return m_iMinThreadsInPool; } set { m_iMinThreadsInPool = value; } }

        private Int32 m_iMaxThreadsInPool;

        private Int32 GetMaxThreadsInPool { get { return m_iMaxThreadsInPool; } set { m_iMaxThreadsInPool = value; } }


        private Object m_pCriticalSection;

        private Object GetCriticalSection { get { return m_pCriticalSection; } set { m_pCriticalSection = value; } }


        private USER_FUNCTION m_pfnUserFunction;

        private USER_FUNCTION GetUserFunction { get { return m_pfnUserFunction; } set { m_pfnUserFunction = value; } }


        private Boolean m_bDisposeFlag;

        /// <summary> SimType: Flag to indicate if the class is disposing </summary> 

        private Boolean IsDisposed { get { return m_bDisposeFlag; } set { m_bDisposeFlag = value; } }

        private Int32 m_iCurThreadsInPool;

        /// <summary> SimType: The current number of threads in the thread pool </summary> 

        public Int32 GetCurThreadsInPool { get { return m_iCurThreadsInPool; } set { m_iCurThreadsInPool = value; } }

        /// <summary> SimType: Increment current number of threads in the thread pool </summary> 

        private Int32 IncCurThreadsInPool() { return Interlocked.Increment(ref m_iCurThreadsInPool); }

        /// <summary> SimType: Decrement current number of threads in the thread pool </summary> 

        private Int32 DecCurThreadsInPool() { return Interlocked.Decrement(ref m_iCurThreadsInPool); }


        private Int32 m_iActThreadsInPool;

        /// <summary> SimType: The current number of active threads in the thread pool </summary> 

        public Int32 GetActThreadsInPool { get { return m_iActThreadsInPool; } set { m_iActThreadsInPool = value; } }

        /// <summary> SimType: Increment current number of active threads in the thread pool </summary> 

        private Int32 IncActThreadsInPool() { return Interlocked.Increment(ref m_iActThreadsInPool); }

        /// <summary> SimType: Decrement current number of active threads in the thread pool </summary> 

        private Int32 DecActThreadsInPool() { return Interlocked.Decrement(ref m_iActThreadsInPool); }


        private Int32 m_iCurWorkInPool;

        /// <summary> SimType: The current number of Work posted in the thread pool </summary> 

        public Int32 GetCurWorkInPool { get { return m_iCurWorkInPool; } set { m_iCurWorkInPool = value; } }

        /// <summary> SimType: Increment current number of Work posted in the thread pool </summary> 

        private Int32 IncCurWorkInPool() { return Interlocked.Increment(ref m_iCurWorkInPool); }

        /// <summary> SimType: Decrement current number of Work posted in the thread pool </summary> 

        private Int32 DecCurWorkInPool() { return Interlocked.Decrement(ref m_iCurWorkInPool); }

        public IOCPThreadPool(Int32 iMaxConcurrency, Int32 iMinThreadsInPool, Int32 iMaxThreadsInPool, USER_FUNCTION pfnUserFunction)
        {
            try
            {
                // Set initial class state 

                GetMaxConcurrency = iMaxConcurrency;

                GetMinThreadsInPool = iMinThreadsInPool;

                GetMaxThreadsInPool = iMaxThreadsInPool;

                GetUserFunction = pfnUserFunction;


                // Init the thread counters 

                GetCurThreadsInPool = 0;

                GetActThreadsInPool = 0;

                GetCurWorkInPool = 0;


                // Initialize the Monitor Object 

                GetCriticalSection = new Object();


                // Set the disposing flag to false 

                IsDisposed = false;


                unsafe
                {

                    // Create an IO Completion Port for Thread Pool use 
                    GetHandle = CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, null, (UInt32)GetMaxConcurrency);

                }


                // Test to make sure the IO Completion Port was created 

                if (GetHandle == 0)

                    throw new Exception("Unable To Create IO Completion Port");


                // Allocate and start the Minimum number of threads specified 

                Int32 iStartingCount = GetCurThreadsInPool;



                ThreadStart tsThread = new ThreadStart(IOCPFunction);

                for (Int32 iThread = 0; iThread < GetMinThreadsInPool; ++iThread)
                {

                    // Create a thread and start it 

                    Thread thThread = new Thread(tsThread);

                    thThread.Name = "IOCP " + thThread.GetHashCode();

                    thThread.Start();


                    // Increment the thread pool count 

                    IncCurThreadsInPool();

                }

            }


            catch
            {

                throw new Exception("Unhandled Exception");

            }

        }

        ~IOCPThreadPool()
        {

            if (!IsDisposed)

                Dispose();

        }

        public void Dispose()
        {

            try
            {

                // Flag that we are disposing this object 

                IsDisposed = true;


                // Get the current number of threads in the pool 

                Int32 iCurThreadsInPool = GetCurThreadsInPool;


                // Shutdown all thread in the pool 

                for (Int32 iThread = 0; iThread < iCurThreadsInPool; ++iThread)
                {
                    unsafe
                    {

                        bool bret = PostQueuedCompletionStatus(GetHandle, 4, (UInt32*)SHUTDOWN_IOCPTHREAD, null);

                    }

                }


                // Wait here until all the threads are gone 

                while (GetCurThreadsInPool != 0) Thread.Sleep(100);


                unsafe
                {

                    // Close the IOCP Handle 
                    CloseHandle(GetHandle);

                }

            }

            catch
            {

            }

        }
        private void IOCPFunction()
        {
            UInt32 uiNumberOfBytes;

            Int32 iValue;

            try
            {
                while (true)
                {

                    unsafe
                    {

                        System.Threading.NativeOverlapped* pOv;


                        // Wait for an event 

                        GetQueuedCompletionStatus(GetHandle, &uiNumberOfBytes, (UInt32*)&iValue, &pOv, INIFINITE);
                    }

                    // Decrement the number of events in queue 

                    DecCurWorkInPool();


                    // Was this thread told to shutdown 

                    if (iValue == SHUTDOWN_IOCPTHREAD)

                        break;


                    // Increment the number of active threads 

                    IncActThreadsInPool();


                    try
                    {
                        // Call the user function 
                        GetUserFunction(iValue);

                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    // Get a lock 

                    Monitor.Enter(GetCriticalSection);


                    try
                    {

                        // If we have less than max threads currently in the pool 

                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {

                            // Should we add a new thread to the pool 

                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {

                                if (IsDisposed == false)
                                {

                                    // Create a thread and start it 

                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);

                                    Thread thThread = new Thread(tsThread);

                                    thThread.Name = "IOCP " + thThread.GetHashCode();

                                    thThread.Start();


                                    // Increment the thread pool count 

                                    IncCurThreadsInPool();

                                }

                            }

                        }

                    }

                    catch
                    {

                    }


                    // Relase the lock 

                    Monitor.Exit(GetCriticalSection);


                    // Increment the number of active threads 

                    DecActThreadsInPool();

                }

            }


            catch (Exception ex)
            {
                string str = ex.Message;

            }


            // Decrement the thread pool count 

            DecCurThreadsInPool();

        }

        //public void PostEvent(Int32 iValue 
        public void PostEvent(int iValue)
        {

            try
            {

                // Only add work if we are not disposing 

                if (IsDisposed == false)
                {

                    unsafe
                    {

                        // Post an event into the IOCP Thread Pool 

                        PostQueuedCompletionStatus(GetHandle, 4, (UInt32*)iValue, null);

                    }


                    // Increment the number of item of work 

                    IncCurWorkInPool();


                    // Get a lock 

                    Monitor.Enter(GetCriticalSection);


                    try
                    {

                        // If we have less than max threads currently in the pool 

                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {

                            // Should we add a new thread to the pool 

                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {

                                if (IsDisposed == false)
                                {

                                    // Create a thread and start it 

                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);

                                    Thread thThread = new Thread(tsThread);

                                    thThread.Name = "IOCP " + thThread.GetHashCode();

                                    thThread.Start();


                                    // Increment the thread pool count 

                                    IncCurThreadsInPool();

                                }

                            }

                        }

                    }


                    catch
                    {

                    }


                    // Release the lock 

                    Monitor.Exit(GetCriticalSection);

                }

            }


            catch (Exception e)
            {

                throw e;

            }


            catch
            {

                throw new Exception("Unhandled Exception");

            }

        }

        public void PostEvent()
        {

            try
            {

                // Only add work if we are not disposing 

                if (IsDisposed == false)
                {

                    unsafe
                    {

                        // Post an event into the IOCP Thread Pool 

                        PostQueuedCompletionStatus(GetHandle, 0, null, null);

                    }


                    // Increment the number of item of work 

                    IncCurWorkInPool();


                    // Get a lock 

                    Monitor.Enter(GetCriticalSection);


                    try
                    {

                        // If we have less than max threads currently in the pool 

                        if (GetCurThreadsInPool < GetMaxThreadsInPool)
                        {

                            // Should we add a new thread to the pool 

                            if (GetActThreadsInPool == GetCurThreadsInPool)
                            {

                                if (IsDisposed == false)
                                {

                                    // Create a thread and start it 

                                    ThreadStart tsThread = new ThreadStart(IOCPFunction);

                                    Thread thThread = new Thread(tsThread);

                                    thThread.Name = "IOCP " + thThread.GetHashCode();

                                    thThread.Start();


                                    // Increment the thread pool count 

                                    IncCurThreadsInPool();

                                }

                            }

                        }

                    }


                    catch
                    {

                    }


                    // Release the lock 

                    Monitor.Exit(GetCriticalSection);

                }

            }

            catch
            {

                throw new Exception("Unhandled Exception");

            }

        }

    }

}