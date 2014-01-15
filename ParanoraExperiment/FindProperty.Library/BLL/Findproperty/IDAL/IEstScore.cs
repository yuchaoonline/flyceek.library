using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IEstScore
    {
        [CommonCallHandler(CacheSecond = 86400)]
        List<ViewModel.EstScore> SelectEstScore(int raidus, double lat, double lng,int pageindex,string posttype,int sort);

        [CommonCallHandler(CacheSecond = 1200)]
        ViewModel.EstScore GetPosition(string EstCode, string BigEstCode);
    }
}
