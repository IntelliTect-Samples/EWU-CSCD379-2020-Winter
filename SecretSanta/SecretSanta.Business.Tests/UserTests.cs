using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Create_User_Success()
        {
            // Arrange
            int id = 0;
            string firstName = "Jerett";
            string lastName = "Latimer";

            // Act
            User sut = new User(id, firstName, lastName);

            // Assert
            Assert.IsNotNull(sut);
        }

        [DataTestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [DataRow(null, "Latimer")]
        [DataRow("Jerett", null)]
        public void Create_NamesAreNull_ThrowsException(string firstName, string lastName)
        {
            // Arrange
            int id = 0;

            // Act
            User sut = new User(id, firstName, lastName);

            // Assert
        }
    }
}
