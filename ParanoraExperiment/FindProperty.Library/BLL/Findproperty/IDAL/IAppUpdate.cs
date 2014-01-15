using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IAppUpdate
    {
        [CommonCallHandler(CacheSecond = 4800)]
        string GetApkUpdateInfo(string filepath);

        [CommonCallHandler(CacheSecond = 1200)]
        string GetApkNews(string filepath);
    }
}
