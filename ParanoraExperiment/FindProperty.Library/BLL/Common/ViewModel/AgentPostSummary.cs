using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class AgentPostSummary:CriteriaSummary
    {
        public AgentPostSummary()
        {
            SalePrices = new Facade.AgentHouseSalePriceCriteria().GetPriceCriteria();

            RentPrices = new Facade.AgentHouseRentPriceCriteria().GetPriceCriteria();

            Sizes = new Facade.AgentHouseSizeCriteria().GetSizeCriteria();

            BedRooms = new Facade.AgentHouseBedRoomCriteria().GetBedRoomCriteria();
        }
    }
}
