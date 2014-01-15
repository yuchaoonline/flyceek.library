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
    public class HouseRentPriceCriteria : IHouseRentPriceCriteria
    {
        public List<ViewModel.PriceCriteria> GetPriceCriteria()
        {
            List<ViewModel.PriceCriteria> result = new List<ViewModel.PriceCriteria>();
            result.Add(new ViewModel.PriceCriteria() { ID = "1", Min = 0, Max = 2000, DisplayString = "2000元以下", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "2", Min = 2000, Max = 5000, DisplayString = "2000-5000元", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "3", Min = 5000, Max = 8000, DisplayString = "5000-8000元", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "4", Min = 8000, Max = 11000, DisplayString = "8000-11000元", Type = 1 });
            result.Add(new ViewModel.PriceCriteria() { ID = "5", Min = 11000, Max = 0, DisplayString = "大于11000元", Type = 1 });
            return result;
        }

    }
}
