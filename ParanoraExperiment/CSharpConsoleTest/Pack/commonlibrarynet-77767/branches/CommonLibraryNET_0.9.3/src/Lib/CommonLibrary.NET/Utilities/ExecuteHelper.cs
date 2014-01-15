using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using ComLib.Logging;


namespace ComLib
{
    /// <summary>
    /// Action that returns void
    /// </summary>
    public delegate void ActionVoid();



    public class ExecuteHelper
    {
        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatch(ActionVoid action)
        {
            TryCatchLog(string.Empty, null, action);
        }

        
        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchLog(ActionVoid action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            TryCatchLog(string.Empty, log, action);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchLog(string errorMessage, ActionVoid action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            TryCatchLog(errorMessage, log, action);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchLog(string errorMessage, ILog log, ActionVoid action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(errorMessage, ex, null);
            }
        }


        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchHandle(ActionVoid actionCode)
        {
            TryCatchFinallySafe(string.Empty, actionCode, (ex) => HandleException(ex), null);
        }


        /// <summary>
        /// Executes an action inside a try catch and does not do anything when
        /// an exception occurrs.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchHandle(ActionVoid actionCode, ActionVoid finallyCode)
        {
            TryCatchFinallySafe(string.Empty, actionCode, (ex) => HandleException(ex), finallyCode);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatchFinallySafe(string errorMessage, ActionVoid action, Action<Exception> exceptionHandler, ActionVoid finallyHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }
            finally
            {
                if (finallyHandler != null)
                {
                    TryCatchHandle(() => finallyHandler());
                }
            }
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static void TryCatch( ActionVoid action, Action<Exception> exceptionHandler)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (exceptionHandler != null)
                    exceptionHandler(ex);
            }            
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static T TryCatchLog<T>(string errorMessage, Func<T> action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            return TryCatchLogRethrow<T>(errorMessage, log, false, action);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static T TryCatchLog<T>(string errorMessage, ILog log, Func<T> action)
        {
            return TryCatchLogRethrow<T>(errorMessage, log, false, action);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static T TryCatchLogRethrow<T>(string errorMessage, ILog log, bool rethrow, Func<T> action)
        {
            T result = default(T);
            try
            {
                result = action();
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(errorMessage, ex, null);
                if( rethrow) throw ex;
            }
            return result;
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions
        /// </summary>
        /// <param name="action"></param>
        public static BoolMessageItem<T> TryCatchLogGet<T>(string errorMessage, Func<T> action)
        {
            ILog log = Logger.GetNew<ExecuteHelper>();
            return TryCatchLogGet<T>(errorMessage, log, action);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static BoolMessageItem<T> TryCatchLogGet<T>(string errorMessage, ILog log, Func<T> action)
        {        
            T result = default(T);
            bool success = false;
            string message = string.Empty;
            try
            {
                result = action();
                success = true;
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(errorMessage, ex, null);
                message = ex.Message;
            }
            return new BoolMessageItem<T>(result, success, message);
        }


        /// <summary>
        /// Executes an action inside a try catch and logs any exceptions.
        /// </summary>
        /// <param name="action"></param>
        public static BoolMessageItem TryCatchLogGetBoolMessageItem(string errorMsg, ILog log, Func<BoolMessageItem> action)
        {
            BoolMessageItem result = BoolMessageItem.False;
            string fullMessage = string.Empty;
            try
            {
                result = action();
            }
            catch (Exception ex)
            {
                if (log != null) log.Error(errorMsg, ex, null);
                fullMessage = errorMsg + Environment.NewLine
                        + ex.Message + Environment.NewLine
                        + ex.Source + Environment.NewLine
                        + ex.StackTrace;
                result = new BoolMessageItem(null, false, fullMessage);
            }
            return result;
        }


        /// <summary>
        /// Handle the highest level application exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleException(Exception ex)
        {
            string message = string.Format("{0}, {1} \r\n {2}", ex.Message, ex.Source, ex.StackTrace);
            Console.WriteLine(message);
            try
            {
                ILog log = Logger.GetNew<ExecuteHelper>();
                log.Error(null, ex, null);
            }
            catch {}
        }


        /// <summary>
        /// Handle the highest level application exception.
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleExceptionLight(Exception ex)
        {
            string message = string.Format("{0}, {1}", ex.Message, ex.Source);
            Console.WriteLine(message);
            try
            {
                ILog log = Logger.GetNew<ExecuteHelper>();
                log.Error(message, null, null);
            }
            catch {}
        }
    }
}
