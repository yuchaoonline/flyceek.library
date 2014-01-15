using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComLib.Configuration;


namespace ComLib.EmailSupport
{
    public class EmailHelper
    {
        /// <summary>
        /// Create the email service settings from the configuration source provided.
        /// </summary>
        /// <param name="config">The Configuration source.</param>
        /// <param name="emailServiceSectionName">The name of the section in the config source
        /// containing the email service settings.</param>
        /// <returns></returns>
        public static EmailServiceSettings GetSettings(IConfigSource config, string emailServiceSectionName)
        {
            var settings = new EmailServiceSettings();
            settings.IsAuthenticationRequired = config.GetDefault<bool>(emailServiceSectionName, "useAuthentication", false);
            settings.AuthenticationUserName = config.GetDefault<string>(emailServiceSectionName, "emailAuthenticationUser", "");
            settings.AuthenticationPassword = config.GetDefault<string>(emailServiceSectionName, "emailAuthenticationPassword", "");
            settings.SmptServer = config.GetDefault<string>(emailServiceSectionName, "smtpServer", "");
            settings.UsePort = config.GetDefault<bool>(emailServiceSectionName, "usePort", false);
            settings.Port = settings.UsePort ? config.GetDefault<int>(emailServiceSectionName, "port", 25) : 25;
            settings.From = config.GetDefault<string>(emailServiceSectionName, "from", "");
            return settings;
        }
    }
}
