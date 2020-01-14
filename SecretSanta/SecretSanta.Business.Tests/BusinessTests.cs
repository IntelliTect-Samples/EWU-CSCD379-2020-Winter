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
        public void GiftHasCorrectValues()
        {
            int testId = 0;
            string testTitle = "My New Webpage";

            string testDescription = "Hello, Internet";
            string testUrl = "www.WhoKnows.com";
            Gift newGift = new Gift(testId, testTitle, testDescription, testUrl, _TestUser);

            Assert.AreEqual<string>(testTitle, newGift.Title, "Title value is unexpected");
            Assert.AreEqual<string>(testDescription, newGift.Description, "Description value is unexpected");
            Assert.AreEqual<string>(_TestUser.ToString(), newGift.User.ToString(), "toString() value is unexpected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftNullExceptionTitle()
        {
            Gift theGift = new Gift(1, null!, "Blank", ".com", _TestUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftNullExceptionDescription()
        {
            Gift theGift = new Gift(1, "HomeSlice", null!, ".com", _TestUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftNullExceptionUrl()
        {
            Gift theGift = new Gift(1, "HomeSlice", "Blank", null!, _TestUser);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftNullExceptionUser()
        {
            Gift theGift = new Gift(1, "HomeSlice", "Blank", ".com", null!);
        }

        [TestMethod]
        public void UserHasCorrectValues()
        {
            int testId = 0;
            string testLast = "James";
            string testFirst = "Jesse";
            User tempUser = new User(testId, testFirst, testLast);

            Assert.AreEqual<string>(testFirst, tempUser.FirstName, "FirstName value is unexpected");
            Assert.AreEqual<string>(testLast, tempUser.LastName, "LastName value is unexpected");
            Assert.AreEqual<int>(testId, tempUser.Id, "Id value is unexpected");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserNullExceptionFirstName()
        {
            User newUser = new User(12, null!, "Mark");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserNullExceptionLastName()
        {
            User newUser = new User(12, "James", null!);
        }
    }
}
