using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseRentOrderCriteria
    {
        private IDAL.IHouseRentOrderCriteria dal = DALFactory.DataAccessFactoryCreator.Create().HouseRentOrderCriteria();

        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            return dal.GetOrderCriteria();
        }
    }
}
