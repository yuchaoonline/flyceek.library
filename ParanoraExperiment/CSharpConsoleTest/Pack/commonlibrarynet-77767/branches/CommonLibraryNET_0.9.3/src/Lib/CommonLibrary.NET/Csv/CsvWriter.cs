using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;


namespace ComLib.CsvParse
{
    public class CsvWriter : IDisposable
    {
        #region Private data
        private string _filename = string.Empty;
        private List<List<string>> _data;
        private List<string> _columns;
        private bool _hasColumns;
        private bool _hasData;
        private CsvWriterConfig _config;
        private StreamWriter _writer;
        #endregion


        /// <param name="fileName">The file name to write the csv data to.</param>
        /// <param name="data">The csv data.</param>
        /// <param name="delimeter">The delimeter to use.</param>
        /// <param name="columns">The header columns.</param>
        /// <param name="quoteChar">Quote character to use.</param>
        /// <param name="append">Append to file</param>
        /// <param name="newLine">New line to use.</param>
        /// <param name="quoteAll">Whether or not to quote all the values.</param>        
        public CsvWriter(string fileName, List<List<string>> data, string delimeter, List<string> columns, bool firstRowInDataAreColumns, bool quoteAll, string quoteChar, string newLine, bool append)
        {
            Init(fileName, data, delimeter, columns, firstRowInDataAreColumns, quoteAll, quoteChar, newLine, append);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">The file name to write the csv data to.</param>
        /// <param name="data">The csv data.</param>
        /// <param name="delimeter">The delimeter to use.</param>
        /// <param name="columns">The header columns.</param>
        /// <param name="quoteAll">Whether or not to quote all the values.</param>  
        /// <param name="append">Whether or not to append to csv file.</param>
        /// <param name="quoteChar">The quote char to use to enclose data.</param>
        /// <param name="newLine">New line to use.</param>
        public void Init(string fileName, List<List<string>> data, string delimeter, List<string> columns, bool firstRowInDataAreColumns, bool quoteAll, string quoteChar, string newLine, bool append)
        {
            _filename = fileName;
            _columns = columns;
            _data = data;
            _hasData = _data != null && _data.Count > 0;

            // Columns in first row of data.
            if (firstRowInDataAreColumns && _hasData)
            {
                _columns = data[0];
                data.RemoveAt(0);
            }

            _hasColumns = _columns != null && _columns.Count > 0;
            _config = new CsvWriterConfig(delimeter, quoteAll, newLine, quoteChar, append);
            _writer = new StreamWriter(_filename, _config.Append);
        }
                

        /// <summary>
        /// Write out the data supplied at Initialization.
        /// </summary>
        public void Write()
        {
            // Validate
            if (!_hasData) throw new InvalidOperationException("No csv data to write.");
           
            // Write out columns and data.
            WriteColumns(_columns);            
            foreach (var record in _data) WriteRow(record);
        }


        /// <summary>
        /// Write out a row of data.
        /// </summary>
        /// <param name="data"></param>
        public void WriteRow(List<string> record)
        {            
            // First rec column val.
            if (_config.QuoteAll) _writer.Write(_config.QuoteChar + record[0] + _config.QuoteChar);
            else _writer.Write(record[0]);

            // Write the rest using the delimeter.
            for (int ndx = 0; ndx < record.Count; ndx++)
            {
                string val = _config.QuoteAll ? _config.QuoteChar + record[ndx] + _config.QuoteChar : record[ndx];
                _writer.Write(_config.Delimeter + val);
            }
            _writer.Write(_config.NewLineChar);
        }


        /// <summary>
        /// Write out the columns.
        /// </summary>
        /// <param name="columns"></param>
        public void WriteColumns(List<string> columns)
        {
            _hasColumns = true;
            _columns = columns;
            _writer.Write(_columns[0]);
            for (int ndx = 1; ndx < _columns.Count; ndx++)
                _writer.Write(_config.Delimeter + _columns[ndx]);

            _writer.Write(_config.NewLineChar);
        }



        /// <summary>
        /// Settings for writing out the csv data.
        /// </summary>
        class CsvWriterConfig
        {
            public string Delimeter;
            public bool QuoteAll;
            public string NewLineChar;
            public string QuoteChar;
            public bool Append;
           

            public CsvWriterConfig(string delimeter, bool quoteAll, string newLineChar, string quoteChar, bool append)
            {
                Delimeter = delimeter;
                QuoteAll = quoteAll;
                NewLineChar = newLineChar;
                Append = append;
                QuoteChar = quoteChar;
            }
        }

        #region IDisposable Members
        //Implement IDisposable.
        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this); 
        }


        /// <summary>
        /// Dispose object.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) 
        {
            if (disposing) 
            {
                if (_writer != null)
                {
                    _writer.Flush();
                    _writer.Close();
                }
            }            
        }


        /// <summary>
        /// Destructor.
        /// </summary>
        ~CsvWriter()
        {
           // Simply call Dispose(false).
           Dispose (false);
        }
        #endregion
    }
}
