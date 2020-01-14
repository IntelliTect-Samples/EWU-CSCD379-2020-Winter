using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Constructor_ValidInput_Success()
        {
            // Arrange
            User user = new User(0, "First", "Last", new List<Gift>());
            Gift gift = new Gift(0, "Title", "Desc", "Url", user);

            // Act

            // Assert
            Assert.AreEqual(0, gift.Id);
            Assert.AreEqual("Title", gift.Title);
            Assert.AreEqual("Desc", gift.Description);
            Assert.AreEqual("Url", gift.Url);
            Assert.AreEqual(user, gift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullTitle_ExceptionThrown()
        {
            new Gift(0, null!, "Desc", "Url", new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyTitle_ExceptionThrown()
        {
            new Gift(0, "", "Desc", "Url", new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullDescription_ExceptionThrown()
        {
            new Gift(0, "Title", null!, "Url", new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyDescription_ExceptionThrown()
        {
            new Gift(0, "Title", "", "Url", new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullUrl_ExceptionThrown()
        {
            new Gift(0, "Title", "Desc", null!, new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyUrl_ExceptionThrown()
        {
            new Gift(0, "Title", "Desc", "", new User(0, "First", "Last", new List<Gift>()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullUser_ExceptionThrown()
        {
            new Gift(0, "Title", "Desc", "Url", null!);
        }
    }
}
