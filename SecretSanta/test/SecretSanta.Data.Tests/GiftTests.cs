using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task Gift_CanBeSavedToDatabase()
        {
            Gift gift = SampleData.Gift1;
            string title = gift.Title;
            string url = gift.Url;
            string desc = gift.Description;

            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(title, gifts[0].Title);
                Assert.AreEqual(url, gifts[0].Url);
                Assert.AreEqual(desc, gifts[0].Description);
            }
        }

        [TestMethod]
        public async Task Gift_HasFingerPrintDataAddedOnInitialSave()
        {
            User user = SampleData.User1;
            string identifier = $"{user.LastName}, {user.FirstName}";

            Gift gift = SampleData.Gift1;

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gifts.Add(gift);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(identifier, gifts[0].CreatedBy);
                Assert.AreEqual(identifier, gifts[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task Gift_HasFingerPrintDataUpdateOnUpdate()
        {
            User user1 = SampleData.User1;
            string identifier1 = $"{user1.LastName}, {user1.FirstName}";

            User user2 = SampleData.User3;
            string identifier2 = $"{user2.LastName}, {user2.FirstName}";

            Gift gift1 = SampleData.Gift1;

            Gift gift2 = SampleData.Gift2;
            string title = gift2.Title;
            string desc = gift2.Description;
            string url = gift2.Url;

            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier1));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gifts.Add(gift1);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, identifier2));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                gifts[0].Title = title;
                gifts[0].Description = desc;
                gifts[0].Url = url;

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(identifier1, gifts[0].CreatedBy);
                Assert.AreEqual(identifier2, gifts[0].ModifiedBy);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(
                null!, "The doorbell that saw too much",
                "www.ring.com", SampleData.User1
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(
                "Ring Doorbell", null!,
                "www.ring.com", SampleData.User2
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(
                "Ring Doorbell", "The doorbell that saw too much",
                null!, SampleData.User3
            );
        }
    }
}
