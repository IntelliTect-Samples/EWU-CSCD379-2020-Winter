using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Constructor_ValidInput_Success()
        {
            // Arrange
            List<Gift> gifts = new List<Gift>();
            User user = new User(0, "First", "Last", gifts);

            // Act

            // Assert
            Assert.AreEqual(0, user.Id);
            Assert.AreEqual("First", user.FirstName);
            Assert.AreEqual("Last", user.LastName);
            Assert.AreEqual(gifts, user.Gifts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFirst_ExceptionThrown()
        {
            new User(0, null!, "Last", new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyFirst_ExceptionThrown()
        {
            new User(0, "", "Last", new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullLast_ExceptionThrown()
        {
            new User(0, "First", null!, new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyLast_ExceptionThrown()
        {
            new User(0, "First", "", new List<Gift>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullGifts_ExceptionThrown()
        {
            new User(0, "First", "Last", null!);
        }
    }
}
