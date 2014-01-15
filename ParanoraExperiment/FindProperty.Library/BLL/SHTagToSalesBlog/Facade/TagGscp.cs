using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.Facade
{
    public class TagGscp
    {
        private IDAL.ITagGscp dal = DALFactory.DataAccessFactoryCreator.Create().TagGscp();

        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory,int top)
        {
            return dal.SelectTagGscp(scpMkt,gscpId,tag,tagCategory,top);
        }

        public List<ViewModel.TagGscp> SelectTagGscp(string scpMkt, string gscpId, string tag, string tagCategory, int top, string orderBy, string order)
        {
            return dal.SelectTagGscp(scpMkt, gscpId, tag, tagCategory, top, orderBy, order);
        }

        public List<ViewModel.TagGscp> SelectTagGscpGroupByTag(string scpMkt, string gscpId, string tag, string tagCategory, int top)
        {
            return SelectTagGscpGroupByTag(scpMkt, gscpId, tag, tagCategory, top);
        }
    }
}
