using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public void Gift_CanBeCreate_AllPropertiesGetSet()
        {
            // Arrange
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

            // Act

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
            _ = new Gift
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
            _ = new Gift
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
            _ = new Gift
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

        [TestMethod]
        public async Task AddGift_WithUser_ShouldCreateForeignRelationship()
        {
            var user = new User
            {
                FirstName = "TestFirst",
                LastName = "TestLast"
            };
            var gift = new Gift
            {
                Title = "TestTitle",
                Description = "TestDescription",
                Url = "TestUrl",
                User = user
            };
            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();
                Assert.AreEqual<int>(1, gifts.Count);
                Assert.AreEqual<string>(gift.Title, gifts[0].Title);
                Assert.IsNotNull(gifts[0].User);
                Assert.AreEqual<string>(gift.User.FirstName, gifts[0].User.FirstName);
            }
        }
    }
}
