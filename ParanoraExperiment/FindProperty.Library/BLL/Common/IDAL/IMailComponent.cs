using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FindProperty.Lib.BLL.Common.IDAL
{
    public interface IMailComponent
    {
        void Send(MailMessage msg, string host, string port,string user,string psd);
    }
}
