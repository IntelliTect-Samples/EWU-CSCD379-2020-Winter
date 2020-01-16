using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
        public void Gift_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange & Act
            Gift gift = new Gift
            {
                Id = 1,
                Title = "Ring 2",
                Description = "Amazing way to keep the creepers away",
                Url = "www.ring.com",
                User = new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                }
            };

            // Assert
            Assert.AreEqual(1, gift.Id);
            Assert.AreEqual("Ring 2", gift.Title);
            Assert.AreEqual("Amazing way to keep the creepers away", gift.Description);
            Assert.AreEqual("www.ring.com", gift.Url);
            Assert.IsNotNull(gift.User);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            new Gift
            {
                Id = 1,
                Title = null!,
                Description = "Amazing way to keep the creepers away",
                Url = "www.ring.com",
                User = new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            new Gift
            {
                Id = 1,
                Title = "Ring 2",
                Description = null!,
                Url = "www.ring.com",
                User = new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            new Gift
            {
                Id = 1,
                Title = "Ring 2",
                Description = "Amazing way to keep the creepers away",
                Url = null!,
                User = new User
                {
                    Id = 1,
                    FirstName = "Inigo",
                    LastName = "Montoya",
                    Gifts = new List<Gift>()
                }
            };
        }
    }
}
