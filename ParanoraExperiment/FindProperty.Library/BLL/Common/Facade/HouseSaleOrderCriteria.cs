using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseSaleOrderCriteria
    {
        private IDAL.IHouseSaleOrderCriteria dal = DALFactory.DataAccessFactoryCreator.Create().HouseSaleOrderCriteria();

        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            return dal.GetOrderCriteria();
        }
    }
}
