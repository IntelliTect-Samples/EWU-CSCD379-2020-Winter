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
        public async Task CreateGift_ShouldInDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<Gift> service = new GiftService(dbContext, Mapper);
            User inigo = SampleData.CreateInigoMontoya();
            Gift gift = new Gift("Sample gift", "www.sample.com", "Sample description", inigo);

            await service.InsertAsync(gift);

            // Act


            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(inigo.Id);
            Assert.AreSame(gift.User, inigo);
            Assert.AreEqual(gift.User.Id, inigo.Id);
        }

        [TestMethod]
        public async Task FetchGiftUsingId_IncludesUser()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<Gift> service = new GiftService(dbContext, Mapper);
            User inigo = SampleData.CreateInigoMontoya();
            Gift gift = new Gift("Sample gift", "www.sample.com", "Sample description", inigo);

            await service.InsertAsync(gift);

            // Act
            using var dbContextAssert = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            gift = await service.FetchByIdAsync(gift.Id);

            // Assert
            Assert.IsNotNull(gift.User);
        }
    }
}
