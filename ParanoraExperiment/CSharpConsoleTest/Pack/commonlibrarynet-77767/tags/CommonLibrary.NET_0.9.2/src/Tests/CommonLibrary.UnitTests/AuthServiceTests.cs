using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;

using CommonLibrary;


namespace CommonLibrary.Tests
{



    [TestFixture]
    public class AuthServiceTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            // TO_DO: Need to allow the windows auth service to do this automatically.
            AuthService.Init(new AuthServiceWindows());
        }


        [Test]
        public void CanAuth()
        {
            Assert.IsTrue(AuthService.IsAuthenticated());
            Assert.IsTrue(!AuthService.IsGuest());
            int ndx = AuthService.UserName.IndexOf("\\");
            string username = ndx == -1 ? AuthService.UserName : AuthService.UserName.Substring(ndx + 1);
            Assert.IsTrue(username == Environment.UserName);
        }
    }
}
