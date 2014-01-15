using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    
    public interface IScp
    {
        [CommonCallHandler]
        List<Model.Scp> GetAllScp();

        List<Model.Scp> GetScpByScpMkt(string scpMkt);
    }
}
