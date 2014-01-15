using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HouseRentOrderCriteria:IDAL.IHouseRentOrderCriteria
    {
        public List<ViewModel.OrderCriteria> GetOrderCriteria()
        {
            List<ViewModel.OrderCriteria> result = new List<ViewModel.OrderCriteria>();

            result.Add(new ViewModel.OrderCriteria() { ID = "1", Name = "关注度", DisplayString = "按关注度由高到低", Value = "", OrderBy = "score", Order = "desc", Where = "score asc", GroupName = "关注度", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "2", Name = "关注度", DisplayString = "按关注度由低到高", Value = "", OrderBy = "score", Order = "asc", Where = "score desc", GroupName = "关注度", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "3", Name = "租价", DisplayString = "按租价由高到低", Value = "", OrderBy = "Rental", Order = "desc", Where = "Rental asc", GroupName = "租价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "4", Name = "租价", DisplayString = "按租价由低到高", Value = "", OrderBy = "Rental", Order = "asc", Where = "Rental desc", GroupName = "租价", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "5", Name = "面积", DisplayString = "按面积由高到低", Value = "", OrderBy = "Size", Order = "desc", Where = "Size asc", GroupName = "面积", Type = 2 });
            result.Add(new ViewModel.OrderCriteria() { ID = "6", Name = "面积", DisplayString = "按面积由低到高", Value = "", OrderBy = "Size", Order = "asc", Where = "Size desc", GroupName = "面积", Type = 2 });
            
            return result;
        }

    }
}
