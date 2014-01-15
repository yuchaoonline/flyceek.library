using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class MatchHouseSearchKeyword
    {
        private readonly IMatchHouseSearchKeyword dal = DataAccessFactoryCreator.Create().MatchHouseSearchKeyword();

        public ViewModel.MatchHouseSearchKeyword Match(string keyWord, string ip, string session)
        {
            ViewModel.MatchHouseSearchKeyword mach = null;
            if (!string.IsNullOrEmpty(keyWord))
            {
                mach=dal.Match(keyWord, ip, session);
            }
            return mach;
        }

        public ViewModel.HouseSearchCriteria Match(ViewModel.HouseSearchCriteria searchCriteria)
        {
            ViewModel.MatchHouseSearchKeyword matchSearchKeyword = dal.Match(searchCriteria.KeyWord, searchCriteria.Ip, searchCriteria.Session);

            searchCriteria.TagCommonCodes = matchSearchKeyword.TagCommonCodes;
            searchCriteria.RefNo = matchSearchKeyword.RefNo;
            searchCriteria.AgentCName = matchSearchKeyword.AgentCName;
            searchCriteria.AgentMobile = matchSearchKeyword.AgentMobile;
            searchCriteria.Code = matchSearchKeyword.Code;
            searchCriteria.Type = matchSearchKeyword.Type;
            searchCriteria.ScopeMkt = matchSearchKeyword.ScopeMkt;
            if (!string.IsNullOrEmpty(matchSearchKeyword.KeyWord))
            {
                searchCriteria.KeyWord = matchSearchKeyword.KeyWord;
            }

            return searchCriteria;
        }
    }
}
