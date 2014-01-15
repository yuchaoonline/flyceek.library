using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IHitCountRequestCheck
    {
        bool IsAllowed(ViewModel.HitCountRequestContext requestContext);
    }
}
