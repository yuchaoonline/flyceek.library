using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IHistoryPost
    {
        List<ViewModel.HistoryPost> SelectHistoryPost();

        List<ViewModel.HistoryPost> SaveHistoryPost(List<ViewModel.HistoryPost> hisPosts);

        List<ViewModel.HistoryPost> AddHistoryPost(List<ViewModel.HistoryPost> hisPosts, ViewModel.HistoryPost hisPost);
    }
}
