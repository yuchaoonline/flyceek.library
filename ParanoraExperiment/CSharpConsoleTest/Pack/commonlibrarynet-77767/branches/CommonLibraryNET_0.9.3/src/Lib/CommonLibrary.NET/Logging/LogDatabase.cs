using System.Collections.Generic;
using System.Data;
using System.Data.Linq;

namespace ComLib.Logging
{
    public class LogDatabase : LogBase
    {
        private const int FlushInterval = 5;
        private readonly IDbConnection _connection;
        private readonly IList<LogEventEntity> _uncommitedList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Connection to a database that contains the log events table</param>
        public LogDatabase(IDbConnection connection, LogLevel logLevel)
        {
            var settings = new LogSettings();
            settings.Level = logLevel;
            Settings = settings;
            _connection = connection;
            _uncommitedList = new List<LogEventEntity>();
        }


        /// <summary>
        /// Adds a log event to a internal list then runs a flush check
        /// </summary>
        /// <param name="logEvent"></param>
        public override void Log(LogEvent logEvent)
        {
            LogEventEntity eventEntity = new LogEventEntityMapper().MapFrom(logEvent);
            _uncommitedList.Add(eventEntity);
            FlushCheck();
        }


        /// <summary>
        /// Persists a batch of log events to a database table
        /// </summary>
        public override void Flush()
        {
            var db = new DataContext(_connection);
            using (db)
            {
                lock (_uncommitedList)
                {
                    ExecuteHelper.TryCatch(() =>
                    {
                        // Get the LogEventEntity table
                        Table<LogEventEntity> logEvents = db.GetTable<LogEventEntity>();

                        // Ask the entity table to insert the uncommitted list upon submit.
                        logEvents.InsertAllOnSubmit(_uncommitedList);

                        // Submit the changes to the database.
                        db.SubmitChanges(ConflictMode.ContinueOnConflict);

                        // Clear the internal uncomitted log event list
                        _uncommitedList.Clear();
                    });
                }
            }
        }


        /// <summary>
        /// Checks if the internal list has reached the flush interval
        /// </summary>
        private void FlushCheck()
        {
            // If the uncommitted list has reached the flush interval then flush the log entries.
            if (_uncommitedList.Count.Equals(FlushInterval))
            {
                Flush();
            }
        }
    }
}