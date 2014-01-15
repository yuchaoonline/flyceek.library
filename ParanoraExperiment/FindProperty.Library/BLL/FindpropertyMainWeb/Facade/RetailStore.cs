using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.FindpropertyMainWeb.Facade
{
    public class RetailStore
    {
        private IDAL.IRetailStore dal = DALFactory.DataAccessFactoryCreator.Create().RetailStore();

        public List<ViewModel.RetailStore> GetTopRetailStoreByArea(int top, string scpMkt, string gscp)
        {
            return dal.GetTopRetailStoreByArea(top, scpMkt, gscp);
        }
    }
}
