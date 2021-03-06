﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Findproperty.IDAL;

namespace FindProperty.Lib.BLL.Findproperty.DALFactory
{
    public class MDbDataAccessFactory:IDataAccessFactory
    {
        public IDAL.IBranch Branch()
        {
            IBranch idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Branch, IDAL.IBranch>();

            return idal;
        }

        public IDAL.IEstScore EstScore()
        {
            IEstScore idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.EstScore, IDAL.IEstScore>();

            return idal;
        }

        public IDAL.IEnquiry Enquiry()
        {
            IEnquiry idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Enquiry, IDAL.IEnquiry>();

            return idal;
        }

        public IDAL.IGscp Gscp()
        {
            IGscp idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Gscp, IDAL.IGscp>();

            return idal;
        }

        public IDAL.IRegion Region()
        {
            IRegion idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Region, IDAL.IRegion>();

            return idal;
        }

        public IDAL.IScp Scp()
        {
            IScp idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Scp, IDAL.IScp>();

            return idal;
        }

        public IDAL.IPost Post()
        {
            IPost idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Post, IDAL.IPost>();

            return idal;
        }

        public IDAL.IPostImage PostImage()
        {
            IPostImage idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.PostImage, IDAL.IPostImage>();

            return idal;
        }

        public IDAL.IPostItem PostItem()
        {
            IPostItem idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.PostItem, IDAL.IPostItem>();

            return idal;
        }

        public IDAL.ICenest Cenest()
        {
            ICenest idal = Aop.PolicyInjectionFactory.Create().Create<DAL.Db.Cenest, IDAL.ICenest>();

            return idal;
        }

        public IDAL.IAgent Agent()
        {
            IAgent idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.Agent, IDAL.IAgent>();

            return idal;
        }


        public IAppUpdate AppUpdate()
        {
            IAppUpdate idal = Aop.PolicyInjectionFactory.Create().Create<DAL.MDb.AppUpdate, IDAL.IAppUpdate>();

            return idal;
        }
    }
}
