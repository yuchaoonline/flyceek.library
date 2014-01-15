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
using System.Data;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Data.Common;
using System.Data.SqlClient;


namespace CommonLibrary
{
    /// <summary>
    /// Interface for a DatabaseHelper
    /// </summary>
    public interface IDBHelper
    {
        /// <summary>
        /// Execute non-query sql.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] dbParameters);


        /// <summary>
        /// Execute sql and return datareader.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, CommandType commandType, params DbParameter[] dbParameters);


        /// <summary>
        /// Execute sql and return single scalar value.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, CommandType commandType, params DbParameter[] dbParameters);


        /// <summary>
        /// Execute sql and return dataset.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, CommandType commandType, params DbParameter[] dbParameters);


        /// <summary>
        /// Execute sql and return datatable
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string commandText, CommandType commandType, params DbParameter[] dbParameters);


        /// <summary>
        /// Add an in parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="val"></param>
        /// <param name="ctx"></param>
        void AddInParam(string paramName, DbType dbType, object val, DbConnectivityContext ctx);


        /// <summary>
        /// Add out parameter.
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="ctx"></param>
        void AddOutParam(string paramName, DbType dbType, DbConnectivityContext ctx);


        /// <summary>
        /// Build an input parameter.
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        DbParameter BuildInParam(string paramName, DbType dbType, object val);


        /// <summary>
        /// Build an input parameter.
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        DbParameter BuildOutParam(string paramName, DbType dbType);
        

        /// <summary>
        /// Get a connection to the appropriate database.
        /// </summary>
        /// <param name="connectionInfo"></param>
        /// <returns></returns>
        DbConnection GetConnection();


        /// <summary>
        /// Create a new dbcommand using the connection.
        /// </summary>
        /// <param name="con"></param>
        /// <param name="commmandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        DbCommand GetCommand(DbConnection con, string commmandText, CommandType commandType);
    }
}
