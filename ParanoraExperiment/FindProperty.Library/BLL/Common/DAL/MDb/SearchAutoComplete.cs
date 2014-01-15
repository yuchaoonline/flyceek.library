using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.BLL;
using FindProperty.Lib.Common.ValueConvert.StringConverter;
using ShTag = FindProperty.Lib.BLL.SHTagToSalesBlog;

namespace FindProperty.Lib.BLL.Common.DAL.MDb
{
    public class SearchAutoComplete:IDAL.ISearchAutoComplete
    {
        public List<ViewModel.SearchAutoComplete> GetSearchAutoComplete(string keyWord, string postType, int searchType)
        {
            List<ViewModel.SearchAutoComplete> result = new List<ViewModel.SearchAutoComplete>();

            List<ShTag.ViewModel.TagAgent> agents = new List<ShTag.ViewModel.TagAgent>();
            List<ShTag.ViewModel.TagArea> areas = new List<ShTag.ViewModel.TagArea>();
            List<ShTag.ViewModel.TagMain> mains = new List<ShTag.ViewModel.TagMain>();

            string searchKey = keyWord;

            bool isNumOrPy = new Regex(@"^(\d*[a-z]*[A-Z]*)+$", RegexOptions.IgnoreCase | RegexOptions.Multiline).Match(keyWord).Success;
            bool isNum=new Regex(@"^(\d)+$", RegexOptions.IgnoreCase | RegexOptions.Multiline).Match(keyWord).Success;
            if (isNumOrPy || isNum)
            {
                searchKey = @"^" + keyWord;
            }

            if (searchType == 2)
            {
                if (isNum)
                {
                    agents = new ShTag.Facade.TagAgent().SelectTagAgentLike("", "", searchKey, 30);
                }
                else
                {
                    agents = new ShTag.Facade.TagAgent().SelectTagAgentLike("", searchKey, "", 30);
                }

            }
            else
            {
                areas = new ShTag.Facade.TagArea().SelectTagArea(searchKey);

                //string tagCate = "楼盘,地铁,学校";
                //if (areas.Count > 0)
                //{
                //    tagCate = "楼盘,地铁";
                //}

                mains = new ShTag.Facade.TagMain().SelectTagMain(searchKey, "", 100);
            }
            if (areas.Count > 0)
            {
                areas.ForEach((x) =>
                {
                    result.Add(new ViewModel.SearchAutoComplete() { Tag = x.c_distname, AboutNum = x.SNum, CategoryRank = 0 });
                });
            }

            Func<string, int> GetCategoryRank = (string TagCategory) =>
            {
                int rank=9999;
                switch (TagCategory)
                {
                    case "楼盘":
                        rank= 1;
                        break;
                    case "道路":
                        rank = 2;
                        break;
                    case "房源":
                        rank = 3;
                        break;
                    case "学校":
                        rank = 5;
                        break;
                }
                return rank;
            };

            if (mains.Count > 0)
            {
                mains.ForEach((x) =>
                {
                    result.Add(new ViewModel.SearchAutoComplete()
                    {
                        Tag = x.Tag,
                        AboutNum = x.AboutNum,
                        CategoryRank = GetCategoryRank(x.TagCategory.Trim())
                    });
                });
            }
            if (agents.Count > 0)
            {
                if (isNum)
                {
                    agents.ForEach((x) =>
                    {
                        result.Add(new ViewModel.SearchAutoComplete() { Tag = x.AgentMobile, AboutNum = x.SNum});
                    });
                }
                else
                {
                    agents.ForEach((x) =>
                    {
                        result.Add(new ViewModel.SearchAutoComplete() { Tag = x.AgentCName, AboutNum = x.SNum });
                    });
                }
            }

            result = result.Take(15).OrderBy(x => x.CategoryRank).ToList();

            return result;
        }
    }
}
