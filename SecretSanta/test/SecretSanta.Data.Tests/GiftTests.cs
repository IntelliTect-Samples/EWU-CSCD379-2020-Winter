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
                dbContext.Gifts.Add(SampleData.CreateChiliGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(SampleData.ChiliTitle, gifts[0].Title);
                Assert.AreEqual(SampleData.ChiliUrl, gifts[0].Url);
                Assert.AreEqual(SampleData.ChiliDesc, gifts[0].Description);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(null, "desc", "url", new User("random", "user"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("title", null, "url", new User("random", "user"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("title", "desc", null, new User("random", "user"));
        }

        [TestMethod]
        public async Task Gift_UpdateData_UpdatesSuccessfully()
        {
            await using (var context = new ApplicationDbContext(Options))
            {
                context.Add(SampleData.CreateTeapotGift());
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            // Update Teapot Data
            await using (var context = new ApplicationDbContext(Options))
            {
                var gift = await context.Gifts.Include(g => g.User).SingleOrDefaultAsync();
                Assert.AreEqual("Teapot", gift.Title);
                Assert.AreEqual("It comes with bonus gifts!", gift.Description);
                Assert.AreEqual("Jim", gift.User.FirstName);

                // Update
                gift.Title       = "Cyan Teapot";
                gift.Description = "Getting more specific, now!";
                gift.User        = new User("firstname", "lastname");
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            // Check that update succeeded
            await using (var context = new ApplicationDbContext(Options))
            {
                var gift = await context.Gifts.Include(g => g.User).SingleOrDefaultAsync();
                Assert.AreEqual("Cyan Teapot", gift.Title);
                Assert.AreEqual("Getting more specific, now!", gift.Description);
                Assert.AreEqual("firstname", gift.User.FirstName);
                Assert.AreEqual("lastname", gift.User.LastName);
            }
        }

        [TestMethod]
        public async Task DeleteGift_RunsSuccessfully()
        {
            await using (var context = new ApplicationDbContext(Options))
            {
                context.Add(SampleData.CreateTeapotGift());
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            await using (var context = new ApplicationDbContext(Options))
            {
                var gift = await context.Gifts.SingleOrDefaultAsync();
                context.Gifts.Remove(gift);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            await using (var context = new ApplicationDbContext(Options))
            {
                var gifts = await context.Gifts.ToListAsync();
                Assert.AreEqual(0, gifts.Count);
            }
        }

    }

}
