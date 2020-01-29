using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task Gift_InsertAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();

            var gift = SampleData.CreateGift1();
            gift.User = user;

            // Act
            await service.InsertAsync(gift);

            // Assert
            Assert.AreNotEqual<int>(0, gift.Id);
            Assert.AreNotEqual<int>(0, user.Id);
            Assert.AreSame(gift.User, user);
            Assert.AreEqual<int>(user.Id, gift.User.Id);
        }

        [TestMethod]
        public async Task Gift_InsertAsyncUsingMultipleGifts_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();
            var user2 = SampleData.CreatePrincessButtercup();

            var gift = SampleData.CreateGift1();
            var gift2 = SampleData.CreateGift2();
            gift.User = user;
            gift2.User = user2;

            // Act
            await service.InsertAsync(gift, gift2);

            // Assert
            Assert.AreNotEqual<int>(0, gift.Id);
            Assert.AreNotEqual<int>(0, gift2.Id);
            Assert.AreSame(gift.User, user);
            Assert.AreSame(gift2.User, user2);
            Assert.AreEqual<int>(user.Id, gift.User.Id);
            Assert.AreEqual<int>(user2.Id, gift2.User.Id);
        }

        [TestMethod]
        public async Task Gift_FetchByIdAsync_ShouldIncludeUser()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            Gift gift = SampleData.CreateGift1();

            await service.InsertAsync(gift);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext2, Mapper);
            gift = await service.FetchByIdAsync(gift.Id);

            // Assert
            Assert.IsNotNull(gift.User);
        }

        [TestMethod]
        public async Task Gift_FetchAllAsync_ShouldIncludeUser()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            Gift gift = SampleData.CreateGift1();
            Gift gift2 = SampleData.CreateGift2();

            await service.InsertAsync(gift, gift2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext2, Mapper);
            List<Gift> list = await service.FetchAllAsync();

            // Assert
            Assert.AreEqual<int>(2, list.Count);
            Assert.AreEqual<string>(SampleData.GiftTitle1, list[0].Title);
            Assert.AreEqual<string>(SampleData.GiftTitle2, list[1].Title);
            Assert.IsNotNull(list[0].User);
            Assert.IsNotNull(list[1].User);
        }

        [TestMethod]
        public async Task Gift_UpdateAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();

            var gift = SampleData.CreateGift1();
            var gift2 = SampleData.CreateGift2();
            gift.User = user;
            await service.InsertAsync(gift);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext2, Mapper);
            await service.UpdateAsync(gift.Id, gift2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext3, Mapper);
            var giftAssert = await service.FetchByIdAsync(user.Id);

            Assert.AreEqual<int>(1, giftAssert.Id);
            Assert.AreEqual<string>(SampleData.GiftTitle2, giftAssert.Title);
        }

        [TestMethod]
        public async Task Gift_DeleteAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();

            var gift = SampleData.CreateGift1();
            var gift2 = SampleData.CreateGift2();
            gift.User = user;
            await service.InsertAsync(gift, gift2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext2, Mapper);
            await service.DeleteAsync(2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new GiftService(dbContext3, Mapper);
            List<Gift> list = await service.FetchAllAsync();
            Assert.AreEqual<int>(1, list.Count);
        }
    }
}
