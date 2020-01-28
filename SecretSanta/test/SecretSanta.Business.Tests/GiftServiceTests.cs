using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task FetchAllAsync_TwoGroups_Success()
        {
            //Arrange
            Gift gift1 = SampleData.CreateSpongebobsSpatula;
            Gift gift2 = SampleData.CreateMrKrabsMoney;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(gift1);
            dbContext.Gifts.Add(gift2);
            await dbContext.SaveChangesAsync();
            GiftService giftService = new GiftService(dbContext, _Mapper);
            //Act
            List<Gift> gifts = giftService.FetchAllAsync().Result;
            //Assert
            Assert.AreEqual<string>(gift1.Title, gifts.ElementAt(0).Title);
        }

        [TestMethod]
        public async Task FetchByIdAsync_OneGift_Success()
        {
            //Arrange
            Gift gift = SampleData.CreateMrKrabsMoney;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(gift);
            await dbContext.SaveChangesAsync();
            GiftService giftService = new GiftService(dbContext, _Mapper);
            //Act
            Gift result = giftService.FetchByIdAsync(1).Result;
            //Assert
            Assert.AreEqual<string>(gift.Title, result.Title);
        }

        [TestMethod]
        public async Task InsertAsync_OneGift_Success()
        {
            //Arrange
            Gift gift = SampleData.CreateMrKrabsMoney;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GiftService giftService = new GiftService(dbContext, _Mapper);
            //Act
            await giftService.InsertAsync(gift);
            //Assert
            Gift result = await dbContext.Gifts.FirstOrDefaultAsync();
            Assert.AreEqual<string>(gift.Title, result.Title);
        }

        [TestMethod]
        public async Task UpdateAsync_OneGift_Success()
        {
            //Arrange
            Gift gift1 = SampleData.CreateMrKrabsMoney;
            Gift gift2 = SampleData.CreateSpongebobsSpatula;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Gifts.Add(gift1);
            dbContext.Gifts.Add(gift2);//have to add gift2 here so its fingerprint properties get set, otherwise Sqlite rejects update becasue fingerpint properties are null
            await dbContext.SaveChangesAsync();
            GiftService giftService = new GiftService(dbContext, _Mapper);
            //Act
            await giftService.UpdateAsync(1, gift2);
            //Assert
            Gift result = await dbContext.Gifts.FirstOrDefaultAsync();
            Assert.AreEqual(gift2.Title, result.Title);
        }
    }
}
