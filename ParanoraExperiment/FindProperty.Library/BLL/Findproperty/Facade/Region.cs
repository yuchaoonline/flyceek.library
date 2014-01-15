using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Region
    {
        private IDAL.IRegion dal = DALFactory.DataAccessFactoryCreator.Create().Region();

        public List<ViewModel.Region> GetAllRegion()
        {
            return dal.GetAllRegion();
        }
    }
}
