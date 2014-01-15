using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.CentalineEst.DALFactory
{
    public class IoCDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IEstHeader EstHeader()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.EstHeader, IDAL.IEstHeader>();
        }
    }
}
