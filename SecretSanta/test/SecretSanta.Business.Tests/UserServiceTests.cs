using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        [TestMethod]
        public async Task User_InsertAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();

            // Act
            await service.InsertAsync(user);

            // Assert
            Assert.AreNotEqual<int>(0, user.Id);
        }

        [TestMethod]
        public async Task User_InsertAsyncUsingMultipleUsers_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();
            var user2 = SampleData.CreatePrincessButtercup();

            // Act
            await service.InsertAsync(user, user2);

            // Assert
            Assert.AreNotEqual<int>(0, user.Id);
            Assert.AreNotEqual<int>(0, user2.Id);
        }

        [TestMethod]
        public async Task User_FetchByIdAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();

            await service.InsertAsync(user);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new UserService(dbContext2, Mapper);
            user = await service.FetchByIdAsync(user.Id);

            // Assert
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task User_FetchAllAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();
            var user2 = SampleData.CreatePrincessButtercup();

            await service.InsertAsync(user, user2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new UserService(dbContext2, Mapper);
            List<User> list = await service.FetchAllAsync();

            // Assert
            Assert.AreEqual<int>(2, list.Count);
            Assert.AreEqual<string>(SampleData.Inigo, list[0].FirstName);
            Assert.AreEqual<string>(SampleData.Princess, list[1].FirstName);
        }

        [TestMethod]
        public async Task User_UpdateAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();
            var user2 = SampleData.CreatePrincessButtercup();

            await service.InsertAsync(user);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new UserService(dbContext2, Mapper);
            await service.UpdateAsync(user.Id, user2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new UserService(dbContext3, Mapper);
            var userAssert = await service.FetchByIdAsync(user.Id);

            Assert.AreEqual<int>(1, userAssert.Id);
            Assert.AreEqual<string>(SampleData.Princess, userAssert.FirstName);
        }

        [TestMethod]
        public async Task User_DeleteAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IUserService service = new UserService(dbContext, Mapper);

            var user = SampleData.CreateInigoMontoya();
            var user2 = SampleData.CreatePrincessButtercup();

            await service.InsertAsync(user, user2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new UserService(dbContext2, Mapper);
            await service.DeleteAsync(2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new UserService(dbContext3, Mapper);
            List<User> list = await service.FetchAllAsync();
            Assert.AreEqual<int>(1, list.Count);
        }
    }
}
