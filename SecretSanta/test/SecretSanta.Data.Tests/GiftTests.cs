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
                dbContext.Gifts.Add(SampleData.CreateRingGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.RingTitle, gifts[0].Title);
                Assert.AreEqual(SampleData.RingUrl, gifts[0].Url);
                Assert.AreEqual(SampleData.RingDescription, gifts[0].Description);
            }
        }

        [TestMethod]
        public async Task Gift_Add_ShouldHaveFingerprintData()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateRingGift());
                await dbContext.SaveChangesAsync();
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();

                Assert.IsNotNull(gifts[0].CreatedBy);
                Assert.IsNotNull(gifts[0].ModifiedBy);
                Assert.AreNotEqual(new DateTime(), gifts[0].CreatedOn);
                Assert.AreNotEqual(new DateTime(), gifts[0].ModifiedOn);
                Assert.AreEqual(1, gifts[0].Id);
            }
        }

        [TestMethod]
        public async Task Gift_AddWithUser_ShouldCreateForeignRelaitonship()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateRingGift());
                await dbContext.SaveChangesAsync();
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.RingTitle, gifts[0].Title);
                Assert.AreEqual(SampleData.RingUrl, gifts[0].Url);
                Assert.AreEqual(SampleData.RingDescription, gifts[0].Description);
                Assert.AreEqual(1, gifts[0].Id);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(null!, SampleData.RingUrl, SampleData.RingDescription, SampleData.CreateInigoMontoya());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(SampleData.ArduinoTitle, SampleData.ArduinoUrl, null!, SampleData.CreateJackRyan());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(SampleData.RingTitle, null!, SampleData.RingDescription, SampleData.CreateJerettLatimer());
        }

        [TestMethod]
        public async Task Gift_UpdateUser_Success()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateRingGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.Include(g => g.User).SingleOrDefaultAsync();

                Assert.AreEqual(SampleData.RingTitle, gift.Title);
                Assert.AreEqual(SampleData.RingUrl, gift.Url);
                Assert.AreEqual(SampleData.RingDescription, gift.Description);
                Assert.AreEqual(1, gift.Id);

                gift.User.FirstName = "Changed Name";

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.Include(g => g.User).SingleOrDefaultAsync();

                Assert.AreEqual("Changed Name", gift.User.FirstName);
            }
        }

        [TestMethod]
        public async Task Gift_DeleteGift_Success()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateRingGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.Include(g => g.User).SingleOrDefaultAsync();
                dbContext.Gifts.Remove(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();

                Assert.AreEqual(0, gifts.Count);
            }
        }
    }
}
