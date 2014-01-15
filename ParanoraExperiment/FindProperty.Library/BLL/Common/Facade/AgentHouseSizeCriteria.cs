using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentHouseSizeCriteria
    {
        private readonly IAgentHouseSizeCriteria dal = DataAccessFactoryCreator.Create().AgentHouseSizeCriteria();
        public List<ViewModel.SizeCriteria> GetSizeCriteria()
        {
            return dal.GetSizeCriteria();
        }
    }
}
