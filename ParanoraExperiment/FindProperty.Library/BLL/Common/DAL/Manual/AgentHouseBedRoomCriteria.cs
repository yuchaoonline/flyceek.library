using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentHouseBedRoomCriteria:IDAL.IAgentHouseBedRoomCriteria
    {
        public List<ViewModel.BedRoomCriteria> GetBedRoomCriteria()
        {
            List<ViewModel.BedRoomCriteria> result = new List<ViewModel.BedRoomCriteria>();

            result.Add(new ViewModel.BedRoomCriteria() { ID = "1", Min = 1, Max = 1, DisplayString = "一", Type = 1 });
            result.Add(new ViewModel.BedRoomCriteria() { ID = "2", Min = 2, Max = 2, DisplayString = "二", Type = 1 });
            result.Add(new ViewModel.BedRoomCriteria() { ID = "3", Min = 3, Max = 3, DisplayString = "三", Type = 1 });
            result.Add(new ViewModel.BedRoomCriteria() { ID = "4", Min = 4, Max = 4, DisplayString = "四", Type = 1 });
            result.Add(new ViewModel.BedRoomCriteria() { ID = "5", Min = 5, Max = 0, DisplayString = "五", Type = 1 });

            return result;
        }
    }
}
