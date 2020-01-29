using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            using var dbContextInsert = new ApplicationDbContext(Options);
            var service = new GroupService(dbContextInsert, Mapper);

            Group group = SampleData.Group1;

            await service.InsertAsync(group);

            Assert.IsNotNull(group.Id);
        }
    }
}
