using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.IDAL;

namespace FindProperty.Lib.BLL.Common.DAL.Manual
{
    public class MailComponet : IMailComponent
    {
        public void Send(MailMessage msg, string host, string port, string user, string psd)
        {
            System.Net.Mail.SmtpClient Smtp = new SmtpClient();
            Smtp.Host = host;
            Smtp.Port = Int32.Parse(port);
            Smtp.Credentials = new NetworkCredential(user, psd);
            Smtp.Send((MailMessage)msg);
        }
    }
}
