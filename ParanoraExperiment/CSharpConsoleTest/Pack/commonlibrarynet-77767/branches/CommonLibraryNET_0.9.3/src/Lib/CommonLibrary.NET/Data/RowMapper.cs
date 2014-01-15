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
using System.Data;
using ComLib;



namespace ComLib.Database
{
    /// <summary>
    /// Abstract class for mapping a DataTable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RowMapperTableBased<T> : RowMapperBase<DataTable, T, int>, IRowMapper<DataTable, T>
    {
        /// <summary>
        /// Maps all the rows using DataTable.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public IList<T> MapRows(DataTable table)
        {
            IList<T> records = new List<T>();
            for (int ndx = 0; ndx < table.Rows.Count; ndx++ )
            {
                T record = MapRow(table, ndx);
                records.Add(record);
            }
            return records;
        }
    }



    /// <summary>
    /// Abstract class for mapping a row from a DataReader.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RowMapperReaderBased<T> : RowMapperBase<IDataReader, T, int>, IRowMapper<IDataReader, T>
    {
        /// <summary>
        /// Map all the rows to IList of objects T using DataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public IList<T> MapRows(IDataReader reader)
        {
            IList<T> records = new List<T>();
            int ndx = 0;
            while(reader.Read())
            {
                T record = MapRow(reader, ndx);
                records.Add(record);
                ndx++;
            }
            return records;
        }
    }



    /// <summary>
    /// Context used when mapping a row.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRowId"></typeparam>
    public class RowMappingContext<TSource, TResult, TRowId>
    {
        /// <summary>
        /// Just used for contextual information at the moment.
        /// </summary>
        public bool IsRowIdStringBased;


        /// <summary>
        /// The Datasource. e.g. Xmldocument, Inidocument, Csvdocument.
        /// </summary>
        public TSource Source;


        /// <summary>
        /// The Row id, either a sting, int or some other object.
        /// </summary>
        public TRowId RowId;


        /// <summary>
        /// Collect the errors during the mapping.
        /// </summary>
        public IValidationResults ValidationResults;
    }



    /// <summary>
    /// Abstract class for row mapping.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class RowMapperBase<TSource, TResult, TRowId>
    {
        /// <summary>
        /// Map the row number.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public abstract TResult MapRow(TSource source, TRowId rowNumber);
    }



    /// <summary>
    /// Row Mapper with contextual information.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TRowId"></typeparam>
    public abstract class RowMapperContextual<TSource, TResult, TRowId> : RowMapperBase<TSource, TResult, TRowId>
    {
        /// <summary>
        /// Map rows with more extensive error capturing.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public abstract BoolMessageItem<IList<TResult>> MapRows(RowMappingContext<TSource, TResult, TRowId> ctx);


        /// <summary>
        /// Map row with more extensive error capturing.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowId"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public abstract BoolMessageItem<TResult> MapRow(RowMappingContext<TSource, TResult, TRowId> ctx);
    }
}
