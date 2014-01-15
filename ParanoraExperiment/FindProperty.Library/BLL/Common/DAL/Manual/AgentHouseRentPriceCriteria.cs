using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentHouseRentPriceCriteria:IDAL.IAgentHouseRentPriceCriteria
    {
        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            List<ViewModel.PriceCriteria> result = new List<ViewModel.PriceCriteria>();
            result.Add(new ViewModel.PriceCriteria() { ID = "1", Min = 0, Max = 1000, DisplayString = "小于1000", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "2", Min = 1000, Max = 3000, DisplayString = "1000-3000", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "3", Min = 3000, Max = 5000, DisplayString = "3000-5000", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "4", Min = 5000, Max = 8000, DisplayString = "5000-8000", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "5", Min = 8000, Max = 0, DisplayString = "大于8000", Type = 1 });
            return result;
        }
    }
}
