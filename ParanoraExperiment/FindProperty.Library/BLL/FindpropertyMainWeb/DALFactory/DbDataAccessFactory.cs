using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.DALFactory
{
    public class DbDataAccessFactory:IDataAccessFactory
    {

        public IDAL.IRetailStore RetailStore()
        {
            IDAL.IRetailStore idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.RetailStore, IDAL.IRetailStore>();

            return idal;
        }
    }
}
