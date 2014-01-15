using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.BLL.Common.ViewModel;
using FindProperty.Lib.Common;
using FindProperty.Lib.Json;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class CallAgent400
    {
        private readonly ICallAgent400 dal = DataAccessFactoryCreator.Create().CallAgent400();

        public CallAgent400Result GetCall400Object(string post_id,
            string called1, 
            string called1_no,
            string called1_name, 
            string called2,
            string called2_no,
            string called2_name,
            string called_message)
        {
            return JsonSerializer.FromJson<CallAgent400Result>(GetCall400(post_id,
                    called1,
                    called1_no,
                    called1_name,
                    called2,
                    called2_no,
                    called2_name,
                    called_message));
        }

        public string GetCall400(string post_id,
            string called1, 
            string called1_no,
            string called1_name, 
            string called2,
            string called2_no,
            string called2_name, 
            string called_message)
        {
            string rev = string.Empty;
            try
            {
                ViewModel.CallAgent400 call400 = dal.GetCall400(post_id,
                    called1,
                    called1_no,
                    called1_name,
                    called2,
                    called2_no,
                    called2_name,
                    called_message,
                    ConfigInfo.CallAPI400,
                    ConfigInfo.ConsumerKeySalesBlog,
                    ConfigInfo.ConsumerSecretSalesBlog);
                if (call400 != null)
                {
                    rev = call400.tel_num;
                }
            }
            catch
            {
                rev = called1;
            }
            if (string.IsNullOrEmpty(rev))
            {
                rev = called1;
            }

            string json = string.Empty;

            if (string.IsNullOrEmpty(rev))
            {
                json = "{\"tel\":\"" + called1 + "\",\"exten\":\"0\"}";
            }
            else
            {
                string[] tellist = rev.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder sb = new StringBuilder();
                if (tellist.Length > 0&&tellist.Length>1)
                {
                    sb.AppendFormat("{0}-", tellist[0].Substring(0, 4));
                    sb.AppendFormat("{0}-", tellist[0].Substring(4, 3));
                    sb.AppendFormat("{0}", tellist[0].Substring(7));
                    json = "{\"tel\":\"" + sb.ToString() + "\",\"exten\":\"" + tellist[1] + "\"}";
                }
                else
                {
                    json = "{\"tel\":\"" + called1 + "\",\"exten\":\"0\"}";
                }
            }

            return json;
        }
    }
}
