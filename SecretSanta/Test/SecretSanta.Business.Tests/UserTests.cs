using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// Extra Credit
        /// </summary>
        [TestMethod]
        public void ReflectionUserCreate_ValidData_PropertiesNotNull()
        {
            // Arrange
            int id = 1;
            string firstName = "first";
            string lastName = "last";
            List<Gift> gifts = new List<Gift>();

            // Act
            User user = new User(id, firstName, lastName, gifts);

            // Assert
            Type type = user.GetType();

            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                Console.WriteLine(property.Name + " " + property.PropertyType + " " + property.GetValue(user));
                Assert.IsNotNull(property.GetValue(user));

            }
        }

        [TestMethod]
        public void UserCreate_ValidData_CorrectProperties()
        {
            // Arrange
            int id = 1;
            string firstName = "first";
            string lastName = "last";
            List<Gift> gifts = new List<Gift>();

            // Act
            User user = new User(id, firstName, lastName, gifts);

            // Assert
            Assert.AreEqual<int>(id, user.Id);
            Assert.AreEqual<string>(firstName, user.FirstName);
            Assert.AreEqual<string>(lastName, user.LastName);
            Assert.AreEqual<List<Gift>>(gifts, user.Gifts);
        }

        [DataTestMethod]
        [DataRow(1, null!, "last")]
        [DataRow(1, "first", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void UserCreate_NullData_ThrowsException(int userId, string firstName, string lastName)
        {
            // Arrange
            List<Gift> gifts = new List<Gift>();

            // Act
            _ = new User(userId, firstName, lastName, gifts);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void UserCreate_GiftsNull_ThrowsException()
        {
            // Arrange
            int id = 1;
            string firstName = "first";
            string lastName = "last";

            // Act
            _ = new User(id, firstName, lastName, null!);

            // Assert

        }
    }
}
