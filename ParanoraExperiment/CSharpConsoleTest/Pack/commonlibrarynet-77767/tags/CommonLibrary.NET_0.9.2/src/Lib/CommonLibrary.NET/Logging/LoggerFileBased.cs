using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace CommonLibrary
{
    /// <summary>
    /// File based logger.
    /// </summary>
    public class LoggerFileBased : LoggerBase, ILog, IDisposable
    {
        private string _filepath;
        private StreamWriter _writer;
        private int _iterativeFlushCount = 0;
        private int _iterativeFlushPeriod = 5;
        private object _lockerFlush = new object();
        private int _maxFileSizeInMegs;
        private bool _rollFile = true;
        

        /// <summary>
        /// Initialize with path of the log file.
        /// </summary>
        /// <param name="filepath"></param>
        public LoggerFileBased(string name, string filepath) :base(name)
        {
            _filepath = string.IsNullOrEmpty(filepath) ? @"log.txt" : filepath;
            _writer = new StreamWriter(_filepath, true);
        }


        /// <summary>
        /// The full path to the log file.
        /// </summary>
        public string FilePath
        {
            get { return _filepath; }
        }


        /// <summary>
        /// Log the event to file.
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            string finalMessage = logEvent.FinalMessage;
            if(string.IsNullOrEmpty(finalMessage))
                finalMessage = BuildMessage(logEvent);

            _writer.WriteLine(finalMessage);
            FlushCheck();
        }        


        /// <summary>
        /// Flush the output.
        /// </summary>
        public override void Flush()
        {
            _writer.Flush();
        }


        /// <summary>
        /// Shutsdown the logger.
        /// </summary>
        public override void ShutDown()
        {
            Dispose();
        }


        #region IDisposable Members
        /// <summary>
        /// Flushes the data to the file.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_writer != null)
                {
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion


        /// <summary>
        /// Destructor to close the writer
        /// </summary>
        ~LoggerFileBased()
        {
            Dispose();
        }


        /// <summary>
        /// Flush the data and check file size for rolling.
        /// </summary>
        private void FlushCheck()
        {
            lock (_lockerFlush)
            {
                if (_iterativeFlushCount % _iterativeFlushPeriod == 0)
                {
                    _writer.Flush();
                    _iterativeFlushCount = 1;
                }
            }
        }
    }
}
