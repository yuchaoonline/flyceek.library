using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Findproperty.Facade
{
    public class PostImage
    {
        private IDAL.IPostImage dal = DALFactory.DataAccessFactoryCreator.Create().PostImage();

        public List<ViewModel.PostImage> SelectPostImage(string postId)
        {
            return dal.SelectPostImage(postId);
        }
    }
}
