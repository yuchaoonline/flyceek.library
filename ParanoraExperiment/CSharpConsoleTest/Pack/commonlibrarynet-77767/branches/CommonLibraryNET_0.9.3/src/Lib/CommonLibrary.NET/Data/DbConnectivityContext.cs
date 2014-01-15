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
using System.Data;
using System.Data.Common;


namespace CommonLibrary
{
    /// <summary>
    /// Connectivity context to wrap up a factory, connection, and command into a single container.
    /// </summary>
    public class DbConnectivityContext
    {
        public DbProviderFactory Factory;
        public DbConnection Con;
        public DbCommand Cmd;


        /// <summary>
        /// Initializes the context members.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        public DbConnectivityContext(ConnectionInfo connInfo, string commandText, CommandType commandType)
        {
            Factory = DbProviderFactories.GetFactory(connInfo.ProviderName);

            Con = Factory.CreateConnection();
            Con.ConnectionString = connInfo.ConnectionString;
            InitCommand(commandText, commandType);
        }


        /// <summary>
        /// Initializes the context members.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        public DbConnectivityContext(ConnectionInfo connInfo)
        {
            Factory = DbProviderFactories.GetFactory(connInfo.ProviderName);

            Con = Factory.CreateConnection();
            Con.ConnectionString = connInfo.ConnectionString;
        }


        public void InitCommand(string commandText, CommandType commandType)
        {
            Cmd = Con.CreateCommand();
            Cmd.CommandType = commandType;
            Cmd.CommandText = commandText;
        }


        /// <summary>
        /// Creates a DbParameter object from the arguments and adds it to the ctx command.
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="ctx"></param>
        public void AddOutParam(string paramName, DbType dbType)
        {
            // Parameter.
            DbParameter param = this.Factory.CreateParameter();
            param.ParameterName = paramName;
            param.DbType = dbType;
            param.Direction = ParameterDirection.Output;
            this.Cmd.Parameters.Add(param);
        }


        /// <summary>
        /// Create a DbParameter object from the arguments and add as input argument to the ctx command.
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="dbType"></param>
        /// <param name="val"></param>
        /// <param name="ctx"></param>
        public void AddInParam(string paramName, DbType dbType, object val)
        {
            // Parameter.
            DbParameter param = this.Factory.CreateParameter();
            param.ParameterName = paramName;
            param.DbType = dbType;
            param.Value = val;
            this.Cmd.Parameters.Add(param);
        }
    }  
}
