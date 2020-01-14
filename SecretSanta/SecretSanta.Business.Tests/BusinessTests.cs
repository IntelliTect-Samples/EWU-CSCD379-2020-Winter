using SecretSanta.Business;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class BusinessTests
    {
        private readonly User _TestUser = new User(2, "Dave", "Smith");
        [TestMethod]
        public void CompareBasics()
        {
            // arrange
            int testId = 0;
            string testTitle = "My New Webpage";

            string testDescription = "Hello, Internet";
            string testUrl = "www.WhoKnows.com";
            Gift newGift = new Gift(testId, testTitle, testDescription, testUrl, _TestUser);

            // assert
            Assert.AreEqual<string>(testTitle, newGift.Title, "Title value is unexpected");
            Assert.AreEqual<string>(testDescription, newGift.Description, "Content value is unexpected");
            Assert.AreEqual<string>(_TestUser.ToString(), newGift.User.ToString(), "Author value is unexpected");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftNullException()
        {
            Gift theGift = new Gift(1, null!, "Blank", ".com", _TestUser);
        }
    }
}
