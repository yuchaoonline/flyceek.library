using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseSalePriceCriteria
    {
        private readonly IHouseSalePriceCriteria dal = DataAccessFactoryCreator.Create().HouseSalePriceCriteria();

        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            return dal.GetPriceCriteria();
        }
    }
}
