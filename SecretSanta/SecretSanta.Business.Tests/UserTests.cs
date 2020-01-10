using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
