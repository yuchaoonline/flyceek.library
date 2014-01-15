using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.ViewModel;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class Post
    {
        private IDAL.IPost dal = DALFactory.DataAccessFactoryCreator.Create().Post();

        public List<ViewModel.Post> SelectPost(HouseSearchCriteria searchCriteria)
        {
            return dal.SelectPost(searchCriteria);
        }

        public int SelectPostCount(HouseSearchCriteria searchCriteria)
        {
            return dal.SelectPostCount(searchCriteria);
        }

        public List<ViewModel.Post> SelectRecommendPost(string refNo, string agentNo, string bigestcode, string cestCode, string postType, int top)
        {
            return dal.SelectRecommendPost(refNo, agentNo, bigestcode,cestCode, postType, top);
        }

        public List<ViewModel.Post> SelectSimPostByHouseType(string refNo, string scpMkt, string gscpId, int top, int sittingRoomCount, int bedroomCount, int toiletCount, int balconyCount, int ensuiteCount, string postType)
        {
            return dal.SelectSimPostByHouseType(refNo, scpMkt, gscpId, top, sittingRoomCount, bedroomCount, toiletCount, balconyCount, ensuiteCount, postType);
        }

        public List<ViewModel.Post> SelectSimPostByPrice(string refNo, int top, double minPrice, double maxPrice, string scpMkt, string gscpId, string postType) 
        {
            return dal.SelectSimPostByPrice(refNo, top, minPrice, maxPrice, scpMkt, gscpId, postType);
        }

        public List<ViewModel.Post> SelectSimPostBySize(string refNo, int top, double minSize, double maxSize, string scpMkt, string gscpId, string postType)
        {
            return dal.SelectSimPostBySize(refNo, top, minSize, maxSize, scpMkt, gscpId, postType);
        }

        public List<ViewModel.Post> SelectPost(AgentInfoSearchCriteria searchCriteria)
        {
            return dal.SelectPost(searchCriteria);
        }
        public int SelectPostCount(AgentInfoSearchCriteria searchCriteria)
        {
            return  dal.SelectPostCount(searchCriteria);
        }

        public List<ViewModel.Post> SelectPost(string refNo)
        {
            return dal.SelectPost(refNo); 
        }
        public List<ViewModel.PostDetail> SelectPostDetail(string refNo)
        {
            return dal.SelectPostDetail(refNo);
        }
        //public List<ViewModel.PostDetail> SelectPostDetail(string refNo)
        //{

        //}

        ////add 2013.09.10
        //public List<ViewModel.PostDetail> SelectTempPost(string refNo)
        //{
        //    return dal.SelectTempPost(refNo);
        //}

        public List<ViewModel.Post> SelectRecommendPostByAgentNo(string agentNo, string postType, int top)
        {
            return dal.SelectRecommendPostByAgentNo(agentNo, postType, top);
        }

        public List<ViewModel.Post> SelectPkPostByRefNos(string refNos)
        {
            return dal.SelectPkPostByRefNos(refNos);
        }

        public List<ViewModel.Post> SelectPostByAgentNo(string agentNo)
        {
            return dal.SelectPostByAgentNo(agentNo);
        }

        public List<ViewModel.Post> SelectTopPost(int top)
        {
            return dal.SelectTopPost("score", "desc", top);
        }

        public ViewModel.PostState GetPostState()
        {
            return dal.GetPostState();
        }

        public int getPunishCout()
        {
            return dal.getPunishCout();
        }
    }
}
