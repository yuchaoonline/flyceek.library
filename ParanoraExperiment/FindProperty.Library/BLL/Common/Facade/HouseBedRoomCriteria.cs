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
    public class HouseBedRoomCriteria
    {
        private readonly IHouseBedRoomCriteria dal = DataAccessFactoryCreator.Create().HouseBedRoomCriteria();

        public List<ViewModel.BedRoomCriteria> GetBedRoomCriteria()
        {
            return dal.GetBedRoomCriteria();
        }

        public ViewModel.BedRoomCriteria GetBedRoomCriteria(string id)
        {
            ViewModel.BedRoomCriteria item = null;
            int idIint = 0;
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out idIint))
            {
                item = GetBedRoomCriteria().Where(x => x.ID == id).FirstOrDefault();
            }
            return item;
        }
    }
}
