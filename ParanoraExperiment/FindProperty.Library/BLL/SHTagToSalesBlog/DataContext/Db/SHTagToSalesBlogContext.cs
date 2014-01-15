using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DataContext.Db
{
    public class SHTagToSalesBlogContext
    {
        public SqlDatabase Context(params object[] param)
        {
            return DatabaseFactory.CreateDatabase("SHTagToSalesBlog") as SqlDatabase;
        }
    }
}