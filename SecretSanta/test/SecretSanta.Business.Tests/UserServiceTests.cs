using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            using var dbContextInsert = new ApplicationDbContext(Options);
            var service = new UserService(dbContextInsert, Mapper);

            User user = SampleData.User1;

            await service.InsertAsync(user);

            Assert.IsNotNull(user.Id);
        }
    }
}
