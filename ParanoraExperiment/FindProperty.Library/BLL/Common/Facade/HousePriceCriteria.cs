using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HousePriceCriteria
    {
        public List<ViewModel.PriceCriteria> GetPriceCriteria(string postType)
        {
            List<ViewModel.PriceCriteria> list = new List<ViewModel.PriceCriteria>();

            if (!string.IsNullOrEmpty(postType))
            {
                switch (postType.Trim().ToUpper())
                {
                    case "S":
                        list = new HouseSalePriceCriteria().GetPriceCriteria();
                        break;
                    case "R":
                        list = new HouseRentPriceCriteria().GetPriceCriteria();
                        break;
                    default:
                        break;
                }
            }

            return list;
        }

        public ViewModel.PriceCriteria GetPriceCriteria(string postType,string id)
        {
            ViewModel.PriceCriteria item = null;
            int idInt = 0;
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out idInt))
            {
                item = GetPriceCriteria(postType).Where(x => x.ID == id).FirstOrDefault();
            }
            return item;
        }

        public List<ViewModel.PriceCriteria> GetAgentShopHousePriceCriteria(string postType)
        {
            List<ViewModel.PriceCriteria> list = new List<ViewModel.PriceCriteria>();

            if (!string.IsNullOrEmpty(postType))
            {
                switch (postType.Trim().ToUpper())
                {
                    case "S":
                        list = new AgentHouseSalePriceCriteria().GetPriceCriteria();
                        break;
                    case "R":
                        list = new AgentHouseRentPriceCriteria().GetPriceCriteria();
                        break;
                    default:
                        break;
                }
            }

            return list;
        }

        public ViewModel.PriceCriteria GetAgentShopHousePriceCriteria(string postType, string id)
        {
            ViewModel.PriceCriteria item = null;
            int idInt = 0;
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out idInt))
            {
                item = GetAgentShopHousePriceCriteria(postType).Where(x => x.ID == id).FirstOrDefault();
            }
            return item;
        }
    }
}
