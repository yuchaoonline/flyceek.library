using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class AgentHitCount
    {
        private readonly IAgentHitCount dal = DataAccessFactoryCreator.Create().AgentHitCount();

        public void Add(Model.AgentHitCount model)
        {
            dal.Add(model);
        }
    }
}
