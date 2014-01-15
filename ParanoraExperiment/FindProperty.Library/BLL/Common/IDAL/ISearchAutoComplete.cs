using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface ISearchAutoComplete
    {
        [CommonCallHandler]
        List<ViewModel.SearchAutoComplete> GetSearchAutoComplete(string keyWord, string postType, int searchType);
    }
}
