using System; 
using System.Threading;  // Included for the Thread.Sleep call 
using System.Runtime.InteropServices;
using IOCPThreading;

namespace IOCPDemo 
{ 
    //============================================================================= 
    /// <summary> Sample class for the threading class </summary> 
    public class UtilThreadingSample 
    { 
        //*****************************************************************************    
        /// <summary> Test Method </summary> 
        //static void Main() 
        //{ 
        //    // Create the MSSQL IOCP Thread Pool 
        //    IOCPThreadPool pThreadPool = new IOCPThreadPool(0, 10, 20, new IOCPThreadPool.USER_FUNCTION(IOCPThreadFunction)); 
       
        //    //for(int i =1;i<10000;i++) 
        //    { 
        //        pThreadPool.PostEvent(1234); 
        //    } 
       
        //    Thread.Sleep(100); 
       
        //    pThreadPool.Dispose(); 
        //} 
     
        //******************************************************************** 
        /// <summary> Function to be called by the IOCP thread pool.  Called when 
        ///           a command is posted for processing by the SocketManager </summary> 
        /// <param name="iValue"> The value provided by the thread posting the event </param> 
        static public void IOCPThreadFunction(int iValue) 
        { 
            try 
            { 
                Console.WriteLine("Value: {0}", iValue.ToString()); 
                Thread.Sleep(3000); 
            } 
       
            catch (Exception pException) 
            { 
                Console.WriteLine(pException.Message); 
            } 
        } 
    } 
 
} 