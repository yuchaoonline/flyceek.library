using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Cms.DALFactory
{
    public class DataAccessFactoryCreator
    {
        public static IDataAccessFactory Create()
        {
            IDataAccessFactory dalfactory = new IoCDataAccessFactory();
            return dalfactory;
        }
    }
}
