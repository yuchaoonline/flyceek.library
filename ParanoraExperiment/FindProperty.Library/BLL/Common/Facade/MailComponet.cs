using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class MailComponet
    {
        private readonly IMailComponent dal = new FindProperty.Lib.BLL.Common.DAL.Manual.MailComponet();

        public void Send(System.Net.Mail.MailMessage msg, string host, string port,string user,string psd)
        {
            dal.Send(msg, host,port,user,psd);
        }
    }
}
