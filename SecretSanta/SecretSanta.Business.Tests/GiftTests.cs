using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        private readonly User _TestUser = new User(1, "TestFirstName", "TestLastName", new List<Gift>());
        [TestMethod]
        public void Constructor_ValidParameters_Success()
        {
            //Arrange
            Gift gift = new Gift(1, "TestTitle", "TestDescription", "TestUrl", _TestUser);
            //Act
            //Assert
            Assert.AreEqual<int>(1, gift.Id);
            Assert.AreEqual<string>("TestTitle", gift.Title);
            Assert.AreEqual<string>("TestDescription", gift.Description);
            Assert.AreEqual<string>("TestUrl", gift.Url);
            Assert.AreEqual<int>(1, gift.User.Id);//User id uniquely identifies user, no need to test other properties because we are not testing user constructor here
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullTitle_ThrowsException()
        {
            //Arrange
            _ = new Gift(1, null!, "test", "test", _TestUser);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullDescription_ThrowsException()
        {
            //Arrange
            _ = new Gift(1, "test", null!, "test", _TestUser);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullUrl_ThrowsException()
        {
            //Arrange
            _ = new Gift(1, "test", "test", null!, _TestUser);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullUser_ThrowsException()
        {
            //Arrange
            _ = new Gift(1, "test", "test", "test", null!);
            //Act
            //Assert
        }
    }
}
