using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  FindProperty.Lib.Aop.Componet;
using FindProperty.Lib.BLL.SHTagToSalesBlog.ViewModel;
using FindProperty.Lib.Cache.Component.ResultExaminer;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL
{
    public interface ITagMain
    {
        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer))]
        List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer))]
        List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory,int top,string orderBy,string order);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer))]
        List<ViewModel.TagMain> SelectTagMain(string tag, string tagCategory, int pageSize,int pageIndex, string orderBy, string order);

        [CommonCallHandler(ResultExaminerType = typeof(CollectionResultExaminer))]
        long GetTagCategoryCount(string tagCategory);
    }
}
