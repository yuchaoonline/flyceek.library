using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Common.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HouseSalePriceCriteria : IHouseSalePriceCriteria
    {
        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            List<ViewModel.PriceCriteria> result = new List<ViewModel.PriceCriteria>();
            result.Add(new ViewModel.PriceCriteria() { ID = "1", Min = 0, Max = 500000, DisplayString = "50万以下", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "2", Min = 500000, Max = 2000000, DisplayString = "50-200万", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "3", Min = 2000000, Max = 5000000, DisplayString = "200-500万", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "4", Min = 5000000, Max = 10000000, DisplayString = "500-1000万", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "5", Min = 10000000, Max = 0, DisplayString = "大于1000万元", Type = 1 });
            return result;
        }

    }
}
