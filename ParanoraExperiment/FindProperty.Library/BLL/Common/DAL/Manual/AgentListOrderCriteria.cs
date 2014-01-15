using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class AgentListOrderCriteria:IDAL.IAgentListOrderCriteria
    {
        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            List<ViewModel.OrderCriteria> result = new List<ViewModel.OrderCriteria>();

            result.Add(new ViewModel.OrderCriteria() { ID = "1", Name = "星级", DisplayString = "按星级度由高到低", Value = "", OrderBy = "agentscore", Order = "desc", Where = "agentscore asc", GroupName = "星级", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "2", Name = "星级", DisplayString = "按星级度由低到高", Value = "", OrderBy = "agentscore", Order = "asc", Where = "agentscore desc", GroupName = "星级", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "3", Name = "点击量", DisplayString = "按点击量由高到低", Value = "", OrderBy = "agentscore1", Order = "desc", Where = "agentscore1 asc", GroupName = "点击量", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "4", Name = "点击量", DisplayString = "按点击量由低到高", Value = "", OrderBy = "agentscore1", Order = "asc", Where = "agentscore1 desc", GroupName = "点击量", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "5", Name = "成交量", DisplayString = "按成交量由高到低", Value = "", OrderBy = "agentscore2", Order = "desc", Where = "agentscore2 asc", GroupName = "成交量", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "6", Name = "成交量", DisplayString = "按成交量由低到高", Value = "", OrderBy = "agentscore2", Order = "asc", Where = "agentscore2 desc", GroupName = "成交量", Type = 2 });

            return result;
        }
    }
}
