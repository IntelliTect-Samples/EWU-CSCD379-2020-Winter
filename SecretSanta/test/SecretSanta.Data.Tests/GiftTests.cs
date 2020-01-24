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
