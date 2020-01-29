using AutoMapper;
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
        public async Task Save_InsertUser_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            UserService service = new UserService(dbContext, Mapper);

            User user1 = SampleData.CreateSampleUser();
            User user2 = SampleData.CreateSampleUser();

            // Act
            await service.InsertAsync(user1);
            await service.InsertAsync(user2);

            // Assert
            Assert.AreNotEqual(user1.Id, user2.Id);
        }

        [TestMethod]
        public async Task Fetch_InsertUser_HasData()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            UserService service = new UserService(dbContext, Mapper);

            User user1 = SampleData.CreateSampleUser();

            // Act
            await service.InsertAsync(user1);
            User user2 = await service.FetchByIdAsync(user1.Id);

            // Assert
            Assert.IsNotNull(user2);
            Assert.AreEqual(user1, user2);
        }

        [TestMethod]
        public async Task Delete_InsertUser_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            UserService service = new UserService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();

            // Act
            await service.InsertAsync(user);
            bool result = await service.DeleteAsync(user.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Update_ChangeFirstName_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            UserService service = new UserService(dbContext, Mapper);

            User user = SampleData.CreateSampleUser();

            // Act
            await service.InsertAsync(user);
            user.FirstName = "New FirstName";
            await service.UpdateAsync(user.Id, user);
            User user2 = await service.FetchByIdAsync(user.Id);

            // Assert
            Assert.AreEqual("New FirstName", user2.FirstName);
        }
    }
}
