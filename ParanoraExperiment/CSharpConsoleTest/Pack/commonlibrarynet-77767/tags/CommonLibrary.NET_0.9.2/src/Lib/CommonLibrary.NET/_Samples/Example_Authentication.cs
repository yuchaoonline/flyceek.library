using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Security.Principal;
using CommonLibrary;
using CommonLibrary.DomainModel;
using CommonLibrary.Membership;


namespace CommonLibrary.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Authentication : ApplicationTemplate
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Authentication()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {            
            // 1. Using default authentication ( WINDOWS )
            // NOTE: Known bug, need to figure out how to determine if admin on windows.
            Console.WriteLine("====================================================");
            Console.WriteLine("AUTHENTICATION ");
            Console.WriteLine("Is authenticated : " + AuthService.IsAuthenticated());
            Console.WriteLine("Is guest         : " + AuthService.IsGuest());
            Console.WriteLine("Is admin         : " + AuthService.IsAdmin());
            Console.WriteLine("UserName         : " + AuthService.UserName);
            Console.WriteLine("UserNameShort    : " + AuthService.UserShortName);
            Console.WriteLine(Environment.NewLine);

            // 2. Use a FAKE authentication ( useful for UNIT-TESTING )
            UserPrincipal userForUnitTest = new UserPrincipal(2, "kdog", "admin", new UserIdentity(2, "kdog", "custom", false));
            AuthService.Init(new AuthServiceWindows("admin", userForUnitTest));
            Console.WriteLine("Is authenticated : " + AuthService.IsAuthenticated());
            Console.WriteLine("Is guest         : " + AuthService.IsGuest());
            Console.WriteLine("Is admin         : " + AuthService.IsAdmin()); 
            Console.WriteLine("UserName         : " + AuthService.UserName);
            Console.WriteLine("UserNameShort    : " + AuthService.UserShortName);
            
            // 3. Using ASP.NET authentication ( via HttpContext.Current.User ).
            // The lambda is used for getting an IPrincipal given a username
            // who may not be the HttpContext.Current.User
            AuthService.Init(new AuthServiceWeb("admin", (username) => GetUser(username)));
            // Can not use this obviously.

            // Reset to windows.
            AuthService.Init(new AuthServiceWindows());
            return BoolMessageItem.True;
        }


        /// <summary>
        /// Get user data given the username.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private IPrincipal GetUser(string userName)
        {
            UserPrincipal sampleUser = new UserPrincipal(2, userName, "poweruser", new UserIdentity(2, userName, "poweruser", false));
            return sampleUser;
        }
    }
}
