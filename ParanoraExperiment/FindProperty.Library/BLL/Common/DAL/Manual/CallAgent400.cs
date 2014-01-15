using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class CallAgent400:IDAL.ICallAgent400
    {
        public ViewModel.CallAgent400 GetCall400(string post_id,
            string called1, 
            string called1_no,
            string called1_name, 
            string called2,
            string called2_no,
            string called2_name, 
            string called_message,
            string sericeUrl,
            string auth_consumer_key,
            string consumerSecret)
        {
            ViewModel.CallAgent400 call400=null;
            try
            {
                CentaAPIs.Auth.AuthBase authbase = new CentaAPIs.Auth.AuthBase();
                string ts = authbase.GenerateTimeStamp();
                string nonce = authbase.GenerateNonce();
                string url = sericeUrl;
                //string auth_consumer_key = System.Configuration.ConfigurationManager.AppSettings["ConsumerKeySalesBlog"].ToString();
                //string consumerSecret = System.Configuration.ConfigurationManager.AppSettings["ConsumerSecretSalesBlog"].ToString();
                string auth_signature = authbase.GenerateSignature(url, auth_consumer_key, consumerSecret, "POST", nonce, ts);
                //if (System.Configuration.ConfigurationManager.AppSettings["IsDebug"].ToString().Equals("1"))
                //{
                //    called1 = System.Configuration.ConfigurationManager.AppSettings["TempTel"].ToString();

                //}
                List<Centaline.CentaLib.Net.Part> pList = new List<Centaline.CentaLib.Net.Part>();
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "post_id", Value = post_id });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called1", Value = called1 });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called1_no", Value = called1_no });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called1_name", Value = called1_name });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called2", Value = "" });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called2_no", Value = "" });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called2_name", Value = "" });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "called_message", Value = called_message });

                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "auth_nonce", Value = nonce });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "auth_timestamp", Value = ts });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "auth_version", Value = "v1.0" });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "auth_signature", Value = auth_signature });
                pList.Add(new Centaline.CentaLib.Net.NormalPart() { Name = "auth_consumer_key", Value = auth_consumer_key });

                Centaline.CentaLib.Net.HttpPost hp = new Centaline.CentaLib.Net.HttpPost();
                string result = hp.PostRequest(url, pList);
                call400 = Json.JsonSerializer.DeserializeObject<ViewModel.CallAgent400>(result);
            }
            catch
            {
                call400 = null;
            }

            return call400;
        }
    }
}
