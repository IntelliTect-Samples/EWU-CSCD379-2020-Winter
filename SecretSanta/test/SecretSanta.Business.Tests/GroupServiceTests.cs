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
        public async Task CreateGroup_ColonialFleet_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            var fleet = SampleData.CreateGroupColonialFleet();

            // Act
            await service.InsertAsync(fleet);


            // Assert
            Assert.IsNotNull(fleet.Id);
        }

        [TestMethod]
        public async Task UpdateGroup_SaveIntoDatabase()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            var cylonShip = SampleData.CreateGroupCylonShip();
            

            cylonShip = await service.InsertAsync(cylonShip);
            

            // Act

            const string diffTitle = "Other Cylons";
            cylonShip.Title = diffTitle;
           

            await service.UpdateAsync(cylonShip.Id, cylonShip);
            

            using var dbContextFetch = new ApplicationDbContext(Options);
            Group cylonShipFromDb = await dbContextFetch.Groups.SingleAsync(item => item.Id == cylonShip.Id);
            

            // Assert
            using var dbContextAssert = new ApplicationDbContext(Options);

            cylonShipFromDb = await dbContextAssert.Groups.SingleAsync(item => item.Id == cylonShip.Id);
            

            Assert.AreEqual(cylonShipFromDb.Title, diffTitle);
        }

        [TestMethod]
        public async Task DeleteGroup_CylonShip_Success()
        {
            // Arrange
            using var dbContextInsert = new ApplicationDbContext(Options);
            IGroupService service = new GroupService(dbContextInsert, Mapper);

            Group cylonShip = SampleData.CreateGroupCylonShip();
            

            // Act
            await service.InsertAsync(cylonShip);
            Assert.IsNotNull(cylonShip.Id);
            await service.DeleteAsync(cylonShip.Id);

            // Assert
            List<Group> groups = await service.FetchAllAsync();

            Assert.AreEqual(0, groups.Count);
        }
    }
}
