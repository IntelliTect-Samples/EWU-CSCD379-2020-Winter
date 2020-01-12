using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CA1707 // doesn't apply to test
namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Create_Gift_Success()
        {
            //arrange
            int id = 0;
            string title = "gift";
            string description = "is a gift!";
            string url = "http://127.0.0.1:7";
            User user = new User(0, "John", "Smith");

            Gift gift = new Gift(id, title, description, url, user);

            //act

            //assert
            Assert.AreEqual<int>(id, gift.Id);
            Assert.AreEqual<string>(title, gift.Title);
            Assert.AreEqual<string>(description, gift.Description);
            Assert.AreEqual<string>(url, gift.Url);
            Assert.AreSame(user, gift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_VerifyTitleNotNullable()
        {
            Gift gift = new Gift(0, null!, "", "", new User(0, "John", "Smith"));
        }
    }
}
#pragma warning restore CA1707