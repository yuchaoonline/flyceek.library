using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Cms.DALFactory
{
    public class DbDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IIcmArticle IcmArticle()
        {
            return new DAL.Db.IcmArticle();
        }
    }
}
