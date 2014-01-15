using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using ComLib.EmailSupport;
using ComLib.Notifications;


namespace CommonLibrary.Tests
{
    [TestFixture]
    public class NotificationTests
    {
        [SetUp]
        public void Init()
        {
            NotificationService.Init(new EmailService(new EmailServiceSettings()), new NotificationSettings());
            NotificationService.Settings["website.name"] = "KnowledgeDrink.com";
            NotificationService.Settings["website.url"] = "http://www.KnowledgeDrink.com";
            NotificationService.Settings["website.urls.contactus"] = "http://www.KnowledgeDrink.com/contactus.aspx";
            NotificationService.Settings.EnableNotifications = false;
            NotificationService.Settings.DebugOutputMessageToFile = true;
            NotificationService.Settings.DebugOutputMessageFolderPath = @"F:\dev\Workshops\knowledgedrink\src\Tests\GenericCode.Tests\email.tests";
        }


        [Test]
        public void CanSendWelcomeEmail()
        {            
            IDictionary<string, string> values = new Dictionary<string, string>();
            values["firstname"] = "kishore";
            values["username"] = "kuser1";
            values["password"] = "password";
            values["message.subject"] = "welcome to knowledgedrink.com";
            values["message.to"] = "kishore@k.com";
            NotificationService.AccountService.WelcomeNewUser(new NotificationContext(values));
            NotificationService.Queue.Process();
        }


        [Test]
        public void CanSendRemindUserEmail()
        {
            IDictionary<string, string> values = new Dictionary<string, string>();
            values["firstname"] = "kishore";
            values["username"] = "kuser1";
            values["password"] = "password";
            values["message.subject"] = "Password reminder from knowledgedrink.com";
            values["message.to"] = "kishore@k.com";
            NotificationService.AccountService.RemindUserPassword(new NotificationContext(values));
            NotificationService.Queue.Process();
        }


        [Test]
        public void CanSendWebSiteUrlEmail()
        {
            IDictionary<string, string> values = new Dictionary<string, string>();
            values["firstname"] = "superman";
            values["from.name"] = "kishore";
            values["message.briefmessage"] = "check out this site.";
            values["message.subject"] = "welcome to knowledgedrink.com";
            values["message.to"] = "kishore@k.com";
            NotificationService.MessageService.SendToFriend(new NotificationContext(values));
            NotificationService.Queue.Process();
        }


        [Test]
        public void CanSendWebSitePostUrlEmail()
        {
            IDictionary<string, string> values = new Dictionary<string, string>();
            values["firstname"] = "superman";
            values["post.title"] = "learn to use web development frameworks.";
            values["post.url"] = "http://www.knowledgedrink.com/learn-to-use-web-development-frameworks.aspx";
            values["from.name"] = "kishore";
            values["message.briefmessage"] = "check out this site.";
            values["message.subject"] = "welcome to knowledgedrink.com";
            values["message.to"] = "kishore@k.com";
            NotificationService.MessageService.SendToFriendPost(new NotificationContext(values));
            NotificationService.Queue.Process();
        }
    }
}
