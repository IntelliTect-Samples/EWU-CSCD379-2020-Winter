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
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task InsertAsync_CoolAndCrazy_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            var crazyGroup = SampleData.CreateCrazyGroup();
            var coolGroup = SampleData.CreateCoolGroup();

            // Act
            await service.InsertAsync(crazyGroup);
            await service.InsertAsync(coolGroup);

            // Assert
            Assert.IsNotNull(crazyGroup.Id);
            Assert.IsNotNull(coolGroup.Id);
        }

        [TestMethod]
        public async Task FetchAll_ShouldRetrieveAllGroups_Success()
        {
            //Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService service = new GroupService(dbContext, Mapper);

            Group group1 = SampleData.CreateCoolGroup();
            Group group2 = SampleData.CreateCrazyGroup();

            await service.InsertAsync(group1);
            await service.InsertAsync(group2);

            //Act
            List<Group> groups = await service.FetchAllAsync();

            Group groupFromDb = groups[0];
            Group groupFromDb2 = groups[1];

            //Assert
            Assert.AreEqual(group1, groupFromDb);
            Assert.AreEqual(group2, groupFromDb2);
            Assert.IsNotNull(groupFromDb.Title);
            Assert.IsNotNull(groupFromDb2.Title);
            Assert.AreEqual(SampleData.CoolGroupName, groupFromDb.Title);
            Assert.AreEqual(SampleData.CrazyGroupName, groupFromDb2.Title);
        }

        [TestMethod]
        public async Task FetchById_Group_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);

            IGroupService groupService = new GroupService(dbContext, Mapper);

            var group = SampleData.CreateCoolGroup();

            // Act
            await groupService.InsertAsync(group);

            using var dbContext2 = new ApplicationDbContext(Options);
            groupService = new GroupService(dbContext, Mapper);
            Group groupFromDb = await groupService.FetchByIdAsync(group.Id!.Value);

            // Assert
            Assert.IsNotNull(groupFromDb);
        }

        [TestMethod]
        public async Task UpdateGroup_ShouldSaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            var crazyGroup = SampleData.CreateCrazyGroup();
            var coolGroup = SampleData.CreateCoolGroup();

            await service.InsertAsync(crazyGroup);
            await service.InsertAsync(coolGroup);

            // Act
            using var dbContextFetch = new ApplicationDbContext(Options);
            Group crazyGroupFromDb = await dbContextFetch.Groups.SingleAsync(item => item.Id == crazyGroup.Id);

            const string updateTitle = "Extreme group";
            crazyGroupFromDb.Title = updateTitle;

            // Update Crazy Group using the Cool Groups Id.
            await service.UpdateAsync(coolGroup.Id!.Value, crazyGroupFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            crazyGroupFromDb = await dbContextAssert.Groups.SingleAsync(item => item.Id == crazyGroup.Id);
            Group coolGroupFromDb = await dbContextAssert.Groups.SingleAsync(item => item.Id == 2);

            Assert.AreEqual(updateTitle, coolGroupFromDb.Title);
            Assert.AreEqual(SampleData.CrazyGroupName, crazyGroupFromDb.Title);

        }

        [TestMethod]
        public async Task Delete_SingleGroupOnly_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IGroupService groupService = new GroupService(dbContext, Mapper);

            var group = SampleData.CreateCoolGroup();
            var group2 = SampleData.CreateCrazyGroup();

            await groupService.InsertAsync(group);
            await groupService.InsertAsync(group2);

            await dbContext.SaveChangesAsync();

            // Act
            bool deleted = await groupService.DeleteAsync(group.Id!.Value);
            using var dbContextAssert = new ApplicationDbContext(Options);
            Group groupFromDb = await dbContextAssert.Set<Group>().SingleOrDefaultAsync(e => e.Id == group.Id);
            Group groupFromDb2 = await dbContextAssert.Set<Group>().SingleOrDefaultAsync(e => e.Id == group2.Id);

            // Assert
            Assert.IsTrue(deleted);
            Assert.IsNull(groupFromDb);
            Assert.AreEqual(group2.Title, groupFromDb2.Title);
            Assert.AreEqual(group2.Id, groupFromDb2.Id);
        }
    }
}
