using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DALFactory
{
    public class MDbDataAccessFactory:IDataAccessFactory
    {
        public IDAL.ITagMain TagMain()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.TagMain, IDAL.ITagMain>();
        }

        public IDAL.ITagGscp TagGscp()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.TagGscp, IDAL.ITagGscp>();
        }

        public IDAL.ITagMkt TagMkt()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.TagMkt, IDAL.ITagMkt>();
        }

        public IDAL.ITagArea TagArea()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.TagArea, IDAL.ITagArea>();
        }
        public IDAL.ITagAgent TagAgent()
        {
            return Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.TagAgent, IDAL.ITagAgent>();
        }

    }
}
