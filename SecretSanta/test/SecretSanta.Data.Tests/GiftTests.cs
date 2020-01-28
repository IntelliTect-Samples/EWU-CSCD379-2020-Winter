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
                dbContext.Gifts.Add(SampleData.CreateMrKrabsMoney);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.Money, gifts[0].Title);
                Assert.AreEqual(SampleData.MoneyUrl, gifts[0].Url);
                Assert.AreEqual(SampleData.MoneyDescription, gifts[0].Description);
            }
        }

        [TestMethod]
        public async Task AddGift_WithUser_CreatesRelationship()
        {
            //Arrange
            //Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateSpongebobsSpatula);
                await dbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                Gift gift = await dbContext.Gifts.Include(g => g.User).FirstOrDefaultAsync();
                Assert.IsNotNull(gift.User);
                Assert.AreEqual<string>(SampleData.Spongebob, gift.User.FirstName);
                Assert.IsTrue(gift.User.Id != 0);
            }
        }

        [TestMethod]
        public async Task AddGift_ValidGift_HasFingerprintData()
        {
            //Arrange
            //Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateMrKrabsMoney);
                await dbContext.SaveChangesAsync();
            }
            //Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                Gift gift = await dbContext.Gifts.FirstOrDefaultAsync(); //dont need to do check for null gift or anything like that because we already tested adding gift to database
                Assert.IsNotNull(gift.CreatedBy);
                Assert.IsNotNull(gift.CreatedOn);
                Assert.IsNotNull(gift.ModifiedBy);
                Assert.IsNotNull(gift.ModifiedOn);
                Assert.IsTrue(gift.Id != 0);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateSpongebobsSpatula.Title = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateSpongebobsSpatula.Description = null!;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            SampleData.CreateSpongebobsSpatula.Url = null!;
        }
    }
}
