using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : TestBase
    {
        [TestMethod]
        public async Task CreateGift_ShouldSaveInDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<Gift> service = new GiftService(dbContext, Mapper);
            User inigoMontoya = SampleData.CreateInigoMontoya();
            Gift gift = SampleData.CreateGift(inigoMontoya);

            await service.InsertAsync(gift);

            // Act


            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(inigoMontoya.Id);
            Assert.AreSame(gift.User, inigoMontoya);
            Assert.AreEqual(gift.User.Id, inigoMontoya.Id);
        }

        [TestMethod]
        public async Task FetchGift_UsingUserId()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<Gift> service = new GiftService(dbContext, Mapper);
            Gift gift = SampleData.CreateGift();

            await service.InsertAsync(gift);

            // Act
            using var applicationDbContext = new ApplicationDbContext(Options);
            service = new GiftService(dbContext, Mapper);
            gift = await service.FetchByIdAsync(gift.UserId);

            // Assert
            Assert.IsNotNull(gift.User);
        }
    }
}