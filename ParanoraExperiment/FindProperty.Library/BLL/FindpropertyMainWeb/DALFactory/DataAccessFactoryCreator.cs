using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.DALFactory
{
    public class DataAccessFactoryCreator
    {
        public static IDataAccessFactory Create()
        {
            IDataAccessFactory dalfactory = new DbDataAccessFactory();
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dalfactory;
        }
    }
}
