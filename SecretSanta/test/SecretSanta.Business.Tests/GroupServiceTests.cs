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

            const string extremeTitle = "Extreme group";
            crazyGroupFromDb.Title = extremeTitle;

            // Update Crazy Group using the Cool Groups Id.
            await service.UpdateAsync(coolGroup.Id!.Value, crazyGroupFromDb);

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);
            crazyGroupFromDb = await dbContextAssert.Groups.SingleAsync(item => item.Id == crazyGroup.Id);
            Group coolGroupFromDb = await dbContextAssert.Groups.SingleAsync(item => item.Id == 2);

            Assert.AreEqual(SampleData.CrazyGroupName, crazyGroupFromDb.Title);
            //Assert.AreEqual(extremeTitle, coolGroupFromDb.Title);

        }
    }
}
