using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTester
    {
        [TestMethod]
        public void Create_Gift_Success()
        {
            //Arrange
            int id = 1;
            string title = "RandomGift title";
            string description = "A random Gift description";
            string url = "AGiftUrl";
            User testUser = new User(123, "First", "Last", null);

            //Act

            Gift testGift = new Gift(id, title, description, url, testUser);

            //Assert

            Assert.AreEqual(testGift.Id, id);
            Assert.AreEqual(testGift.Title, title);
            Assert.AreEqual(testGift.Description, description);
            Assert.AreEqual(testGift.Url, url);
            Assert.IsNotNull(testGift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTitle_Gift_ThrowsArgumentNullException()
        {
            //Arrange

            int id = 1;
            string title = "RandomGift title";
            string description = "A random Gift description";
            string url = "AGiftUrl";
            User testUser = new User(123, "First", "Last", null);

            //Act
            Gift testGift = new Gift(id, null, description, url, testUser);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullDescription_Gift_ThrowsArgumentNullException()
        {
            //Arrange

            int id = 1;
            string title = "RandomGift title";
            string description = "A random Gift description";
            string url = "AGiftUrl";
            User testUser = new User(123, "First", "Last", null);

            //Act
            Gift testGift = new Gift(id, title, null, url, testUser);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullUrl_Gift_ThrowsArgumentNullException()
        {
            //Arrange

            int id = 1;
            string title = "RandomGift title";
            string description = "A random Gift description";
            string url = "AGiftUrl";
            User testUser = new User(123, "First", "Last", null);

            //Act
            Gift testGift = new Gift(id, title, description, null, testUser);

            //Assert
        }

        
    }
}
