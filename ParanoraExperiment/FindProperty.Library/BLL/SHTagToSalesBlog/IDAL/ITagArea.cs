using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL
{
    public interface ITagArea
    {
        [CommonCallHandler(CacheSecond = 24 * 60 * 60)]
        List<ViewModel.TagArea> SelectTagArea(string distname);

        [CommonCallHandler(CacheSecond = 24*60 * 60)]
        List<ViewModel.TagArea> SelectTagAreaByDistname(string distname);
    }
}
