using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        public List<Gift> TestGifts = new List<Gift> { new Gift(1, "TestTitle", "TestDescription", "TestUrl", new User(2, "Eugene", "Krabs", new List<Gift>())) };
        [TestMethod]
        public void Constructor_ValidParameters_Success()
        {
            //Arrange
            User user = new User(1, "TestFirstName", "TestLastName", TestGifts);
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
            User user = new User(1, null, "test", TestGifts);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLastName_ThrowsException()
        {
            //Arrange
            User user = new User(1, "test", null, TestGifts);
            //Act
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullGifts_ThrowsException()
        {
            //Arrange
            User user = new User(1, "test", "test", null);
            //Act
            //Assert
        }
    }
}
