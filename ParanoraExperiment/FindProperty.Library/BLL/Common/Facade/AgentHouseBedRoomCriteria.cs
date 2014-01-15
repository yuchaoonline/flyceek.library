using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentHouseBedRoomCriteria
    {
        private readonly IAgentHouseBedRoomCriteria dal = DataAccessFactoryCreator.Create().AgentHouseBedRoomCriteria();
        public List<ViewModel.BedRoomCriteria> GetBedRoomCriteria()
        {
            return dal.GetBedRoomCriteria();
        }

        public ViewModel.BedRoomCriteria GetBedRoomCriteria(string id)
        {
            ViewModel.BedRoomCriteria item = null;
            int idIint = 0;
            if (int.TryParse(id, out idIint))
            {
                item = GetBedRoomCriteria().Where(x => x.ID == id).FirstOrDefault();
            }
            return item;
        }
    }
}
