using System;
using System.Collections.Generic;
using System.Text;

using ComLib;
using ComLib.ValidationSupport;
using NUnit.Framework;

namespace CommonLibrary.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void IsPhoneValid()
        {
            ValidationResults errors = new ValidationResults();
            Assert.IsTrue(Validation.IsPhoneUS("347-512-3161", false, errors, null));
        }


        [Test]
        public void IsPhoneInvalid()
        {
            ValidationResults errors = new ValidationResults();
            Assert.IsFalse(Validation.IsPhoneUS("347sdf-512-3161", false, errors, null));
        }


        [Test]
        public void ValidatePhone()
        {
            ValidationResults errors = new ValidationResults();
            Assert.IsTrue(Validation.IsPhoneUS("3475123161", false, errors, null));
        }
    }
}
