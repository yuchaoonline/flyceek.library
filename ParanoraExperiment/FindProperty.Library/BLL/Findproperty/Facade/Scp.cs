using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Scp
    {
        private IDAL.IScp dal = DALFactory.DataAccessFactoryCreator.Create().Scp();

        public List<Model.Scp> GetAllScp()
        {
            return dal.GetAllScp();
        }

        public List<Model.Scp> GetScpByScpMkt(string scpMkt)
        {
            return dal.GetScpByScpMkt(scpMkt);
        }
    }
}
