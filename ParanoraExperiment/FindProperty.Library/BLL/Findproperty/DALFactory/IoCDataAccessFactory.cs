using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Findproperty.DAL.Db;
using FindProperty.Lib.BLL.Findproperty.IDAL;
using FindProperty.Lib.Ioc;

namespace FindProperty.Lib.BLL.Findproperty.DALFactory
{
    public class IoCDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IBranch Branch()
        {
            IBranch idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Branch, IDAL.IBranch>();
            try
            {
                idal = IoCManager.Resolve<IBranch>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IEstScore EstScore()
        {
            IEstScore idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.EstScore, IDAL.IEstScore>();
            try
            {
                idal = IoCManager.Resolve<IEstScore>();
            }
            catch
            {

            }
            return idal;
        }
        public IDAL.IEnquiry Enquiry()
        {
            IEnquiry idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Enquiry, IDAL.IEnquiry>();
            try
            {
                idal = IoCManager.Resolve<IEnquiry>();
            }
            catch
            {

            }
            return idal;
        }
        public IDAL.IGscp Gscp()
        {
            IGscp idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Gscp, IDAL.IGscp>();
            try
            {
                idal = IoCManager.Resolve<IGscp>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IRegion Region()
        {
            IRegion idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Region, IDAL.IRegion>();
            try
            {
                idal = IoCManager.Resolve<IRegion>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IScp Scp()
        {
            IScp idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Scp, IDAL.IScp>();
            try
            {
                idal = IoCManager.Resolve<IScp>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IPost Post()
        {
            IPost idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Post, IDAL.IPost>();
            try
            {
                idal = IoCManager.Resolve<IPost>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IPostImage PostImage()
        {
            IPostImage idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.PostImage, IDAL.IPostImage>();
            try
            {
                idal = IoCManager.Resolve<IPostImage>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IPostItem PostItem()
        {
            IPostItem idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.PostItem, IDAL.IPostItem>();
            try
            {
                idal = IoCManager.Resolve<IPostItem>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.ICenest Cenest()
        {
            ICenest idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Cenest, IDAL.ICenest>();
            try
            {
                idal = IoCManager.Resolve<ICenest>();
            }
            catch
            {

            }
            return idal;
        }

        public IDAL.IAgent Agent()
        {
            IAgent idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Agent, IDAL.IAgent>();
            try
            {
                idal = IoCManager.Resolve<IAgent>();
            }
            catch
            {

            }
            return idal;
        }



        public IAppUpdate AppUpdate()
        {
            IAppUpdate idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.AppUpdate, IDAL.IAppUpdate>();

            try
            {
                idal = IoCManager.Resolve<IAppUpdate>();
            }
            catch
            {

            }
            return idal;
        }
    }
}
