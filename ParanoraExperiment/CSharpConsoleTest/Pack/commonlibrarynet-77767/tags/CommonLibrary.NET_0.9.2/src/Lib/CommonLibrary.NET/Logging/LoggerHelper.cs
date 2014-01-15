using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// Helper class for logging.
    /// </summary>
    public class LoggerHelper
    {
        /// <summary>
        /// Logs to the console.
        /// </summary>
        /// <typeparam name="T">The datatype of the caller that is logging the event.</typeparam>
        /// <param name="level">The log level</param>
        /// <param name="message">Message to log</param>
        /// <param name="ex">Exception to log</param>
        /// <param name="args">Additional arguments.</param>
        public void LogToConsole<T>(LogLevel level, string message, Exception ex, object[] args)
        {
            LogEvent logevent = BuildLogEvent(typeof(T), level, message, ex, null);
            Console.WriteLine(logevent.FinalMessage);
        }


        /// <summary>
        /// Construct the logevent using the values supplied.
        /// Fills in other data values in the log event.
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="message">Message to log</param>
        /// <param name="ex">Exception to log</param>
        /// <param name="args">Additional args.</param>
        public static LogEvent BuildLogEvent(Type logType, LogLevel level, object message, System.Exception ex, object[] args)
        {
            LogEvent logevent = new LogEvent();
            logevent.Level = level;
            logevent.Message = message;
            logevent.Error = ex;
            logevent.Args = args;
            logevent.Computer = System.Environment.MachineName;
            logevent.CreateTime = DateTime.Now;
            logevent.ThreadName = System.Threading.Thread.CurrentThread.Name;
            logevent.UserName = AuthService.UserShortName;
            logevent.LogType = logType;
            logevent.FinalMessage = LoggerFormatter.Format(null, logevent);
            return logevent;
        }
    }



    /// <summary>
    /// Log formatter.
    /// </summary>
    public class LoggerFormatter
    {
        /// <summary>
        /// Quick formatter that toggles between delimited and xml.
        /// </summary>
        /// <param name="formatter"></param>
        /// <param name="logEvent"></param>
        public static string Format(string formatter, LogEvent logEvent)
        {
            if (string.IsNullOrEmpty(formatter))
                return Format(logEvent);

            if (formatter.ToLower().Trim() == "xml")
                return FormatXml(logEvent);

            return Format(logEvent);
        }


        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string Format(LogEvent logEvent)
        {
            string msg = StringHelpers.ConvertToString(logEvent.Args);
            string message = logEvent.Message.ToString();
            msg = string.IsNullOrEmpty(msg) ? message.ToString() : message.ToString() + " - " + msg;

            // Build a delimited string
            // <time>:<thread>:<level>:<loggername>:<message>
            string line = logEvent.CreateTime.ToString();

            if (!string.IsNullOrEmpty(logEvent.ThreadName)) line += ":" + logEvent.ThreadName;
            line += ":" + logEvent.Level.ToString();
            line += ":" + logEvent.LogType.Name;
            line += ":" + msg;
            return line;
        }


        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static string FormatXml(LogEvent logEvent)
        {
            string msg = StringHelpers.ConvertToString(logEvent.Args);
            string message = logEvent.Message.ToString();
            msg = string.IsNullOrEmpty(msg) ? message.ToString() : message.ToString() + " - " + msg;

            // Build a delimited string
            // <time>:<thread>:<level>:<loggername>:<message>
            string line = string.Format("<time>{0}</time>", logEvent.CreateTime.ToString());

            if (!string.IsNullOrEmpty(logEvent.ThreadName)) line += string.Format("<thread>{0}</thread>", logEvent.ThreadName);
            line += string.Format("<level>{0}</level>", logEvent.Level.ToString());
            line += string.Format("<type>{0}</type>", logEvent.LogType.Name);
            line += string.Format("<message>{0}</message>", msg);
            return line;
        }
    }
}
