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
        public async Task Gift_CanBeSavedToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateGift1());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.GiftTitle1, gifts[0].Title);
                Assert.AreEqual(SampleData.GiftDescription1, gifts[0].Description);
                Assert.AreEqual(SampleData.GiftUrl1, gifts[0].Url);
            }
        }

        [TestMethod]
        public async Task Gift_UpdateDescription_Success()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateGift1());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            using (var dbContext = new ApplicationDbContext(Options))
            {
                Gift gift = await dbContext.Gifts.SingleOrDefaultAsync();
                gift.Description = "New Description";
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                Gift gift = await dbContext.Gifts.SingleOrDefaultAsync();

                Assert.AreEqual<string>("New Description", gift.Description);
            }
        }

        [TestMethod]
        public async Task Gift_DeleteGift_Success()
        {
            var gift = SampleData.CreateGift1();
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Remove(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual<int>(0, gifts.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateGift1().Title = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateGift1().Description = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateGift1().Url = null!;
        }
    }
}
