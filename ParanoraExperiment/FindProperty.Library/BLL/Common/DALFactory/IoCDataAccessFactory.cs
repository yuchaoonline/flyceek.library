using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common.DAL.Manual;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.Common.DALFactory
{
    public class IoCDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IAgentHitCount AgentHitCount()
        {
            IAgentHitCount idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentHitCount, IDAL.IAgentHitCount>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IPostHitCount PostHitCount()
        {
            IPostHitCount idal = PolicyInjectionFactory.Create().Create<DAL.Manual.PostHitCount, IDAL.IPostHitCount>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IEntrust Entrust()
        {
            IEntrust idal = PolicyInjectionFactory.Create().Create<DAL.Db.Entrust, IDAL.IEntrust>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentPostSummary AgentPostSummary()
        {
            IAgentPostSummary idal = PolicyInjectionFactory.Create().Create<DAL.Db.AgentPostSummary, IDAL.IAgentPostSummary>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentInfoSearchCriteria AgentInfoSearchCriteria()
        {
            IAgentInfoSearchCriteria idal = PolicyInjectionFactory.Create().Create<AgentInfoSearchCriteria, IDAL.IAgentInfoSearchCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentHouseSizeCriteria AgentHouseSizeCriteria()
        {
            IAgentHouseSizeCriteria idal = PolicyInjectionFactory.Create().Create<AgentHouseSizeCriteria, IDAL.IAgentHouseSizeCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentHouseSalePriceCriteria AgentHouseSalePriceCriteria()
        {
            IAgentHouseSalePriceCriteria idal = PolicyInjectionFactory.Create().Create<AgentHouseSalePriceCriteria, IDAL.IAgentHouseSalePriceCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IAgentHouseRentPriceCriteria AgentHouseRentPriceCriteria()
        {
            IAgentHouseRentPriceCriteria idal = PolicyInjectionFactory.Create().Create<AgentHouseRentPriceCriteria, IDAL.IAgentHouseRentPriceCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentHouseBedRoomCriteria AgentHouseBedRoomCriteria()
        {
            IAgentHouseBedRoomCriteria idal = PolicyInjectionFactory.Create().Create<AgentHouseBedRoomCriteria, IDAL.IAgentHouseBedRoomCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IAgentListSearchCriteria AgentListSearchCriteria()
        {
            IAgentListSearchCriteria idal = PolicyInjectionFactory.Create().Create<AgentListSearchCriteria, IDAL.IAgentListSearchCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IAgentListOrderCriteria AgentListOrderCriteria()
        {
            IAgentListOrderCriteria idal = PolicyInjectionFactory.Create().Create<AgentListOrderCriteria, IDAL.IAgentListOrderCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IAgentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseListStyle HouseListStyle()
        {
            IHouseListStyle idal = PolicyInjectionFactory.Create().Create<HouseListStyle, IDAL.IHouseListStyle>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseListStyle>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IHistoryPost HistoryPost()
        {
            IHistoryPost idal = PolicyInjectionFactory.Create().Create<HistoryPost, IDAL.IHistoryPost>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHistoryPost>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.IHouseSalePriceCriteria HouseSalePriceCriteria()
        {
            IHouseSalePriceCriteria idal = PolicyInjectionFactory.Create().Create<HouseSalePriceCriteria, IDAL.IHouseSalePriceCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseSalePriceCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseRentPriceCriteria HouseRentPriceCriteria()
        {
            IHouseRentPriceCriteria idal = PolicyInjectionFactory.Create().Create<HouseRentPriceCriteria, IDAL.IHouseRentPriceCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseRentPriceCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseSizeCriteria HouseSizeCriteria()
        {
            IHouseSizeCriteria idal = PolicyInjectionFactory.Create().Create<HouseSizeCriteria, IDAL.IHouseSizeCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseSizeCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseBedRoomCriteria HouseBedRoomCriteria()
        {
            IHouseBedRoomCriteria idal = PolicyInjectionFactory.Create().Create<HouseBedRoomCriteria, IDAL.IHouseBedRoomCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseBedRoomCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }


        public IHouseSearchCriteria HouseSearchCriteria()
        {
            IHouseSearchCriteria idal = PolicyInjectionFactory.Create().Create<HouseSearchCriteria, IDAL.IHouseSearchCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseSearchCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseSaleOrderCriteria HouseSaleOrderCriteria()
        {
            IHouseSaleOrderCriteria idal = PolicyInjectionFactory.Create().Create<HouseSaleOrderCriteria, IDAL.IHouseSaleOrderCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseSaleOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }

        public IDAL.IHouseRentOrderCriteria HouseRentOrderCriteria()
        {
            IHouseRentOrderCriteria idal = PolicyInjectionFactory.Create().Create<HouseRentOrderCriteria, IDAL.IHouseRentOrderCriteria>();
            //try
            //{
            //    idal = IoCManager.Resolve<IHouseRentOrderCriteria>();
            //}
            //catch
            //{

            //}
            return idal;
        }
        public IDAL.ISubWayCriteria SubWayCriteria()
        {
            ISubWayCriteria idal = new DAL.MDb.SubWayCriteria();
            try
            {
                idal = IoCManager.Resolve<ISubWayCriteria>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ISubStationCriteria SubStationCriteria()
        {
            ISubStationCriteria idal = new DAL.MDb.SubStationCriteria();
            try
            {
                idal = IoCManager.Resolve<ISubStationCriteria>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ISearchAutoComplete SearchAutoComplete()
        {
            ISearchAutoComplete idal = new FindProperty.Lib.BLL.Common.DAL.Db.SearchAutoComplete();
            try
            {
                idal = IoCManager.Resolve<ISearchAutoComplete>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IMatchHouseSearchKeyword MatchHouseSearchKeyword()
        {
            IMatchHouseSearchKeyword idal = new FindProperty.Lib.BLL.Common.DAL.Db.MatchHouseSearchKeyword();
            try
            {
                idal = IoCManager.Resolve<IMatchHouseSearchKeyword>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ISearchRecommendTag SearchRecommendTag()
        {
            ISearchRecommendTag idal = new FindProperty.Lib.BLL.Common.DAL.MDb.SearchRecommendTag();
            try
            {
                idal = IoCManager.Resolve<ISearchRecommendTag>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ISearchTagCategoryCheck SearchTagCategoryCheck()
        {
            ISearchTagCategoryCheck idal = PolicyInjectionFactory.Create().Create<SearchTagCategoryCheck, IDAL.ISearchTagCategoryCheck>();
            try
            {
                idal = IoCManager.Resolve<ISearchTagCategoryCheck>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ICallAgent400 CallAgent400()
        {
            ICallAgent400 idal = new FindProperty.Lib.BLL.Common.DAL.Manual.CallAgent400();
            try
            {
                idal = IoCManager.Resolve<ICallAgent400>();
            }
            catch
            {

            }
            return idal;
        }
        public IDAL.IBarCode BarCode()
        {
            IBarCode idal = new FindProperty.Lib.BLL.Common.DAL.Manual.BarCode();
            try
            {
                idal = IoCManager.Resolve<IBarCode>();
            }
            catch
            {

            }
            return idal;
        }
    }
}
