using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.DataContext.Db
{
    public class FindpropertyMainWebContext
    {
        public SqlDatabase Context(params object[] param)
        {
            return DatabaseFactory.CreateDatabase("FindproertyMainWeb") as SqlDatabase;
        }
    }
}
