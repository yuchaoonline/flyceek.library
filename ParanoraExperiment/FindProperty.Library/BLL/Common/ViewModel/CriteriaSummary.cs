using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.ViewModel
{
    [Serializable]
    public class CriteriaSummary
    {
        public List<ViewModel.PriceCriteria> RentPrices { get; set; }

        public List<ViewModel.PriceCriteria> SalePrices { get; set; }

        public List<ViewModel.SizeCriteria> Sizes { get; set; }

        public List<ViewModel.BedRoomCriteria> BedRooms { get; set; }

        public CriteriaSummary()
        {

        }
    }
}
