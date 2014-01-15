using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.CentalineEst.IDAL
{
    public interface IEstHeader
    {
        [CommonCallHandler(CacheSecond = 604800)]
        List<ViewModel.EstHeader> SelectEstHeader(string scpmkt, int top);
    }
}
