using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.Findproperty.DALFactory
{
    public class DataAccessFactoryCreator
    {
        public static IDataAccessFactory Create()
        {
            IDataAccessFactory dalfactory = new DbDataAccessFactory();
            try
            {
                dalfactory = IoCManager.Resolve<IDataAccessFactory>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dalfactory;
        }
    }
}
