using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace FindProperty.Lib.BLL.Findproperty.DataContext.Db
{
    public class FindPropertyContext
    {
        public SqlDatabase Context(params object[] param)
        {
            return DatabaseFactory.CreateDatabase("FindProperty") as SqlDatabase;
        }
    }
}
