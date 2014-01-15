using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.SHTagToSalesBlog.DAL.Db;
using FindProperty.Lib.BLL.SHTagToSalesBlog.IDAL;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.SHTagToSalesBlog.DALFactory
{
    public class IoCDataAccessFactory : IDataAccessFactory
    {
        public ITagMain TagMain()
        {
            ITagMain idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagMain, IDAL.ITagMain>();
            try
            {
                idal = IoCManager.Resolve<ITagMain>();
            }
            catch
            {
                
            }
            return idal;
        }

        public IDAL.ITagGscp TagGscp()
        {
            ITagGscp idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagGscp, IDAL.ITagGscp>();
            try
            {
                idal = IoCManager.Resolve<ITagGscp>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ITagMkt TagMkt()
        {
            ITagMkt idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagMkt, IDAL.ITagMkt>();
            try
            {
                idal = IoCManager.Resolve<ITagMkt>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ITagArea TagArea()
        {
            ITagArea idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagArea, IDAL.ITagArea>();
            try
            {
                idal = IoCManager.Resolve<ITagArea>();
            }
            catch
            {

            }
            return idal;
        }
        public IDAL.ITagAgent TagAgent()
        {
            ITagAgent idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.TagAgent, IDAL.ITagAgent>();
            try
            {
                idal = IoCManager.Resolve<ITagAgent>();
            }
            catch
            {

            }
            return idal;
        }
    }
}
