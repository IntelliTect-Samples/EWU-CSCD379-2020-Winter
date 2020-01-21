using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public void GiftCreate_ValidData_ValidData()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Inigo",
                LastName = "Montoya",
                Gifts = new List<Gift>()
            };
            Gift gift = new Gift
            {
                Id = 1, 
                Title = "Ring 2", 
                Description = "Amazing way to keep the creepers away", 
                Url = "www.ring.com", 
                User = user
            };

            // Act

            // Assert
            Assert.AreEqual(1, gift.Id);
            Assert.AreEqual("Ring 2", gift.Title);
            Assert.AreEqual("Amazing way to keep the creepers away", gift.Description);
            Assert.AreEqual("www.ring.com", gift.Url);
            Assert.IsNotNull(gift.User);
        }

        [DataTestMethod]
        [DataRow(null!, "description", "url.com")]
        [DataRow("title", null!, "url.com")]
        [DataRow("title", "description", null!)]
        [ExpectedException(typeof(ArgumentNullException))]
        [ExcludeFromCodeCoverage]
        public void GiftCreate_NullData_ThrowsException(string title, string description, string url)
        {
            User user = new User
            {
                Id = 1,
                FirstName = "David",
                LastName = "Sergio",
                Gifts = new List<Gift>()
            };
            _ = new Gift
            {
                Id = 1,
                Title = title,
                Description = description,
                Url = url,
                User = user
            };
        }

        [TestMethod]
        public async Task GiftCreate_SaveToDatabase_GiftInserted()
        {
            // Arrange
            int giftId = -1;
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                User user = new User
                {
                    FirstName = "David",
                    LastName = "Sergio",
                    Gifts = new List<Gift>()
                };
                applicationDbContext.Users?.Add(user);

                Gift gift = new Gift
                {
                    Title = "SBUX gift card",
                    Description = "coffee",
                    Url = "starbucks.com",
                    User = user
                };
                applicationDbContext.Gifts?.Add(gift);

                await applicationDbContext.SaveChangesAsync();
                giftId = gift.Id;
            }

            // Act

            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options, HttpContextAccessor))
            {
                var gift = await applicationDbContext.Gifts.Where(i => i.Id == giftId).SingleOrDefaultAsync();

                Assert.IsNotNull(gift);
                Assert.AreEqual("SBUX gift card", gift.Title);
            }
        }
    }
}
