using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.CentalineEst.Facade
{
    public class EstHeader
    {
        private IDAL.IEstHeader dal = DALFactory.DataAccessFactoryCreator.Create().EstHeader();

        public List<ViewModel.EstHeader> SelectEstHeader(string scpmkt, int top)
        {
            return dal.SelectEstHeader(scpmkt, top);
        }
    }
}
