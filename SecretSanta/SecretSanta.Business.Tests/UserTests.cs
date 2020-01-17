using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Create_User_Success()
        {
            // arrange
            int id = 1;
            const string firstName = "first";
            const string lastName = "last";

            // act
            User user = new User(id, firstName, lastName, new List<Gift>());

            // assert
            Assert.AreEqual<int>(id, user.Id, "User Id not equal");
            Assert.AreEqual<string>(firstName, user.FirstName, "firstName not equal");
            Assert.AreEqual<string>(lastName, user.LastName, "lastName not equal");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_FirstNamePropertyNotNull_NotNull()
        {
            new User(1, null!, "last", new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Verify_LastNamePropertyNotNull_NotNull()
        {
            new User(1, "first", null!, new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("", "<lastname>")]
        [DataRow("<firstname>", "")]
        public void Create_PropertiesWhiteSpace_ThrowArgumentException(string fname, string lname)
        {
            // Arrange
            var gifts = new List<Gift>();
            // Act
            _ = new User(
                0,
                fname,
                lname,
                gifts);
            // Assert
            // handled from method attributes
        }
    }
}
