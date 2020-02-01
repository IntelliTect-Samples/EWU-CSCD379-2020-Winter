using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            // Arrange
            using (var dbContext = new ApplicationDbContext(Options))
            {
                dbContext.Gifts.Add(SampleData.CreateCoolGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            // Assert
            using (var dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual("Free money", gifts[0].Title);
                Assert.AreEqual("The coolest gift", gifts[0].Description);
                Assert.AreEqual("www.url.com", gifts[0].Url);

            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetTitleToNull_ThrowsArgumentNullException()
        {
            _ = new Gift(null!, "Description", "www.website.com", new User("Inigo", "Montoya"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetDescriptionToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("Title", null!, "www.website.com", new User("Inigo", "Montoya"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Gift_SetUrlToNull_ThrowsArgumentNullException()
        {
            _ = new Gift("Title", "Description", null!, new User("Inigo", "Montoya"));
        }


        [TestMethod]
        public async Task Gift_HasFingerPrintDataAddedOnInitialSave()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "caleb"));

            // Act
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gifts.Add(SampleData.CreateCoolGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<User> users = await dbContext.Users.ToListAsync();

                Assert.AreEqual(1, users.Count);
                Assert.AreEqual("caleb", users[0].CreatedBy);
                Assert.AreEqual("caleb", users[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task Gift_HasFingerPrintDataUpdateOnUpdate()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "caleb"));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Gifts.Add(SampleData.CreateCoolGift());
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            // Act
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                    hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "pbuttercup"));

            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<Gift> gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                gifts[0].Title = "Title Update";
                gifts[0].Description = "Description Update";

                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            // Assert
            using (var dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                List<Gift> gifts = await dbContext.Gifts.ToListAsync();

                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual("caleb", gifts[0].CreatedBy);
                Assert.AreEqual("pbuttercup", gifts[0].ModifiedBy);
            }
        }

        [TestMethod]
        public async Task AddGift_WithUser_ShouldCreateForeignRelationship()
        {
            // Arrange
            User user = new User("Caleb", "Walsh");
            Gift gift = SampleData.CreateCoolGift();

            // Act
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                gift.User = user;
                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                List<Gift> gifts = await dbContext.Gifts.Include(p => p.User).ToListAsync();
                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.IsNotNull(gifts[0].User);
                Assert.AreNotEqual(0, gifts[0].User.Id);
            }
        }
    }
}
