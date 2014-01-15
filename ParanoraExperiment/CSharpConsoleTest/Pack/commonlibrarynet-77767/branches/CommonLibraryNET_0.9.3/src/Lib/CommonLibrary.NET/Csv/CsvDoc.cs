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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using ComLib;
using ComLib.Parsing;
using ComLib.Types;

namespace ComLib.CsvParse
{
    /// <summary>
    /// Csv document.
    /// Lexical parser that can parse.
    /// 
    /// Id, Name,      Desc
    /// 0,  Art,       Art classes
    /// 1,  Painting,  Any type of painting
    /// 2,  Sports,    Sports classes
    /// 3.  Boxing,	   Boxing classes
    /// </summary>
    public class CsvDoc
    {
        #region Private members
        private static CsvConfig _defaultSettings = new CsvConfig();
        private string _content = "";
        private string _filePath = "";
        private bool _isFileBased = false;
        private LexList _parser;
        private ReadOnlyCollection<string> _columnNames;
        private List<OrderedDictionary> _records = null;
        private CsvConfig _settings = null;
        #endregion


        #region Constructors
        /// <summary>
        /// Loads a new csv document.
        /// </summary>
        /// <param name="contentOrFilePath"></param>
        /// <param name="isFilePath"></param>
        /// <param name="isCaseSensitive"></param>
        public CsvDoc(string contentOrFilePath, bool isFilePath) 
            :this(contentOrFilePath, isFilePath, _defaultSettings, true)
        {
        }


        /// <summary>
        /// Create using supplied settings.
        /// </summary>
        /// <param name="contentOfFilePath"></param>
        /// <param name="isFilePath"></param>
        /// <param name="settings"></param>
        public CsvDoc(string contentOrFilePath, bool isFilePath, CsvConfig settings, bool autoLoad)
        {
            // Store settings
            _isFileBased = isFilePath;
            _content = isFilePath ? File.ReadAllText(contentOrFilePath) : contentOrFilePath;
            _filePath = isFilePath ? contentOrFilePath : "";
            _settings = settings;

            LexListSettings lexListSettings = new LexListSettings() { MultipleRecordsUsingNewLine = true };
            _parser = new LexList(lexListSettings);
            if (autoLoad)
            {
                ParseDict();
            }
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// Column names.
        /// </summary>
        public ReadOnlyCollection<string> Columns
        {
            get { return _columnNames; }
        }


        /// <summary>
        /// Records as a list of dictionaries.
        /// </summary>
        public List<OrderedDictionary> Data
        {
            get { return _records; }
        }


        /// <summary>
        /// Get the value at the row/column index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public T Get<T>(int row, int col)
        {
            string result = (string)_records[row][col];
            T typedResult = (T)TypeParsers.Convert<T>(result);
            return typedResult;
        }


        /// <summary>
        /// Get the value at the row/column name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public T Get<T>(int row, string colName)
        {
            string result = (string)_records[row][colName];
            T typedResult = (T)TypeParsers.Convert<T>(result);
            return typedResult;
        } 
        #endregion


        /// <summary>
        /// Parse lists.
        /// </summary>
        /// <returns></returns>
        private void ParseLists()
        {
            List<List<string>> records = Parse();
            if (_settings.ContainsHeaders)
            {
                _columnNames = new ReadOnlyCollection<string>(records[0]);
                records.RemoveAt(0);
            }
            List<OrderedDictionary> tableData = new List<OrderedDictionary>();

            // Load each record into a dictionary.
            foreach (List<string> record in records)
            {
                OrderedDictionary recordMap = new OrderedDictionary();

                for (int ndx = 0; ndx < _columnNames.Count; ndx++)
                {
                    string val = record[ndx];
                    recordMap[ndx.ToString()] = val;
                }
                tableData.Add(recordMap);
            }
            _records = tableData;
        }
        

        /// <summary>
        /// Parse as list of records where each record is a dictionary.
        /// This should be used if the first column has headers.
        /// </summary>
        /// <returns></returns>
        private void ParseDict()
        {
            List<List<string>> records = Parse();

            // Columns
            List<string> columnNames = records[0];
            List<OrderedDictionary> tableData = new List<OrderedDictionary>();

            for(int ndx = 1; ndx < records.Count; ndx++)
            {
                List<string> record = records[ndx];
                OrderedDictionary recordMap = new OrderedDictionary();

                // Assert that each record has same number of columns
                // TO_DO: Need better way to handle this ??
                if (record.Count != columnNames.Count)
                    throw new ArgumentException("Record at line : " + (ndx + 1) + " does not have same number of columns as in header.");

                // Map the column names to record values.
                for(int colIndex = 0; colIndex < columnNames.Count; colIndex++)
                {
                    string colName = columnNames[colIndex];
                    recordMap[colName] = record[colIndex];
                }

                // Now store record.
                tableData.Add(recordMap);
            }
            _records = tableData;
            _columnNames = new ReadOnlyCollection<string>(columnNames);
        }


        /// <summary>
        /// Returns all the records in the csv content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public List<List<string>> Parse()
        {
            List<List<string>> data = _parser.ParseLines(_content);
            return data;
        }



        /// <summary>
        /// Write csv doc to the file in csv format.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="delimeter"></param>
        public void Write(string fileName, string delimeter)
        {
            Write(fileName, delimeter, false, "\"", Environment.NewLine, false);
        }


        /// <summary>
        /// Write csv doc to the filename.
        /// </summary>
        /// <param name="fileName">FileName</param>
        /// <param name="delimeter">Delimeter to use</param>
        /// <param name="quoteAll">Quote all fields</param>
        /// <param name="quoteChar">Character to use for strings when quoting.</param>
        /// <param name="newLine">New Line character to use.</param>
        /// <param name="append"></param>
        public void Write(string fileName, string delimeter, bool quoteAll, string quoteChar, string newLine, bool append)
        {
            List<string> columns = new List<string>(this.Columns);
            List<List<string>> data = new List<List<string>>();
            foreach (OrderedDictionary rec in _records)
            {
                var recToAdd = new List<string>();
                for (int ndx = 0; ndx < rec.Count; ndx++)
                    recToAdd.Add(rec[ndx].ToString());
                data.Add(recToAdd);
            }
            Csv.Write(fileName, data, delimeter, columns, false, quoteAll, quoteChar, Environment.NewLine, append);
        }
    }
}
