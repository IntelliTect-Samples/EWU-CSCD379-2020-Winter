using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Create_Gift_Success()
        {
            // Arrange
            int id = 1;
            string title = "Sample Title";
            string description = "test description";
            string url = "test.com";
            User testUser = new User(1, "Jerett", "Latimer");

            // Act
            Gift sut = new Gift(id, title, description, url, testUser);

            // Assert
            Assert.IsNotNull(sut);
        }

        [DataTestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(1, null, "description", "url")]
        [DataRow(1, "title", null, "url")]
        [DataRow(1, "title", "description", null)]
        public void Create_PropertiesAreNull_ThrowsException(int id, string title, string description, string url)
        {
            // Arrange
            User testUser = new User(1, "Jerett", "Latimer");

            // Act
            Gift sut = new Gift(id, title, description, url, testUser);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_UserIsNull_ThrowsException()
        {
            // Arrange
            int id = 1;
            string title = "Sample Title";
            string description = "test description";
            string url = "test.com";

            // Act
            Gift sut = new Gift(id, title, description, url, null);

            // Assert
        }
    }
}
