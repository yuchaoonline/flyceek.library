/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace CommonLibrary
{
    /// <summary>
    /// A record in the log.
    /// </summary>
    public class LogEvent
    {
        /// <summary>
        /// The log level.
        /// </summary>
        public LogLevel Level;


        /// <summary>
        /// Message that is logged.
        /// </summary>
        public object Message;


        /// <summary>
        /// This is the final message that is printed.
        /// </summary>
        public string FinalMessage;


        /// <summary>
        /// Exception passed.
        /// </summary>
        public Exception Error;


        /// <summary>
        /// Additional arguments passed by caller.
        /// </summary>
        public object[] Args;


        /// <summary>
        /// Name of the computer.
        /// </summary>
        public string Computer;


        /// <summary>
        /// Create time.
        /// </summary>
        public DateTime CreateTime;


        /// <summary>
        /// The name of the currently executing thread that created this log entry.
        /// </summary>
        public string ThreadName;


        /// <summary>
        /// Name of the user.
        /// </summary>
        public string UserName;


        /// <summary>
        /// The exception.
        /// </summary>
        public Exception Ex;


        /// <summary>
        /// The data type of the caller that is logging the event.
        /// </summary>
        public Type LogType;


        /// <summary>
        /// Enable default constructor.
        /// </summary>
        public LogEvent() { }


        public LogEvent(LogLevel level, string message, Exception ex)
        {
            Level = level;
            Message = message;
            Ex = ex;
        }
    }



    /// <summary>
    /// Level for the logging.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug level
        /// </summary>
        Debug,


        /// <summary>
        /// Info level
        /// </summary>
        Info,


        /// <summary>
        /// Warn level
        /// </summary>
        Warn,


        /// <summary>
        /// Error level
        /// </summary>
        Error,


        /// <summary>
        /// Fatal level
        /// </summary>
        Fatal,


        /// <summary>
        /// Used to always log a message regardless of loglevel
        /// </summary>
        Message
    };



    /// <summary>
    /// Settings for a logger.
    /// </summary>
    public class LogSettings
    {
        /// <summary>
        /// Current log level.
        /// </summary>
        public LogLevel Level;        
    }



    /// <summary>
    /// Provides basic methods for implementation classes,
    /// including the Wrapper class around Log4Net.
    /// </summary>
    public abstract class LoggerBase : ILog
    {
        #region Protected Data
        protected ILog _parent;
        protected ReaderWriterLock _readwriteLock = new ReaderWriterLock();
        protected int _lockMilliSecondsForRead = 1000;
        protected int _lockMilliSecondsForWrite = 1000;
        #endregion


        #region Constructors
        /// <summary>
        /// Default logger.
        /// </summary>
        public LoggerBase() { }


        /// <summary>
        /// Initialize logger with default settings.
        /// </summary>
        public LoggerBase(Type type)
        {            
            this.Name = type.FullName;
            Settings = new LogSettings();
            Settings.Level = LogLevel.Info;
        }


        /// <summary>
        /// Initialize logger with default settings.
        /// </summary>
        public LoggerBase(string name)
        {
            this.Name = name;
            Settings = new LogSettings();
            Settings.Level = LogLevel.Info;
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// Name of this logger.
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// Get / set the parent of this logger.
        /// </summary>
        public ILog Parent { get; set; }


        /// <summary>
        /// Log settings.
        /// </summary>
        public LogSettings Settings { get; set; }


        /// <summary>
        /// Log level.
        /// </summary>
        public virtual LogLevel Level
        {
            get { return Settings.Level; }
            set { ExecuteWrite(() => Settings.Level = value); }
        }


        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDebugEnabled { get { return IsEnabled(LogLevel.Debug); } }


        /// <summary>
        /// Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsInfoEnabled { get { return IsEnabled(LogLevel.Info); } }


        /// <summary>
        /// Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsWarnEnabled { get { return IsEnabled(LogLevel.Warn); } }


        /// <summary>
        /// Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsErrorEnabled { get { return IsEnabled(LogLevel.Error); } }


        /// <summary>
        /// Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsFatalEnabled { get { return IsEnabled(LogLevel.Fatal); } }
        #endregion


        #region Public Methods
        /// <summary>
        /// Get a logger by it's name.
        /// </summary>
        /// <param name="logger"></param>
        public virtual ILog this[string loggerName]
        {
            get { return this; }
        }


        /// <summary>
        /// Get logger at the specified index.
        /// This is a single logger and this call will always return 
        /// referece to self.
        /// </summary>
        /// <param name="logIndex"></param>
        /// <returns></returns>
        public virtual ILog this[int logIndex]
        {
            get { return this; }
        }
        

        /// <summary>
        /// Whether or not the level specified is enabled.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            return level >= Settings.Level;
        }


        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <remarks>This is the method to override in any logger that extends this class.</remarks>
        public abstract void Log(LogEvent logEvent);


        /// <summary>
        /// Flush the log entries to output.
        /// </summary>
        public virtual void Flush()
        {
            // Nothing here.
        }


        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Warn(object message, System.Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Warn)) InternalLog(LogLevel.Warn, message, ex, args);
        }


        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Error(object message, System.Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Error)) InternalLog(LogLevel.Error, message, ex, args);
        }


        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Debug(object message, System.Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Debug)) InternalLog(LogLevel.Debug, message, ex, args);
        }


        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Fatal(object message, System.Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Fatal)) InternalLog(LogLevel.Fatal, message, ex, args);
        }


        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Info(object message, System.Exception ex, object[] args)
        {
            if (IsEnabled(LogLevel.Info)) InternalLog(LogLevel.Info, message, ex, args);
        }


        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="args">The args.</param>
        public virtual void Message(object message, System.Exception ex, object[] args)
        {
            InternalLog(LogLevel.Message, message, ex, args);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public virtual void InternalLog(LogLevel level, object message, System.Exception ex, object[] args)
        {
            LogEvent logevent = BuildLogEvent(level, message, ex, args);
            Log(logevent);
        }


        /// <summary>
        /// Shutdown logger.
        /// </summary>
        public virtual void ShutDown()
        {
            Console.WriteLine("Shutting down logger " + this.Name);
        }
        #endregion


        #region Protected Methods
        /// <summary>
        /// Construct the logevent using the values supplied.
        /// Fills in other data values in the log event.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        public virtual LogEvent BuildLogEvent(LogLevel level, object message, System.Exception ex, object[] args)
        {
            return LoggerHelper.BuildLogEvent(this.GetType(), level, message, ex, args);
        }


        /// <summary>
        /// Builds the log message using message and arguments.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        protected virtual string BuildMessage(LogEvent logEvent)
        {
            return LoggerFormatter.Format("", logEvent);
        }
        #endregion
        

        #region Synchronization Helper Methods
        /// <summary>
        /// Exectutes the action under a read operation after
        /// aquiring the reader lock.
        /// </summary>
        /// <param name="executor"></param>
        protected void ExecuteRead(ActionVoid executor)
        {
            AcquireReaderLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ReleaseReaderLock();
            }
        }


        /// <summary>
        /// Exectutes the action under a write operation after
        /// aquiring the writer lock.
        /// </summary>
        /// <param name="executor"></param>
        protected void ExecuteWrite(ActionVoid executor)
        {
            AcquireWriterLock();
            try
            {
                executor();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ReleaseWriterLock();
            }
        }


        /// <summary>
        /// Gets the reader lock.
        /// </summary>
        protected void AcquireReaderLock()
        {
            _readwriteLock.AcquireReaderLock(_lockMilliSecondsForRead);
        }


        /// <summary>
        /// Release the reader lock.
        /// </summary>
        protected void ReleaseReaderLock()
        {
            _readwriteLock.ReleaseReaderLock();
        }


        /// <summary>
        /// Acquire the writer lock.
        /// </summary>
        protected void AcquireWriterLock()
        {
            _readwriteLock.AcquireWriterLock(_lockMilliSecondsForWrite);
        }


        /// <summary>
        /// Release the writer lock.
        /// </summary>
        protected void ReleaseWriterLock()
        {
            _readwriteLock.ReleaseWriterLock();
        }
        #endregion
    }
}
