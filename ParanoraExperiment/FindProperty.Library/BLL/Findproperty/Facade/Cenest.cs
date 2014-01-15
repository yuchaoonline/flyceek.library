using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Cenest
    {
        private IDAL.ICenest dal = DALFactory.DataAccessFactoryCreator.Create().Cenest();

        public ViewModel.Cenest GetCenest(string cestCode)
        {
            return dal.GetCenest(cestCode);
        }
    }
}
