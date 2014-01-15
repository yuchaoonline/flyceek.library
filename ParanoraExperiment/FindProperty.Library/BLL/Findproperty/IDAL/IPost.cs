using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.BLL.Findproperty.ViewModel;
using FindProperty.Lib.Aop.Componet;
using FindProperty.Lib.Cache.Component.CacheKeyCreator;
using FindProperty.Lib.Cache.Component.ResultExaminer;

namespace FindProperty.Lib.BLL.Findproperty.IDAL
{

    public interface IPost
    {
        [CommonCallHandler(CacheSecond = 1200,ResultExaminerType=(typeof(IntegerResultExaminer)),DisableCache=true)]
        int SelectPostCount(AgentInfoSearchCriteria searchCriteria);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectPost(AgentInfoSearchCriteria searchCriteria);

        [CommonCallHandler(CacheSecond = 1200, ResultExaminerType = (typeof(IntegerResultExaminer)))]
        int SelectPostCount(HouseSearchCriteria searchCriteria);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectPost(HouseSearchCriteria searchCriteria);

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.Post> SelectPost(string refNo);

        [CommonCallHandler(CacheSecond = 1800)]
        List<ViewModel.PostDetail> SelectPostDetail(string refNo);

        //add 2013.09.10
        //[CommonCallHandler(CacheSecond = 1800)]
        //List<TempModel.Post> SelectTempPost(string refNo);

        //[CommonCallHandler(CacheSecond = 1800)]
        //List<ViewModel.PostDetail> SelectTempPost(string refNo);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectSimPostByPrice(string refNo, int top, double minPrice, double maxPrice, string scpMkt, string gscpId, string postType);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectSimPostBySize(string refNo, int top, double minSize, double maxSize, string scpMkt, string gscpId, string postType);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectSimPostByHouseType(string refNo, string scpMkt, string gscpId, int top, int sittingRoomCount, int bedroomCount, int toiletCount, int balconyCount, int ensuiteCount, string postType);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectRecommendPost(string refNo, string agentNo, string bigestcode, string cestCode, string postType, int top);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectPostByAgentNo(string agentNo);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectRecommendPostByAgentNo(string agentNo, string postType, int top);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectPkPostByRefNos(string refNos);

        [CommonCallHandler(CacheSecond = 1200)]
        List<ViewModel.Post> SelectTopPost(string orderBy,string order,int top);

        [CommonCallHandler(CacheSecond = 1200)]
        int getPunishCout();

        PostState GetPostState();
    }
}
