using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.Facade
{
    public class TagMkt
    {
        private IDAL.ITagMkt dal = DALFactory.DataAccessFactoryCreator.Create().TagMkt();

        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top)
        {
            return dal.SelectTagMkt(scpMkt, tag, tagCategory, top);
        }

        public List<ViewModel.TagMkt> SelectTagMktGroupByTag(string scpMkt, string tag, string tagCategory, int top)
        {
            return dal.SelectTagMktGroupByTag(scpMkt,tag,tagCategory,top);
        }

        public List<ViewModel.TagMkt> SelectTagMkt(string scpMkt, string tag, string tagCategory, int top, string orderBy, string order)
        {
            return dal.SelectTagMkt(scpMkt, tag, tagCategory, top, orderBy, order);
        }
    }
}
