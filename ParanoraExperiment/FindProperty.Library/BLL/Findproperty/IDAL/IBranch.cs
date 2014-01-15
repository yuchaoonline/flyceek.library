using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IBranch
    {
        [CommonCallHandler(CacheSecond = 86400)]
        List<ViewModel.Branch> SelectBranch(string scpid, double lat, double lng,int pageSize, int pageIndex, int sort);
    }
}
