using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data.Tests;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateBillyBob();
            Gift gift = SampleData.CreateCrazyGift();

            // Act
            await service.InsertAsync(gift);

            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(user.Id);
            Assert.AreSame(gift.User, user);
            Assert.AreEqual(user.Id, gift.User.Id);
        }

        [TestMethod]
        public async Task FetchAll_ShouldRetrieveAllGifts_Success()
        {
            //Arrange
            using var dbContext = new ApplicationDbContext(Options);
    
            IGiftService service = new GiftService(dbContext, Mapper);

            Gift gift1 = SampleData.CreateCoolGift();
            Gift gift2 = SampleData.CreateCrazyGift();

            await service.InsertAsync(gift1);
            await service.InsertAsync(gift2);

            //Act
            List<Gift> gifts = await service.FetchAllAsync();

            Gift giftFromDb1 = gifts[0];
            Gift giftFromDb2 = gifts[1];

            //Assert
            Assert.AreEqual(gift1, giftFromDb1);
            Assert.AreEqual(gift2, giftFromDb2);
            Assert.IsNotNull(giftFromDb1.User);
            Assert.IsNotNull(giftFromDb2.User);
            Assert.AreEqual((SampleData.Fred, SampleData.Flintstone),
                        (giftFromDb1.User.FirstName, giftFromDb1.User.LastName));
            Assert.AreEqual((SampleData.Billy, SampleData.Bob),
                        (giftFromDb2.User.FirstName, giftFromDb2.User.LastName));
        }

        [TestMethod]
        public async Task FetchByIdGift_ShouldIncludeUser()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateBillyBob();
            var gift = SampleData.CreateCrazyGift();

            // Act
            await service.InsertAsync(gift);

            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            gift = await service.FetchByIdAsync(gift.Id!.Value);

            // Assert
            Assert.IsNotNull(gift.User);
        }

        [TestMethod]
        public async Task UpdateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService giftService = new GiftService(dbContext, Mapper);
            IUserService userService = new UserService(dbContext, Mapper);

            var gift = SampleData.CreateCrazyGift();
            var gift2 = SampleData.CreateCoolGift();

            await giftService.InsertAsync(gift);
            await giftService.InsertAsync(gift2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            Gift fetchGift = await dbContext2.Gifts.SingleAsync(item => item.Id == gift.Id);

            fetchGift.Title = "Title Update";
            fetchGift.Description = "Description Update";

            using var dbContext3 = new ApplicationDbContext(Options);
            var service2 = new GiftService(dbContext3, Mapper);
            await service2.UpdateAsync(2, fetchGift);

            using var dbContext4 = new ApplicationDbContext(Options);
            Gift savedGift = await dbContext4.Gifts.SingleAsync(item => item.Id == gift.Id);
            var otherGift = await dbContext4.Gifts.SingleAsync(item => item.Id == 2);

            // Assert
            Assert.AreEqual(("Crazy Gift", "The craziest gift"), (savedGift.Title, savedGift.Description));
            Assert.AreNotEqual((savedGift.Title, savedGift.Description), (otherGift.Title, otherGift.Description));
        }

        [TestMethod]
        public async Task Delete_SingleGiftOnly_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IGiftService giftService = new GiftService(dbContext, Mapper);

            var gift = SampleData.CreateCrazyGift();
            var gift2 = SampleData.CreateCoolGift();

            await giftService.InsertAsync(gift);
            await giftService.InsertAsync(gift2);

            await dbContext.SaveChangesAsync();

            // Act
            bool deleted = await giftService.DeleteAsync(gift.Id!.Value);
            using var dbContextAssert = new ApplicationDbContext(Options);
            Gift giftFromDb = await dbContextAssert.Set<Gift>().SingleOrDefaultAsync(e => e.Id == gift.Id);
            Gift giftFromDb2 = await dbContextAssert.Set<Gift>().SingleOrDefaultAsync(e => e.Id == gift2.Id);

            // Assert
            Assert.IsTrue(deleted);
            Assert.IsNull(giftFromDb);
            Assert.AreEqual(gift2.Title, giftFromDb2.Title);
            Assert.AreEqual(gift2.Description, giftFromDb2.Description);
            Assert.AreEqual(gift2.Id, giftFromDb2.Id);
        }
    }
}
