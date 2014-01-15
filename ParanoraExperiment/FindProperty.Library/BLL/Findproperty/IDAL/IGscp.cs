using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IGscp
    {
        [CommonCallHandler(CacheSecond = 2592000)]
        List<ViewModel.Gscp> GetAllGscp();

        List<ViewModel.Gscp> GetGscpByScpMkt(string scpMkt);
    }
}
