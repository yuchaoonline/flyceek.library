using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.Cms.DALFactory
{
    public class IoCDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IIcmArticle IcmArticle()
        {
            IDAL.IIcmArticle idal = new DAL.Db.IcmArticle();
            try
            {
                idal = IoCManager.Resolve<IDAL.IIcmArticle>();
            }
            catch
            {

            }
            return idal;
        }
    }
}
