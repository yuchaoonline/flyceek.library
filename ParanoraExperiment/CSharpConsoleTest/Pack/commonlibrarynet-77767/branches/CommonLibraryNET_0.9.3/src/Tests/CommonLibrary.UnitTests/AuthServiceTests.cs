using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Resources;
using NUnit.Framework;


using ComLib.Authentication;


namespace CommonLibrary.Tests
{
    [TestFixture]
    public class AuthTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            // TO_DO: Need to allow the windows auth service to do this automatically.
            Auth.Init(new AuthWin());
        }


        [Test]
        public void CanAuth()
        {
            Assert.IsTrue(Auth.IsAuthenticated());
            Assert.IsTrue(!Auth.IsGuest());
            int ndx = Auth.UserName.IndexOf("\\");
            string username = ndx == -1 ? Auth.UserName : Auth.UserName.Substring(ndx + 1);
            Assert.IsTrue(username == Environment.UserName);
        }
    }
}
