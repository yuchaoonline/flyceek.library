using FindProperty.Lib.Aop.Componet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FindProperty.Lib.BLL.Findproperty.IDAL
{
    public interface IPostItem
    {   
        [CommonCallHandler]
        List<ViewModel.PostItem> SelectPostItem(string postId);
    }
}
