using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Create_ValidData_Stub()
        {
            // Arrange
            int giftId = 1;
            string title = "";
            string description = "";
            string url = "";

            int userId = 1;
            string firstName = "";
            string lastName = "";
            List<Gift> gifts = new List<Gift>();

            // Act
            User user = new User(userId, firstName, lastName, gifts);
            Gift gift = new Gift(giftId, title, description, url, user);

            // Assert
            Assert.AreEqual<string>(title, gift.Title);
        }
    }
}
