using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using ComLib;
using ComLib.Entities;
using ComLib.Membership;
using ComLib.Application;
using ComLib.Logging;
using ComLib.EmailSupport;
using ComLib.Notifications;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Notifications : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Notifications()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // Configure the notification service.
            // Since emailing is disabled, the EmailServiceSettings are not configured.
            // The emails are not sent as "EnableNotifications" = false above.
            // Debugging is turned on so you can see the emails in the folder "Notifications.Tests".
            NotificationService.Init(new EmailService(new EmailServiceSettings()), new NotificationSettings());
            NotificationService.Settings["website.name"] = "KnowledgeDrink.com";
            NotificationService.Settings["website.url"] = "http://www.KnowledgeDrink.com";
            NotificationService.Settings["website.urls.contactus"] = "http://www.KnowledgeDrink.com/contactus.aspx";
            NotificationService.Settings.EnableNotifications = false;
            NotificationService.Settings.DebugOutputMessageToFile = true;
            NotificationService.Settings.DebugOutputMessageFolderPath = @"Notifications.Tests";

            Logger.Info("====================================================");
            Logger.Info("NOTIFICATION EMAILS ");
            Logger.Info("Emails are generated to folder : " + NotificationService.Settings.DebugOutputMessageFolderPath);
            NotificationService.AccountService.WelcomeNewUser("user1@mydomain.com", "Welcome to www.knowledgedrink.com", "batman", "user1", "password");
            NotificationService.AccountService.RemindUserPassword("user1@mydomain.com", "Welcome to www.knowledgedrink.com", "batman", "user1", "password");
            NotificationService.MessageService.SendToFriend("batman@mydomain.com", "Check out www.knowledgedrink.com", "superman", "bruce", "Learn to fight.");
            NotificationService.MessageService.SendToFriendPost("superman@mydomain.com", "Check out class at www.knowledgedrink.com", "batman", "clark", "Punk, learn to fly.",
                "Learn to fly", "http://www.knowledgedrink.com/classes/learn-to-fly.aspx");
            NotificationService.Queue.Process();
            return BoolMessageItem.True;
        }
    }
}
