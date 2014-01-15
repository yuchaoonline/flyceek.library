using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Common.DALFactory;
using FindProperty.Lib.BLL.Common.IDAL;
using FindProperty.Lib.Common;

namespace FindProperty.Lib.BLL.Common.Facade
{
    public class Entrust
    {
        private readonly IEntrust dal = DataAccessFactoryCreator.Create().Entrust();

        public int Send(Model.Entrust model)
        {
            Regex mail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            Regex phone = new Regex(@"((\d{3,4})|\d{3,4}-|\s)?\d{7,14}");

            if (!phone.Match(model.SenderMobile).Success)
            {
                return -1;
            }

            if (!string.IsNullOrEmpty(model.SenderEMail))
            {
                if (!mail.Match(model.SenderEMail).Success)
                {
                    return -2;
                }
            }


            int result= dal.Send(model);

            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(ConfigInfo.EmailSenderAddress, ConfigInfo.EmailUserName);
            Message.ReplyToList.Add(new MailAddress(model.SenderEMail, model.SenderName));
            Message.To.Add(new MailAddress(model.AgentEmail, model.AgentCName));
            if (!string.IsNullOrEmpty(model.ManagerEmail))
            {
                Message.To.Add(new MailAddress(model.ManagerEmail, model.ManagerCName));
            }

            Message.Subject ="网上搵楼 (客户查询)";
            Message.IsBodyHtml = true;
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">\r\n");
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><title>" + Message.Subject + "</title>\r\n");
            sb.Append("<style type=\"text/css\">\r\n");
            sb.Append("body { font-size: 1.3em; line-height: 1.5em; }\r\n");
            sb.Append("table th { vertical-align: top; text-align: left; }\r\n");
            sb.Append("table td { vertical-align: top; } \r\n");
            sb.Append("</style>\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>");
            sb.Append("<table>");
            sb.Append("<tr><th>客户姓名:</th><td>");
            sb.Append(System.Web.HttpUtility.HtmlEncode(model.SenderName));
            sb.Append("</td></tr>");
            sb.Append("<tr><th>联络电话:</th><td>");
            sb.Append(System.Web.HttpUtility.HtmlEncode(model.SenderMobile));
            sb.Append("</td></tr>");
            sb.Append("<tr><th>电邮:</th><td>");
            sb.Append(System.Web.HttpUtility.HtmlEncode(model.SenderEMail));
            sb.Append("</td></tr>");
            sb.Append("<tr><th>查询楼盘/内容:</th><td>");
            sb.Append(System.Web.HttpUtility.HtmlEncode(model.Content));
            sb.Append("</td></tr>");
            sb.Append("</body>");
            sb.Append("</html>");

            Message.Body = sb.ToString();
            Message.BodyEncoding = System.Text.Encoding.UTF8;

            try
            {
                new MailComponet().Send(Message, ConfigInfo.EmailHost, ConfigInfo.SmtpPort,ConfigInfo.EmailUserName,ConfigInfo.EmailPassword);
            }
            catch (System.Exception ex)
            {
            	
            }            

            return result;
        }
    }
}
