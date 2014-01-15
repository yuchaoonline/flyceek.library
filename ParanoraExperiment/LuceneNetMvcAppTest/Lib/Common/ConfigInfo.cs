using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuceneNetMvcAppTest.Lib.Common
{
    public class ConfigInfo
    {
        public static string FindPropertyDb
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FindPropertyDb"];
            }
        }
        
        public static string PanGuConfigPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["PanGuConfigPath"];
            }
        }

        public static string FindProperty_LuceneIndex_StorePath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FindProperty_LuceneIndex_StorePath"];
            }
        }

        public static int FindProperty_LuceneSearch_PageSize
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["FindProperty_LuceneSearch_PageSize"]);
            }
        }
    }
}