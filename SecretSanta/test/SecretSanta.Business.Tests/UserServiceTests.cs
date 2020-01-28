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
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<User> service = new UserService(dbContext, Mapper);
            User inigo = SampleData.CreateInigoMontoya();
            User jerett = SampleData.CreateJerettLatimer();

            // Act
            await service.InsertAsync(inigo);
            await service.InsertAsync(jerett);

            // Assert
            Assert.IsNotNull(inigo.Id);
            Assert.IsNotNull(jerett.Id);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<User> service = new UserService(dbContext, Mapper);
            User inigo = SampleData.CreateInigoMontoya();
            User jerett = SampleData.CreateJerettLatimer();

            await service.InsertAsync(inigo);
            await service.InsertAsync(jerett);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            User inigoFromDb = await dbContext.Users.SingleAsync(item => item.Id == inigo.Id);
            const string montoyaNewLast = "Montoya III";
            inigoFromDb.LastName = montoyaNewLast;

            await service.UpdateAsync(jerett.Id, inigoFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            inigoFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == inigo.Id);
            var jerettFromDb = await dbContextAssert.Users.SingleAsync(item => item.Id == 2);

            Assert.AreEqual((SampleData.Inigo, montoyaNewLast), (jerettFromDb.FirstName, jerettFromDb.LastName));
        }
    }
}
