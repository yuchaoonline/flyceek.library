using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HouseListStyle:IDAL.IHouseListStyle
    {
        public List<ViewModel.HouseListStyle> GetHouseListStyle()
        {
            List<ViewModel.HouseListStyle> list = new List<ViewModel.HouseListStyle>();

            list.Add(new ViewModel.HouseListStyle() { ID = "1", Value = "Horizontal",WParam="4" });
            list.Add(new ViewModel.HouseListStyle() { ID = "2", Value = "Portrait" ,WParam="7"});

            return list;
        }
    }
}
