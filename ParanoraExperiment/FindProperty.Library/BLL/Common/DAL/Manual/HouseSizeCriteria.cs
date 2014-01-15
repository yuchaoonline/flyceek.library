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
    public class HouseSizeCriteria : IHouseSizeCriteria
    {
        public List<ViewModel.SizeCriteria> GetSizeCriteria()
        {
            List<ViewModel.SizeCriteria> result = new List<ViewModel.SizeCriteria>();

            result.Add(new ViewModel.SizeCriteria() { ID = "1", Min = 0, Max = 50, DisplayString = "50㎡以下", Type = 1 });
            result.Add(new ViewModel.SizeCriteria() { ID = "2", Min = 50, Max = 150, DisplayString = "50-150㎡", Type = 1 });
            result.Add(new ViewModel.SizeCriteria() { ID = "3", Min = 150, Max = 300, DisplayString = "150-300㎡", Type = 1 });
            result.Add(new ViewModel.SizeCriteria() { ID = "4", Min = 300, Max = 500, DisplayString = "300-500㎡", Type = 1 });
            result.Add(new ViewModel.SizeCriteria() { ID = "5", Min = 500, Max = 0, DisplayString = "500㎡以上", Type = 1 });

            return result;
        }
    }
}
