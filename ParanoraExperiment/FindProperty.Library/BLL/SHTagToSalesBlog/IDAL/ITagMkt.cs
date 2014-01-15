using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL
{
    public interface ITagMkt
    {
        [CommonCallHandler(CacheSecond = 20 * 60)]
        List<ViewModel.TagMkt> SelectTagMkt(string scpMkt,string tag, string tagCategory,int top);

        [CommonCallHandler(CacheSecond = 20 * 60)]
        List<ViewModel.TagMkt> SelectTagMktGroupByTag(string scpMkt, string tag, string tagCategory, int top);

        [CommonCallHandler(CacheSecond = 20 * 60)]
        List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top,string orderBy,string order);
    }
}