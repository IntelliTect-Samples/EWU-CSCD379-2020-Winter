using SecretSanta.Data;
using static SecretSanta.Data.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_User_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            var group = CreateGroup_Cast();

            // Act
            await service.InsertAsync(group);

            // Assert
            Assert.IsNotNull(group.Id);
        }
    }
}