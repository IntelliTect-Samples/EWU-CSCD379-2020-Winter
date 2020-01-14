using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{

    [TestClass]
    public class GiftTests
    {

        [TestMethod]
        public void Constructor_Valid_RunsSuccessfully()
        {
            var user = new User(3141, "Brett", "Henning");
            var gift = new Gift(16, "Soccer Jersey", "Jersey Worn in High School", "https://gamerbah.com", user);

            Assert.IsNotNull(gift);
        }

        [DataTestMethod]
        [DataRow(null, "Description", "URL")]
        [DataRow("Title", null, "URL")]
        [DataRow("Title", "Description", null)]
        // Null User
        [DataRow("Title", "Description", "URL")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Invalid_ThrowsException(string title, string description, string url)
        {
            var unused = new Gift(1, title, description, url, null);
        }

    }

}
