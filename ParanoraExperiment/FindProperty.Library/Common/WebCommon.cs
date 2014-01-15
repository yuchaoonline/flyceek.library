using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FindProperty.Lib.Common
{
    public class WebCommon
    {
        public static string UrlDecodeQueryString(string k)
        {
            string v = HttpContext.Current.Request.QueryString[k];
            if (!string.IsNullOrEmpty(v))
            {
                v = HttpContext.Current.Server.UrlDecode(v);
            }
            else
            {
                v = string.Empty;
            }
            return v;
        }

        public static bool RequestHostValidity(string host,string ip)
        {
            bool result = true;
            result=(ConfigInfo.HostList.Where(x => x.ToLower() == host.ToLower()).Count() > 0);
            return result;
        }
        
        
        
        public static bool RequestHitCountValidity(string host, string ip)
        {
            bool result = true;
            result=(ConfigInfo.HostList.Where(x => x.ToLower() == host.ToLower()).Count() > 0);

            if (result)
            {
                if (!string.IsNullOrEmpty(ip))
                {
                    result = (ConfigInfo.BlockRecordHitCountRequestIpList.Where(x => x == ip).Count() < 1);
                }
            }

            return result;
        }

        public static string Content(string resource)
        {
            return ConfigInfo.FindPropertyWebResourceUrl + resource;
        }
    }
}
