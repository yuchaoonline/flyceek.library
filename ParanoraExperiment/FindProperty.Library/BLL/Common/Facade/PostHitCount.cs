using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class PostHitCount
    {
        private readonly IPostHitCount dal = DataAccessFactoryCreator.Create().PostHitCount();

        public void Add(Model.PostHitCount model)
        {
            dal.Add(model);
        }
    }
}
