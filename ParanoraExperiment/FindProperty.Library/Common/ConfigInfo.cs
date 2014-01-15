using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.Common
{
    public class ConfigInfo
    {
        public static string[] BlockRecordHitCountRequestIpList
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BlockRecordHitCountRequestIpList"].Split(',');
            }
        }

        public static string FindPropertyWebResourceUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FindPropertyWebResourceUrl"];
            }
        }
        
        public static string FindPropertyMainWebImageSaveUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FindPropertyMainWebImageSaveUrl"];
            }
        }
        
        public static int EnableCall400
        {
            get { return int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["EnableCall400"]); }
        }

        public static String CriteriaStateFilePath
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["CriteriaStateFilePath"]; }
        }

        public static String EmailHost
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["EmailHost"]; }
        }

        public static String EmailPassword
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["EmailPassword"]; }
        }

        public static String EmailUserName
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["EmailUserName"]; }
        }

        public static String EmailSenderAddress
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["EmailSenderAddress"]; }
        }

        public static String SmtpHost
        {
            get{return System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPHOST"];}            
        }

        public static String SmtpPort
        {
            get{return System.Web.Configuration.WebConfigurationManager.AppSettings["SMTPPORT"];}
        }

        public static string StartPage
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["StartPage"];
            }
        }
        public static int EnableCache
        {
            get
            {
                int enableCache = 0;
                string enableCacheStr = System.Configuration.ConfigurationManager.AppSettings["EnableCache"];

                if (!int.TryParse(enableCacheStr, out enableCache))
                {
                    enableCache = 0;
                }
                return enableCache;
            }
        }

        public static string[] HostList
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HostList"].Split(',');
            }
        }

        public static string AgentHitCountMSMQPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AgentHitCountMSMQPath"];
            }
        }

        public static string HitCountMSMQPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HitCountMSMQPath"];
            }
        }
        public static string HitCountWMSMQPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HitCountWMSMQPath"];
            }
        }
        public static string MainRecommendTag
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MainRecommendTag"];
            }
        }

        public static string SMTPPORT
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SMTPPORT"];
            }
        }
        public static string SMTPHOST
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SMTPHOST"];
            }
        }
        public static string MainUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MainUrl"];
            }
        }
        public static string CurrentMainUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CurrentMainUrl"];
            }
        }
        public static string AgentNetrust
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AgentNetrust"];
            }
        }
        public static string AgentInfo
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AgentInfo"];
            }
        }
        public static string AgentList
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AgentList"];
            }
        }
        public static string HouseInfoView
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HouseInfoView"];
            }
        }
        public static string HouseListSale
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HouseListSale"];
            }
        }
        public static string WeiboUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WeiboUrl"];
            }
        }
        public static string HouseListRent
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HouseListRent"];
            }
        }
        
        public static string HistoryPostCookieName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HistoryPostCookieName"];
            }
        }
        public static string ImgSercieUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImgSercieUrl"];
            }
        }
        public static string ConsumerSecretSalesBlog
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConsumerSecretSalesBlog"];
            }
        }
        public static string ConsumerKeySalesBlog
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConsumerKeySalesBlog"];
            }
        }
        public static string CallAPI400
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CallAPI400"];
            }
        }
        public static string FindpropertyMongoDatabase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FindpropertyMongoDatabase"];
            }
        }
        public static string SHTagToSalesBlogMongoDatabase
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SHTagToSalesBlogMongoDatabase"];
            }
        }
        public static string MongoDatabaseConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MongoDatabaseConnectionString"];
            }
        }
        public static string MongoDatabasePrimaryConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MongoDatabasePrimaryConnectionString"];
            }
        }

        public static string UnityConfigFilePath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UnityConfigFilePath"];
            }
        }

        public static string IoCComponetTypeName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["IoCComponetTypeName"];
            }
        }


        public static string PropertyInfoConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PropertyInfoConnection"];
            }
        }
        public static string PropertyInfoTableName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PropertyInfoTableName"];
            }
        }


        public static decimal Pricesection
        {
            get 
            {
                return Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["pricesection"]);
            }
        }

        public static decimal Rentalsection
        {
            get
            {
                return Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["rentalsection"]);
            }
        }

        public static double Percent
        {
            get
            {
                return Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["percent"]);
            }
        }

        #region memcache
        public static string[] ServerList
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["ServerList"].Split(new char[] { ',' }); }
        }
        public static int[] Weight
        {
            get
            {
                string[] weights = System.Configuration.ConfigurationManager.AppSettings["Weight"].Split(new char[] { ',' });
                int[] ws = new int[weights.Length];

                for (int i = 0; i < weights.Length; i++)
                {
                    ws[i] = int.Parse(weights[i]);
                }
                return ws;
            }
        }

        public static int MaxConnection
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxConnection"]); }
        }

        public static int InitConnections
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["InitConnections"]); }
        }
        public static int MinConnections
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinConnections"]); }
        }



        public static int SocketConnectTimeout
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SocketConnectTimeout"]); }
        }

        public static int SocketTimeout
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["SocketTimeout"]); }
        }
        public static int MaintenanceSleep
        {
            get { return int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaintenanceSleep"]); }
        }
        public static bool Nagle
        {
            get
            {
                int i = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Nagle"]);

                if (i > 0)
                    return true;
                return false;
            }
        }
        public static bool Failover
        {
            get
            {
                int i = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Failover"]);

                if (i > 0)
                    return true;
                return false;
            }
        }
        #endregion
    }
}
