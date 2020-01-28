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
                Assert.AreEqual(SampleData.Title1, gifts[0].Title);
                Assert.AreEqual(SampleData.Url1, gifts[0].Url);
                Assert.AreEqual(SampleData.Desc1, gifts[0].Description);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
            (
                null!, "Some description", "www.apple@microsoft.com", new User("First", "Last")
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
           (
               "A title", null!, "www.apple@microsoft.com", new User("First", "Last")
           );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift
            (
                "A title", "Some description", null!, new User("First", "Last")
            );
        }

        [TestMethod]
        public async Task Gift_CanUpdateStrings()
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
                //Change Title
                var gift = await dbContext.Gifts.SingleOrDefaultAsync();
                Assert.AreEqual(SampleData.Title1, gift.Title);
                Assert.AreEqual(SampleData.Desc1, gift.Description);
                Assert.AreEqual(SampleData.Url1, gift.Url);

                gift.Title = "A Title";
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.SingleOrDefaultAsync();
                Assert.AreEqual("A Title", gift.Title);
            }
        }

        [TestMethod]
        public async Task Gift_CanUpdateUser()
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
                //Change User
                var gift = await dbContext.Gifts.Include(g => g.User).SingleOrDefaultAsync();
                Assert.AreEqual(SampleData.Title1, gift.Title);
                Assert.AreEqual(SampleData.Desc1, gift.Description);
                Assert.AreEqual(SampleData.Url1, gift.Url);
                Assert.AreEqual(SampleData.Inigo, gift.User.FirstName);
                gift.User.FirstName = "A Name";
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.Include(g => g.User).SingleOrDefaultAsync();
                Assert.AreEqual("A Name", gift.User.FirstName);
            }
        }

        [TestMethod]
        public async Task Gift_CanBeDeleted()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateGift1());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);

            }
            //Act
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gift = await dbContext.Gifts.SingleOrDefaultAsync();
                dbContext.Gifts.Remove(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            //Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();
                Assert.AreEqual(0, gifts.Count);
            }
        }
    }
}
    