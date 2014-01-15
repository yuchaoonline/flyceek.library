using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class SearchAutoComplete
    {
        private readonly ISearchAutoComplete dal = DataAccessFactoryCreator.Create().SearchAutoComplete();

        public List<ViewModel.SearchAutoComplete> GetSearchAutoComplete(string keyWord, string postType, int searchType)
        {
            List<ViewModel.SearchAutoComplete> list = new List<ViewModel.SearchAutoComplete>();
            if (!string.IsNullOrEmpty(keyWord.Trim()))
            {
                list = dal.GetSearchAutoComplete(keyWord, postType, searchType);
            }
            return list;
        }

        public string GetSearchAutoCompleteJson(string input, string postType, int searchType)
        {
            List<ViewModel.SearchAutoComplete> list = GetSearchAutoComplete(input, postType, searchType);
            StringBuilder sb = new StringBuilder();
            if (list.Count > 0)
            {
                sb.Append("{\"props\" : [");
                sb.Append(string.Join(",", list.Select((x) => { return "{\"name\" : \"" + x.Tag + "\",\"count\":\"" + x.AboutNum.ToString() + "\"}"; }).ToArray()));
                sb.Append("]}");
            }
            return sb.ToString();
        }
    }
}
