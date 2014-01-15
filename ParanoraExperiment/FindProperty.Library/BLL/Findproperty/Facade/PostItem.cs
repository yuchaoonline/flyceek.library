using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class PostItem
    {
        private IDAL.IPostItem dal = DALFactory.DataAccessFactoryCreator.Create().PostItem();

        public List<ViewModel.PostItem> SelectPostItem(string postId)
        {
            return dal.SelectPostItem(postId);
        }
    }
}
