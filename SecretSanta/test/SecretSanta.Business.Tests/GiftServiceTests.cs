using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task Save_InsertGift_GiftHasData()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GiftService service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            await service.InsertAsync(gift);

            // Assert
            Assert.IsNotNull(gift.User);
            Assert.AreEqual(user, gift.User);
            Assert.AreEqual(user.Id, gift.User.Id);
        }

        [TestMethod]
        public async Task Fetch_InsertGift_HasData()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GiftService service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            await service.InsertAsync(gift);
            gift = await service.FetchByIdAsync(gift.Id);

            // Assert
            Assert.IsNotNull(gift.User);
            Assert.AreEqual(user, gift.User);
            Assert.AreEqual(user.Id, gift.User.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public async Task Delete_InsertGift_Exception()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GiftService service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            await service.InsertAsync(gift);
            bool result = await service.DeleteAsync(gift.Id);

            // Assert
            Assert.IsTrue(result);

            gift = await service.FetchByIdAsync(gift.Id);
        }

        [TestMethod]
        public async Task Update_ChangeTitle_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GiftService service = new GiftService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();
            Gift gift = SampleData.CreateSampleGift();
            gift.User = user;

            // Act
            await service.InsertAsync(gift);
            gift.Title = "New Title";
            await service.UpdateAsync(gift.Id, gift);
            Gift gift2 = await service.FetchByIdAsync(gift.Id);

            // Assert
            Assert.AreEqual("New Title", gift2.Title);
        }
    }
}
