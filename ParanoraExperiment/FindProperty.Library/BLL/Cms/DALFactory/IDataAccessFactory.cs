using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Cms.IDAL;

namespace FindProperty.Lib.BLL.Cms.DALFactory
{
    public interface IDataAccessFactory
    {
        IIcmArticle IcmArticle();
    }
}
