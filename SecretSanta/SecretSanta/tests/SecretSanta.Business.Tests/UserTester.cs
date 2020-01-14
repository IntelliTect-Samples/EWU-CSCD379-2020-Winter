using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTester
    {
        [TestMethod]
        public void Create_User_Success()
        {
            //Arrange
            int id = 123;
            string firstName = "RandomFirst";
            string lastName = "RandomLast";
            List<Gift> gifts = new List<Gift>();

            //Act

            User testUser = new User(id, firstName, lastName, gifts);

            //Assert

            Assert.AreEqual(testUser.Id, id);
            Assert.AreEqual(testUser.FirstName, firstName);
            Assert.AreEqual(testUser.LastName, lastName);
            Assert.IsNotNull(testUser.GiftList);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFirstName_User_ThrowsArgumentNullException()
        {
            //Arrange
            int id = 123;
            string firstName = null;
            string lastName = "RandomLast";
            List<Gift> gifts = new List<Gift>();

            //Act

            User testUser = new User(id, firstName, lastName, gifts);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullLastName_User_ThrowsArgumentNullException()
        {
            //Arrange
            int id = 123;
            string firstName = "RandomFirst";
            string lastName = null;
            List<Gift> gifts = new List<Gift>();

            //Act

            User testUser = new User(id, firstName, lastName, gifts);

            //Assert
        }
    }
}
