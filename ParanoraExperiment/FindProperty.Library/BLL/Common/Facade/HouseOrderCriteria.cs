using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseOrderCriteria
    {
        public List<ViewModel.OrderCriteria> GetOrderCriteria(string postType)
        {
            List<ViewModel.OrderCriteria> list = new List<ViewModel.OrderCriteria>();

            if (!string.IsNullOrEmpty(postType))
            {
                switch (postType.Trim().ToUpper())
                {
                    case "S":
                        list = new HouseSaleOrderCriteria().GetOrderCriteria();
                        break;
                    case "R":
                        list = new HouseRentOrderCriteria().GetOrderCriteria();
                        break;
                    default:
                        break;
                }
            }

            return list;
        }

        public ViewModel.OrderCriteria GetOrderCriteria(string postType, string id)
        {
            ViewModel.OrderCriteria item = null;
            int idInt = 0;
            if (!string.IsNullOrEmpty(id)&&int.TryParse(id, out idInt))
            {
                item = GetOrderCriteria(postType).Where(x => x.ID == id).FirstOrDefault();
            }
            return item;
        }
    }
}
