using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Gscp
    {
        private IDAL.IGscp dal = DALFactory.DataAccessFactoryCreator.Create().Gscp();

        public List<ViewModel.Gscp> GetAllGscp()
        {
            return dal.GetAllGscp();
        }

        public List<ViewModel.Gscp> GetGscpByScpMkt(string scpMkt)
        {
            return dal.GetGscpByScpMkt(scpMkt);
        }
    }
}
