using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL
{
    public interface ITagGscp
    {
        [CommonCallHandler(CacheSecond=20*60)]
        List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory,int top);

        [CommonCallHandler(CacheSecond = 20 * 60)]
        List<ViewModel.TagGscp> SelectTagGscpGroupByTag(string scpMkt, string gscpId, string tag, string tagCategory, int top);

        [CommonCallHandler(CacheSecond = 20 * 60)]
        List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory, int top, string orderBy, string order);
    }
}
