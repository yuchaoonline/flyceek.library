using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.DALFactory
{
    public interface IDataAccessFactory
    {
        IAgentInfoSearchCriteria AgentInfoSearchCriteria();

        IAgentListSearchCriteria AgentListSearchCriteria();

        IHouseSalePriceCriteria HouseSalePriceCriteria();

        IHouseRentPriceCriteria HouseRentPriceCriteria();

        IHouseSizeCriteria HouseSizeCriteria();

        IHouseBedRoomCriteria HouseBedRoomCriteria();

        IHouseSearchCriteria HouseSearchCriteria();

        IHouseSaleOrderCriteria HouseSaleOrderCriteria();

        IHouseRentOrderCriteria HouseRentOrderCriteria();

        ISubWayCriteria SubWayCriteria();

        ISubStationCriteria SubStationCriteria();

        ISearchAutoComplete SearchAutoComplete();

        IMatchHouseSearchKeyword MatchHouseSearchKeyword();

        ISearchRecommendTag SearchRecommendTag();

        ISearchTagCategoryCheck SearchTagCategoryCheck();

        ICallAgent400 CallAgent400();

        IBarCode BarCode();

        IHistoryPost HistoryPost();

        IHouseListStyle HouseListStyle();

        IAgentListOrderCriteria AgentListOrderCriteria();

        IAgentHouseSizeCriteria AgentHouseSizeCriteria();

        IAgentHouseSalePriceCriteria AgentHouseSalePriceCriteria();

        IAgentHouseRentPriceCriteria AgentHouseRentPriceCriteria();

        IAgentHouseBedRoomCriteria AgentHouseBedRoomCriteria();

        IAgentPostSummary AgentPostSummary();

        IEntrust Entrust();

        IPostHitCount PostHitCount();

        IAgentHitCount AgentHitCount();
    }
}
