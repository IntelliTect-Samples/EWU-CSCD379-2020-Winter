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
                dbContext.Gifts.Add(SampleData.CreateSampleGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.GiftTitle, gifts[0].Title);
                Assert.AreEqual(SampleData.GiftUrl, gifts[0].Url);
                Assert.AreEqual(SampleData.GiftDesc, gifts[0].Description);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            var gift = SampleData.CreateSampleGift();
            gift.Title = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            var gift = SampleData.CreateSampleGift();
            gift.Description = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            var gift = SampleData.CreateSampleGift();
            gift.Url = null!;
        }

        [TestMethod]
        public async Task Create_SaveGift_Success()
        {
            // Arrange
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            dbContext.Gifts.Add(gift);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            List<Gift> gifts = await dbContext.Gifts.ToListAsync();

            // Assert
            Assert.AreEqual(1, gifts.Count);
            Assert.IsNotNull(gifts[0].User);
            Assert.AreEqual(user, gifts[0].User);
            Assert.AreEqual(user.Id, gifts[0].User.Id);
            Assert.AreEqual(gift.Title, gifts[0].Title);
            Assert.AreEqual(gift.Description, gifts[0].Description);
            Assert.AreEqual(gift.Url, gifts[0].Url);
        }

        [TestMethod]
        public async Task UpdateAndRead_ChangeTitle_Success()
        {
            // Arrange
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            dbContext.Gifts.Add(gift);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            Gift gift2 = await dbContext.Gifts.SingleOrDefaultAsync();
            gift2.Title = "New Title";
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            Gift gift3 = await dbContext.Gifts.SingleOrDefaultAsync();

            // Assert
            Assert.AreEqual("New Title", gift3.Title);
            Assert.AreEqual(gift2.Id, gift3.Id);
        }

        [TestMethod]
        public async Task Delete_DeleteGift_Success()
        {
            // Arrange
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            dbContext.Gifts.Add(gift);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            dbContext.Gifts.Remove(gift);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            List<Gift> gifts = await dbContext.Gifts.ToListAsync();

            // Assert
            Assert.AreEqual(0, gifts.Count);
        }
    }
}
