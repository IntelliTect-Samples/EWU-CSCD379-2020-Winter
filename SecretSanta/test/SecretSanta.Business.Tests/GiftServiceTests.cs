using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AutoMapper;
using static SecretSanta.Data.Tests.SampleData;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : GenericEntityServicesTestBase<Gift>
    {
        /* replaced by generic
        [TestMethod]
        public async Task InsertAsync_Gift_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGiftService service = new GiftService(dbContextInsert, Mapper);

            var inigo = CreateUser_InigoMontoya();
            var gift = CreateGift_Junk(inigo);

            // Act
            await service.InsertAsync(gift);

            // Assert
            Assert.IsNotNull(gift.Id);
        }*/

        /* replaced by generic
        [TestMethod]
        public async Task CreateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGiftService service = new GiftService(dbContext, Mapper);

            var inigo = CreateUser_InigoMontoya();

            var gift = CreateGift_Junk(inigo);

            await service.InsertAsync(gift);

            // Act

            // Assert
            Assert.IsNotNull(gift.Id);
            Assert.IsNotNull(inigo.Id);
            Assert.AreSame(gift.User, inigo);
            Assert.AreEqual(inigo.Id, gift.User.Id);
        }*/

        [TestMethod]
        public async Task UpdateGift_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGiftService service = new GiftService(dbContextInsert, Mapper);

            var inigo = CreateUser_InigoMontoya();
            var princess = CreateUser_PrincessButtercup();
            var junk = CreateGift_Junk(inigo);
            var doorbell = CreateGift_Doorbell(princess);

            await service.InsertAsync(junk);
            await service.InsertAsync(doorbell);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            Gift junkFromDb = await dbContextFetch.Gifts.SingleAsync(item => item.Id == junk.Id);

            const string crapTxt = "Pure worthless crap";
            junkFromDb.Description = crapTxt;

            // Update junk using the doorbell Id.
            await service.UpdateAsync(doorbell.Id!.Value, junkFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            junkFromDb = await dbContextAssert.Gifts.SingleAsync(item => item.Id == junk.Id);
            var doorbellFromDb = await dbContextAssert.Gifts.SingleAsync(item => item.Id == 2);

            Assert.AreEqual(
                (GiftJunk_Title, GiftJunk_Url, crapTxt), (doorbellFromDb.Title, doorbellFromDb.Url, doorbellFromDb.Description));

            Assert.AreEqual(
                (GiftJunk_Title, GiftJunk_Url, GiftJunk_Description), (junkFromDb.Title, junkFromDb.Url, junkFromDb.Description));
        }

        protected override IEntityService<Gift> GetService(ApplicationDbContext dbContext, IMapper mapper)
        {
            return new GiftService(dbContext, mapper);
        }

        protected override Gift CreateEntity()
        {
            return CreateGift_Doorbell(CreateUser_InigoMontoya());
        }
    }
}
