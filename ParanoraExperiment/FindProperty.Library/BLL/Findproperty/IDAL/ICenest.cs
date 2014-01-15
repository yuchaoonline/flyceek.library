using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface ICenest
    {
        [CommonCallHandler(CacheSecond = 86400)]
        List<ViewModel.Cenest> SelectCenest(string cestCode);

        [CommonCallHandler(CacheSecond = 86400)]
        ViewModel.Cenest GetCenest(string cestCode);
    }
}
