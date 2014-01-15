using FindProperty.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class HitCountRequestCheck:IDAL.IHitCountRequestCheck
    {
        public bool IsAllowed(ViewModel.HitCountRequestContext requestContext)
        {
            bool result = true;

            if (ConfigInfo.BlockRecordHitCountRequestIpList.Where(x => x == requestContext.Ip).Count() > 0)
            {
                result = false;
            }

            return result;
        }
    }
}
