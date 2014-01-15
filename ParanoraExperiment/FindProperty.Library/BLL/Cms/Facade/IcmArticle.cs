using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Cms.Facade
{
    public class IcmArticle
    {
        private IDAL.IIcmArticle dal = DALFactory.DataAccessFactoryCreator.Create().IcmArticle();

        public List<ViewModel.IcmArticle> SelectIcmArticleByPropValue(string propValue, int top, string orderBy, string order)
        {
            return dal.SelectIcmArticleByPropValue(propValue, top, orderBy, order);
        }

        public List<ViewModel.IcmArticle> SelectIcmArticleByPropValue(string propValue)
        {
            return dal.SelectIcmArticleByPropValue(propValue, 2, "publishDate", "desc");
        }
    }
}
