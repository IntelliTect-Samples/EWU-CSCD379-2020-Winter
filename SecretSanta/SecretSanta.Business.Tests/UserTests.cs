using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        private readonly List<Gift> _TestGifts = new List<Gift> { new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(2, "Eugene", "Krabs", new List<Gift>())) };
        [TestMethod]
        public void Constructor_ValidParameters_Success()
        {
            //Arrange
            User user = new User(1, "TestFirstName", "TestLastName", _TestGifts);
            //Act
            //Assert
            Assert.AreEqual<int>(1, user.Id);
            Assert.AreEqual<string>("TestFirstName", user.FirstName);
            Assert.AreEqual<string>("TestLastName", user.LastName);
            Assert.AreEqual<int>(1, user.Gifts[0].Id);//No need to test more than gift id, not testing gift constructor here
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFirstName_ThrowsException()
        {
            //Arrange
            _ = new User(1, null!, "test", _TestGifts);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLastName_ThrowsException()
        {
            //Arrange
            _ = new User(1, "test", null!, _TestGifts);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullGifts_ThrowsException()
        {
            //Arrange
            _ = new User(1, "test", "test", null!);
            //Act
            //Assert
        }

        [TestMethod]
        public void AddGifts_ReadOnly_Success()
        {
            //Arrange
            User user = new User(1, "test", "test", _TestGifts);
            //Act
            user.Gifts.Add(new Gift(2, "testgift2", "testDesc2", "testUrl2", new User(2, "Spongebob", "Squarepants", new List<Gift>())));
            //Assert
            Assert.AreEqual<int>(2, user.Gifts.Count);
        }
    }
}
