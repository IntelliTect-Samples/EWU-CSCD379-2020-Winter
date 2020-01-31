using Microsoft.EntityFrameworkCore;
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
        public async Task CreateGift_CylonDetector_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGiftService service = new GiftService(dbContextInsert, Mapper);

            Gift cylonDetector = SampleData.CreateGiftCylonDetector();
            cylonDetector.User = SampleData.CreateUserKaraThrace();

            // Act
            await service.InsertAsync(cylonDetector);

            // Assert
            Assert.IsNotNull(cylonDetector.Id);
        }

        

        [TestMethod]
        public async Task UpdateGift_SaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGiftService service = new GiftService(dbContextInsert, Mapper);

            var cylonDetector = SampleData.CreateGiftCylonDetector();
            cylonDetector.User = SampleData.CreateUserKaraThrace();
            var viper = SampleData.CreateGiftViper();
            viper.User = SampleData.CreateUserKaraThrace();

            cylonDetector = await service.InsertAsync(cylonDetector);
            viper = await service.InsertAsync(viper);

            // Act

            const string cylonDetectorV2 = "Cylon Detector V2";
            cylonDetector.Description = cylonDetectorV2;

            // Update Inigo Montoya using the princesses Id.
            await service.UpdateAsync(cylonDetector.Id, cylonDetector);
            

            using var dbContextFetch = new ApplicationDbContext(Options);
            Gift cylonDetectorFromDb = await dbContextFetch.Gifts.SingleAsync(item => item.Id == cylonDetector.Id);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);

            cylonDetectorFromDb = await dbContextAssert.Gifts.SingleAsync(item => item.Id == cylonDetector.Id);
            var viperFromDb = await dbContextAssert.Gifts.SingleAsync(item => item.Id == viper.Id);

            Assert.AreEqual(
                (SampleData.CreateGiftCylonDetector().Title, cylonDetectorV2), 
                (cylonDetectorFromDb.Title, cylonDetectorFromDb.Description));
        }

        [TestMethod]
        public async Task DeleteGift_FTLDrive_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGiftService service = new GiftService(dbContextInsert, Mapper);

            Gift ftlDrive = SampleData.CreateGiftFTLDrive();
            ftlDrive.User = SampleData.CreateUserKaraThrace();

            // Act
            await service.InsertAsync(ftlDrive);
            Assert.IsNotNull(ftlDrive.Id);
            await service.DeleteAsync(ftlDrive.Id);

            // Assert
            List<Gift> gifts = await service.FetchAllAsync();

            Assert.AreEqual(0, gifts.Count);
        }
    }
}
