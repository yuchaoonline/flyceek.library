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
    public class HouseSaleOrderCriteria : IDAL.IHouseSaleOrderCriteria
    {
        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            List<ViewModel.OrderCriteria> result = new List<ViewModel.OrderCriteria>();

            result.Add(new ViewModel.OrderCriteria() { ID = "1", Name = "时间", DisplayString = "按时间由高到低", Value = "", OrderBy = "UpdateDate", Order = "desc", Where = "UpdateDate asc", GroupName = "时间", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "2", Name = "时间", DisplayString = "按时间由低到高", Value = "", OrderBy = "UpdateDate", Order = "asc", Where = "UpdateDate desc", GroupName = "时间", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "3", Name = "总价", DisplayString = "按总价由高到低", Value = "", OrderBy = "Price", Order = "desc", Where = "Price asc", GroupName = "总价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "4", Name = "总价", DisplayString = "按总价由低到高", Value = "", OrderBy = "Price", Order = "asc", Where = "Price desc", GroupName = "总价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "5", Name = "单价", DisplayString = "按单价由高到低", Value = "", OrderBy = "unit_price", Order = "desc", Where = "unit_price asc", GroupName = "单价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "6", Name = "单价", DisplayString = "按单价由低到高", Value = "", OrderBy = "unit_price", Order = "asc", Where = "unit_price desc", GroupName = "单价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "7", Name = "面积", DisplayString = "按面积由高到低", Value = "", OrderBy = "Size", Order = "desc", Where = "Size asc", GroupName = "面积", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "8", Name = "面积", DisplayString = "按面积由低到高", Value = "", OrderBy = "Size", Order = "asc", Where = "Size desc", GroupName = "面积", Type = 2 });
            return result;
        }
    }
}
