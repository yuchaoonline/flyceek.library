using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Cms.IDAL
{
    public interface IIcmArticle
    {
        [CommonCallHandler]
        List<ViewModel.IcmArticle> SelectIcmArticleByPropValue(string propValue, int top, string orderBy, string order);
    }
}
