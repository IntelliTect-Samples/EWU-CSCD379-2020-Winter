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
        public async Task Gift_AddGiftWithUser_ShouldCreateForeignRelationship()
        {
            // Arrange
            var gift = new Gift
            {
                Title = "Test Gift",
                Description = "Test Description",
                Url = "www.TestUrl.com"
            };

            var user = new User
            {
                FirstName = "Jerett",
                LastName = "Latimer",
            };

            // Act
            using(ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                gift.User = user;
                applicationDbContext.Gifts.Add(gift);
                await applicationDbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(Options))
            {
                var gifts = await applicationDbContext.Gifts.Include(g => g.User).ToListAsync();
                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.AreNotEqual(0, gifts[0].Id);
            }
        }
    }
}
