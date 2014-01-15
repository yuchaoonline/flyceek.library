using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HouseSizeCriteria
    {
        private readonly IHouseSizeCriteria dal = DataAccessFactoryCreator.Create().HouseSizeCriteria();
        public List<ViewModel.SizeCriteria> GetSizeCriteria()
        {
            return dal.GetSizeCriteria();
        }

        public ViewModel.SizeCriteria  GetSizeCriteria(string id)
        {
            ViewModel.SizeCriteria item = null;
            int idInt = 0;

            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out idInt))
            {
                item = GetSizeCriteria().Where(x => x.ID == id.Trim()).FirstOrDefault();
            }
            return item;
        }
    }
}
