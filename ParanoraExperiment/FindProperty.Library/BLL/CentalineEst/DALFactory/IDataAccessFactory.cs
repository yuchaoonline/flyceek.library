using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.CentalineEst.DALFactory
{
    public interface IDataAccessFactory
    {
        IDAL.IEstHeader EstHeader();
    }
}
