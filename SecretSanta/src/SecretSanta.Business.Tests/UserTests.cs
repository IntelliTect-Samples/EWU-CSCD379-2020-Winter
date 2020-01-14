using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{

    [TestClass]
    public class UserTests
    {

        [TestMethod]
        public void Constructor_Valid_RunsSuccessfully()
        {
            var user = new User(3141, "Brett", "Henning");

            Assert.IsNotNull(user);
        }

        [DataTestMethod]
        [DataRow(null, "Henning")]
        [DataRow("Brett", null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Invalid_ThrowsException(string firstName, string lastName)
        {
            var unused = new User(3141, firstName, lastName);
        }

    }

}
