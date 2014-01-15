using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.BLL.SHTagToSalesBlog.DALFactory;
using FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.Facade
{
    public class TagMain
    {
        private readonly ITagMain dal = DataAccessFactoryCreator.Create().TagMain();

        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory)
        {
            return dal.SelectTagMain(tag, tagCategory);
        }

        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int top, string orderBy, string order)
        {
            return dal.SelectTagMain(tag, tagCategory,top,orderBy,order);
        }

        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int top)
        {
            return dal.SelectTagMain(tag, tagCategory, top, "AboutNum", "desc");
        }

        public List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int pageSize, int pageIndex, string orderBy, string order)
        {
            return dal.SelectTagMain(tag, tagCategory, pageSize, pageIndex, orderBy, order);
        }

        public long GetTagCategoryCount(string tagCategory)
        {
            return dal.GetTagCategoryCount(tagCategory);
        }
    }
}
