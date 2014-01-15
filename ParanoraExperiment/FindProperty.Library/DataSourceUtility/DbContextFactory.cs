using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Findproperty.DataContext.Db;
using FindProperty.Lib.BLL.SHTagToSalesBlog.DataContext.Db;
using FindProperty.Lib.BLL.Cms.DataContext.Db;
using FindProperty.Lib.Common;
using FindProperty.Lib.DBUtility.Mongo.Official;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using FindProperty.Lib.BLL.CentalineEst.DataContext.Db;
using FindProperty.Lib.DBUtility.Mongo;
using FindProperty.Lib.BLL.FindpropertyMainWeb.DataContext.Db;

namespace FindProperty.Lib.Factory
{
    public class DbContextFactory
    {
        private static SqlDatabase _Findproperty;
        private static SqlDatabase _SHTagToSalesBlog;
        private static SqlDatabase _Cms;
        private static SqlDatabase _CentalineEst;
        private static SqlDatabase _FIndpropertyMainWeb;


        public static SqlDatabase FIndpropertyMainWeb
        {
            get
            {
                if (_FIndpropertyMainWeb == null)
                {
                    _FIndpropertyMainWeb = new FindpropertyMainWebContext().Context();
                }
                return _FIndpropertyMainWeb;
            }
        }

        public static SqlDatabase CentalineEst
        {
            get
            {
                if (_CentalineEst == null)
                {
                    _CentalineEst = new CentalineEstContext().Context();
                }
                return _CentalineEst;
            }
        }

        public static SqlDatabase Findproperty
        {
            get
            {
                if (_Findproperty == null)
                {
                    _Findproperty = new FindPropertyContext().Context();
                }
                return _Findproperty;
            }
        }

        public static SqlDatabase SHTagToSalesBlog
        {
            get
            {
                if (_SHTagToSalesBlog == null)
                {
                    _SHTagToSalesBlog = new SHTagToSalesBlogContext().Context();
                }
                return _SHTagToSalesBlog;
            }
        }

        public static SqlDatabase Cms
        {
            get
            {
                if (_Cms == null)
                {
                    _Cms = new CmsContext().Context();
                }
                return _Cms;
            }
        }

        public static MongoContext<T> FindpropertyMongoDatabase<T>() where T : class
        {
            //MongoContext<T>.ConnectionString = ConfigInfo.MongoDatabaseConnectionString;
            //MongoContext<T>.DataBaseName = ConfigInfo.FindpropertyMongoDatabase;

            var signalValue = MongoConnctionArbitrateFactory.CreateDefault().GetConnction();

            return new MongoContext<T>(signalValue.ConnectionString, ConfigInfo.FindpropertyMongoDatabase,signalValue.Id);
        }

        public static MongoContext<T> SHTagToSalesBlogMongoDatabase<T>() where T : class
        {
            //MongoContext<T>.ConnectionString = ConfigInfo.MongoDatabaseConnectionString;
            //MongoContext<T>.DataBaseName = ConfigInfo.SHTagToSalesBlogMongoDatabase;

            var signalValue=MongoConnctionArbitrateFactory.CreateDefault().GetConnction();

            return new MongoContext<T>(signalValue.ConnectionString, ConfigInfo.SHTagToSalesBlogMongoDatabase, signalValue.Id);
        }


        public static MongoContext<T> FindPropertyInfomationMongoDatabase<T>() where T : class
        {
            //MongoContext<T>.ConnectionString = ConfigInfo.MongoDatabaseConnectionString;
            //MongoContext<T>.DataBaseName = ConfigInfo.FindpropertyMongoDatabase;
            //var signalValue = MongoConnctionArbitrateFactory.CreateDefault().GetConnction();
            return new MongoContext<T>(ConfigInfo.PropertyInfoConnection, ConfigInfo.PropertyInfoTableName,"2");
        }
    }
}
