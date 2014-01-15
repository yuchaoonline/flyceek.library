using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class HistoryPost
    {
        private readonly IHistoryPost dal = DataAccessFactoryCreator.Create().HistoryPost();

        public List<ViewModel.HistoryPost> SelectHistoryPost()
        {
            return dal.SelectHistoryPost();
        }
        public List<ViewModel.HistoryPost> SaveHistoryPost(List<ViewModel.HistoryPost> hisPosts)
        {
            return dal.SaveHistoryPost(hisPosts);
        }
        public List<ViewModel.HistoryPost> AddHistoryPost(List<ViewModel.HistoryPost> hisPosts, ViewModel.HistoryPost hisPost)
        {
            return dal.AddHistoryPost(hisPosts, hisPost);
        }
    }
}
