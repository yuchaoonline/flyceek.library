using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.CentalineEst.DALFactory
{
    public class DbDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IEstHeader EstHeader()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.EstHeader, IDAL.IEstHeader>();
        }
    }
}
