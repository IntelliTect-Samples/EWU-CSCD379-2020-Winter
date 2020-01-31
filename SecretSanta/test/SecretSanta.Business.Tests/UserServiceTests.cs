using SecretSanta.Data;
using static SecretSanta.Data.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AutoMapper;

namespace SecretSanta.Business.Tests
{
    /* Migrating to generic
        [TestClass]
        public class UserServiceTests : TestBase
        {
            [TestMethod]
            public async Task InsertAsync_User_Success()
            {
                // Arrange
                using var dbContextInsert = new ApplicationDbContext(Options);
                IUserService service = new UserService(dbContextInsert, Mapper);

                var inigo = CreateUser_InigoMontoya();

                // Act
                await service.InsertAsync(inigo);

                // Assert
                Assert.IsNotNull(inigo.Id);
            }
        }
        */
    [TestClass]
    public class UserServiceTests : GenericEntityServicesTestBase<User>
    {
        protected override IEntityService<User> GetService(ApplicationDbContext dbContext, IMapper mapper)
        {
            return new UserService(dbContext, mapper);
        }

        protected override User CreateEntity()
        {
            return CreateUser_InigoMontoya();
        }
    }
}
