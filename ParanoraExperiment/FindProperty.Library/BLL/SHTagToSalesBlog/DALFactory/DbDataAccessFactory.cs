using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db;
using FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DALFactory
{
    public class DbDataAccessFactory : IDataAccessFactory
    {
        public ITagMain TagMain()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagMain, IDAL.ITagMain>();
        }

        public IDAL.ITagGscp TagGscp()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagGscp, IDAL.ITagGscp>();
        }

        public IDAL.ITagMkt TagMkt()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagMkt, IDAL.ITagMkt>();
        }

        public IDAL.ITagArea TagArea()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagArea, IDAL.ITagArea>();
        }
        public IDAL.ITagAgent TagAgent()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagAgent, IDAL.ITagAgent>();
        }

    }
}
