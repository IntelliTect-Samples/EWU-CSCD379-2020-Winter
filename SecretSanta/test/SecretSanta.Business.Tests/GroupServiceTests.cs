using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task Group_InsertAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");

            // Act
            await service.InsertAsync(group);

            // Assert
            Assert.AreNotEqual<int>(0, group.Id);
        }

        [TestMethod]
        public async Task Group_InsertAsyncUsingMultipleGroups_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");
            Group group2 = new Group("TestTitle2");

            // Act
            await service.InsertAsync(group, group2);

            // Assert
            Assert.AreNotEqual<int>(0, group.Id);
            Assert.AreNotEqual<int>(0, group2.Id);
        }

        [TestMethod]
        public async Task Group_FetchByIdAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");

            await service.InsertAsync(group);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext2, Mapper);
            group = await service.FetchByIdAsync(group.Id);

            // Assert
            Assert.IsNotNull(group);
        }

        [TestMethod]
        public async Task Group_FetchAllAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");
            Group group2 = new Group("TestTitle2");

            await service.InsertAsync(group, group2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext2, Mapper);
            List<Group> list = await service.FetchAllAsync();

            // Assert
            Assert.AreEqual<int>(2, list.Count);
            Assert.AreEqual<string>("TestTitle", list[0].Title);
            Assert.AreEqual<string>("TestTitle2", list[1].Title);
        }

        [TestMethod]
        public async Task Group_UpdateAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");
            Group group2 = new Group("TestTitle2");

            await service.InsertAsync(group);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext2, Mapper);
            await service.UpdateAsync(group.Id, group2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext3, Mapper);
            var groupAssert = await service.FetchByIdAsync(group.Id);

            Assert.AreEqual<int>(1, groupAssert.Id);
            Assert.AreEqual<string>("TestTitle2", groupAssert.Title);
        }

        [TestMethod]
        public async Task Group_DeleteAsync_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group = new Group("TestTitle");
            Group group2 = new Group("TestTitle2");

            await service.InsertAsync(group, group2);

            // Act
            using var dbContext2 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext2, Mapper);
            await service.DeleteAsync(2);

            // Assert
            using var dbContext3 = new ApplicationDbContext(Options);
            service = new GroupService(dbContext3, Mapper);
            List<Group> list = await service.FetchAllAsync();
            Assert.AreEqual<int>(1, list.Count);
        }
    }
}
