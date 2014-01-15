using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class EstScore
    {
        private IDAL.IEstScore dal = DALFactory.DataAccessFactoryCreator.Create().EstScore();

        public List<ViewModel.EstScore> SelectEstScore(int raidus, double lat, double lng, int pageindex, string posttype,int sort)
        {

            return dal.SelectEstScore(raidus,lat,lng,pageindex,posttype,sort);
        }


        public ViewModel.EstScore GetPosition(string EstCode, string BigEstCode)
        {
            return dal.GetPosition(EstCode, BigEstCode);
        }
    }
}
