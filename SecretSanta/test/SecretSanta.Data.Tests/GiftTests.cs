using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using static SecretSanta.Data.Tests.SampleData;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        // test null borks
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(null!, "www.ring.com", "nonexistant");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("Door Noise Machine", "www.ring.com", null!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("Door Knocker", null!, "your hand! it makes noise on door");
        }

        // CRUD!
        // CREATE!
        [TestMethod]
        public async Task Gift_CanBeSavedToDatabase()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(CreateGift_Doorbell( CreateUser_InigoMontoya()) );
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(GiftDoorbell_Title, gifts[0].Title);
                Assert.AreEqual(GiftDoorbell_Url, gifts[0].Url);
                Assert.AreEqual(GiftDoorbell_Description, gifts[0].Description);
            }
        }

        // UPDATE and READ
        [TestMethod]
        public async Task Gift_UpdateTitleTest()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(CreateGift_Junk(CreateUser_InigoMontoya()));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            // Act
            var gift = await dbContext.Gifts.SingleOrDefaultAsync();
            gift.Title = "I CHANGED IT!";
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Assert
            gift = await dbContext.Gifts.SingleOrDefaultAsync();
            Assert.AreEqual("I CHANGED IT!", gift.Title);
        }

        [TestMethod]
        public async Task Gift_UpdateUrlTest()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(CreateGift_Junk(CreateUser_InigoMontoya()));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            // Act
            var gift = await dbContext.Gifts.SingleOrDefaultAsync();
            gift.Url = "I CHANGED IT!";
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Assert
            gift = await dbContext.Gifts.SingleOrDefaultAsync();
            Assert.AreEqual("I CHANGED IT!", gift.Url);
        }

        [TestMethod]
        public async Task Gift_UpdateDescriptionTest()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(CreateGift_Junk(CreateUser_InigoMontoya()));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            // Act
            var gift = await dbContext.Gifts.SingleOrDefaultAsync();
            gift.Description = "I CHANGED IT!";
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Assert
            gift = await dbContext.Gifts.SingleOrDefaultAsync();
            Assert.AreEqual("I CHANGED IT!", gift.Description);
        }

        // DELETE
        [TestMethod]
        public async Task Gift_Deleteable()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(CreateGift_Junk(CreateUser_InigoMontoya()));
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Act
            var gift = await dbContext.Gifts.SingleOrDefaultAsync();
            dbContext.Gifts.Remove(gift);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            // Assert
            var gifts = await dbContext.Gifts.ToListAsync();
            Assert.AreEqual(0, gifts.Count);
        }
    }
}
