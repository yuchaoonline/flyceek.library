using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Branch
    {
        private IDAL.IBranch dal = DALFactory.DataAccessFactoryCreator.Create().Branch();

        public List<ViewModel.Branch> SelectBranch(string scpid, double lat, double lng, int pageSize, int pageIndex, int sort)
        {
            return dal.SelectBranch(scpid, lat, lng, pageSize, pageIndex, sort);
        }
    }
}
