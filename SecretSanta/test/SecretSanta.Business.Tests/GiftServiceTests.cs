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
        public async Task InsertAsync_Success()
        {
            using var dbContextInsert = new ApplicationDbContext(Options);
            var service = new GiftService(dbContextInsert, Mapper);

            Gift gift = SampleData.Gift1;

            await service.InsertAsync(gift);

            Assert.IsNotNull(gift.Id);
        }

        [TestMethod]
        public async Task FetchByIdPost_ShouldIncludeAuthor()
        {
            User user = SampleData.User3;
            Gift gift = SampleData.Gift2User(user);

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var service = new GiftService(dbContext, Mapper);
                await service.InsertAsync(gift);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                var service = new GiftService(dbContext, Mapper);
                Gift returned = await service.FetchByIdAsync(gift.Id!.Value);

                Assert.IsNotNull(returned.User);
            }
        }
    }
}
