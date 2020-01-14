using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        /// <summary>
        /// Extra Credit
        /// </summary>
        [TestMethod]
        public void ReflectionGiftCreate_ValidData_PropertiesNotNull()
        {
            // Arrange
            int giftId = 1;
            string title = "title";
            string description = "description";
            string url = "a url";

            int userId = 1;
            string firstName = "";
            string lastName = "";
            List<Gift> gifts = new List<Gift>();
            User user = new User(userId, firstName, lastName, gifts);

            // Act
            Gift gift = new Gift(giftId, title, description, url, user);

            // Assert
            Type type = gift.GetType();

            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                Console.WriteLine(property.Name + " " + property.PropertyType + " " + property.GetValue(gift));
                Assert.IsNotNull(property.GetValue(gift));

            }
        }

        [TestMethod]
        public void GiftCreate_ValidData_CorrectProperties()
        {
            // Arrange
            int giftId = 1;
            string title = "title";
            string description = "description";
            string url = "a url";

            int userId = 1;
            string firstName = "";
            string lastName = "";
            List<Gift> gifts = new List<Gift>();
            User user = new User(userId, firstName, lastName, gifts);

            // Act
            Gift gift = new Gift(giftId, title, description, url, user);

            // Assert
            Assert.AreEqual<string>(title, gift.Title);
            Assert.AreEqual<string>(description, gift.Description);
            Assert.AreEqual<string>(url, gift.Url);
            Assert.AreEqual<int>(giftId, gift.Id);
            Assert.AreEqual<User>(user, gift.User);
        }

        [DataTestMethod]
        [DataRow(1, null!, "description", "url")]
        [DataRow(1, "title", null!, "url")]
        [DataRow(1, "title", "description", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void GiftCreate_NullData_ThrowsException(int giftId, string title, string description, string url)
        {
            // Arrange
            int userId = 1;
            string firstName = "";
            string lastName = "";
            List<Gift> gifts = new List<Gift>();
            User user = new User(userId, firstName, lastName, gifts);

            // Act
            _ = new Gift(giftId, title, description, url, user);

            // Assert

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void GiftCreate_UserNull_ThrowsException()
        {
            // Arrange
            int giftId = 1;
            string title = "title";
            string description = "description";
            string url = "a url";

            int userId = 1;
            string firstName = "";
            string lastName = "";
            List<Gift> gifts = new List<Gift>();
            _ = new User(userId, firstName, lastName, gifts);

            // Act
            _ = new Gift(giftId, title, description, url, null!);

            // Assert

        }
    }
}
