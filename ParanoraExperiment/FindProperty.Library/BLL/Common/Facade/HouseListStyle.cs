using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseListStyle
    {
        private readonly IHouseListStyle dal = DataAccessFactoryCreator.Create().HouseListStyle();

        public List<ViewModel.HouseListStyle> GetHouseListStyle()
        {
            return dal.GetHouseListStyle();
        }

        public ViewModel.HouseListStyle GetHouseListStyle(string id)
        {
            var styles = GetHouseListStyle();
            ViewModel.HouseListStyle result = null;
            if (!string.IsNullOrEmpty(id))
            {
                result = GetHouseListStyle().Where(x => x.ID == id).FirstOrDefault();
            }
            if (result == null)
            {
                result = styles.Take(1).FirstOrDefault();
            }
            return result;
        }
    }
}
