using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        private readonly Gift _Gift = new Gift
        {
            Title = "title",
            Url = "url",
            Description = "description",
            User = new User()
        };

        private const string _UserName = "caleb";

        [TestMethod]
        public async Task AddGift_WithUser_ShouldCreateForeignRelationship()
        {
            var gift = new Gift
            {
                Title = "My Title",
                Description = "description",
                
            };
            var user = new User
            {
                FirstName = "Billy",
                LastName = "Bob",
               
            };
            // Arrange
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                gift.User = user;

                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(p => p.User).ToListAsync();
                //var gifts = await dbContext.Posts.ToListAsync();
                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.AreNotEqual(0, gifts[0].Id);
                //Assert.IsNotNull(gifts[0].Author);
            }
        }

        [TestMethod]
        public async Task CreateGift_ShouldSaveIntoDatabase()
        {
            int giftId = -1;
            // Arrange
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var gift = new Gift();
                
                applicationDbContext.Gifts.Add(gift);

                var gift2 = new Gift();
                applicationDbContext.Gifts.Add(gift2);

                await applicationDbContext.SaveChangesAsync();

                giftId = gift.Id;
            }

            // Act
            // Assert
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var gift = await applicationDbContext.Gifts.Where(g => g.Id == giftId).SingleOrDefaultAsync();

                Assert.IsNotNull(gift);
                Assert.AreEqual(_Gift.Title, gift.Title);
                Assert.AreEqual(_Gift.Description, gift.Description);
                Assert.AreEqual(_Gift.Url, gift.Url);
            }
        }

        [TestMethod]
        public async Task CreateGift_ShouldSetFingerPrintDataOnInitialSave()
        {
            var httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, _UserName));
            //arrange
            int giftId = -1;
            
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                applicationDbContext.Gifts.Add(_Gift);
                await applicationDbContext.SaveChangesAsync();

                giftId = _Gift.Id;
            }

            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var gift = await applicationDbContext.Gifts.Where(g => g.Id == giftId).SingleOrDefaultAsync();

                Assert.IsNotNull(gift);
                Assert.AreEqual(_UserName, gift.CreatedBy);
                Assert.AreEqual(_UserName, gift.ModifiedBy);

            }
        }

    }
}
