using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Aop;
using FindProperty.Lib.BLL.Common;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.DALFactory
{
    public class MDbDataAccessFactory:IDataAccessFactory
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
            IAgentPostSummary idal = PolicyInjectionFactory.Create().Create<DAL.MDb.AgentPostSummary, IDAL.IAgentPostSummary>();
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
            IAgentInfoSearchCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentInfoSearchCriteria, IDAL.IAgentInfoSearchCriteria>();
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
            IAgentHouseSizeCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentHouseSizeCriteria, IDAL.IAgentHouseSizeCriteria>();
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
            IAgentHouseSalePriceCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentHouseSalePriceCriteria, IDAL.IAgentHouseSalePriceCriteria>();
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
            IAgentHouseRentPriceCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentHouseRentPriceCriteria, IDAL.IAgentHouseRentPriceCriteria>();
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
            IAgentHouseBedRoomCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentHouseBedRoomCriteria, IDAL.IAgentHouseBedRoomCriteria>();
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
            IAgentListSearchCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentListSearchCriteria, IDAL.IAgentListSearchCriteria>();
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
            IAgentListOrderCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.AgentListOrderCriteria, IDAL.IAgentListOrderCriteria>();
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
            IHouseListStyle idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseListStyle, IHouseListStyle>();
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
            IHistoryPost idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HistoryPost, IHistoryPost>();
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
            IHouseSalePriceCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseSalePriceCriteria, IHouseSalePriceCriteria>();
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
            IHouseRentPriceCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseRentPriceCriteria, IHouseRentPriceCriteria>();
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
            IHouseSizeCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseSizeCriteria, IHouseSizeCriteria>();
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
            IHouseBedRoomCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseBedRoomCriteria, IHouseBedRoomCriteria>();
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
            IHouseSearchCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseSearchCriteria, IHouseSearchCriteria>();
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
            IHouseSaleOrderCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseSaleOrderCriteria, IHouseSaleOrderCriteria>();
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
            IHouseRentOrderCriteria idal = PolicyInjectionFactory.Create().Create<DAL.Manual.HouseRentOrderCriteria, IHouseRentOrderCriteria>();
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
            ISubWayCriteria idal = PolicyInjectionFactory.Create().Create<DAL.MDb.SubWayCriteria, ISubWayCriteria>();
            
            return idal;
        }

        public IDAL.ISubStationCriteria SubStationCriteria()
        {
            ISubStationCriteria idal = PolicyInjectionFactory.Create().Create<DAL.MDb.SubStationCriteria, ISubStationCriteria>();
            
            return idal;
        }

        public IDAL.ISearchAutoComplete SearchAutoComplete()
        {
            ISearchAutoComplete idal = PolicyInjectionFactory.Create().Create<DAL.MDb.SearchAutoComplete, ISearchAutoComplete>();
            
            return idal;
        }

        public IDAL.IMatchHouseSearchKeyword MatchHouseSearchKeyword()
        {
            IMatchHouseSearchKeyword idal = PolicyInjectionFactory.Create().Create<DAL.MDb.MatchHouseSearchKeyword, IMatchHouseSearchKeyword>();
            
            return idal;
        }

        public IDAL.ISearchRecommendTag SearchRecommendTag()
        {
            ISearchRecommendTag idal = PolicyInjectionFactory.Create().Create<DAL.MDb.SearchRecommendTag, ISearchRecommendTag>();
            
            return idal;
        }

        public IDAL.ISearchTagCategoryCheck SearchTagCategoryCheck()
        {
            ISearchTagCategoryCheck idal = PolicyInjectionFactory.Create().Create<DAL.Manual.SearchTagCategoryCheck, ISearchTagCategoryCheck>();

            return idal;
        }

        public IDAL.ICallAgent400 CallAgent400()
        {
            ICallAgent400 idal = PolicyInjectionFactory.Create().Create<DAL.Manual.CallAgent400, ICallAgent400>();

            return idal;
        }
        public IDAL.IBarCode BarCode()
        {
            IBarCode idal = PolicyInjectionFactory.Create().Create<DAL.Manual.BarCode, IBarCode>();

            return idal;
        }
    }
}
