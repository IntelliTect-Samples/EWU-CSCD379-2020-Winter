using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using SecretSanta.Data.Tests;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task FetchAllAsync_TwoGroups_Success()
        {
            //Arrange
            Group group1 = SampleData.CreateMoneyGrubbers;
            Group group2 = SampleData.CreateJellyFishers;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(group1);
            dbContext.Groups.Add(group2);
            await dbContext.SaveChangesAsync();
            GroupService GroupService = new GroupService(dbContext, _Mapper);
            //Act
            List<Group> groups = GroupService.FetchAllAsync().Result;
            //Assert
            Assert.AreEqual<string>(group1.Title, groups.ElementAt(0).Title);
        }

        [TestMethod]
        public async Task FetchByIdAsync_OneGroup_Success()
        {
            //Arrange
            Group group = SampleData.CreateMoneyGrubbers;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(group);
            await dbContext.SaveChangesAsync();
            GroupService GroupService = new GroupService(dbContext, _Mapper);
            //Act
            Group result = GroupService.FetchByIdAsync(1).Result;
            //Assert
            Assert.AreEqual<string>(group.Title, result.Title);
        }

        [TestMethod]
        public async Task InsertAsync_OneGroup_Success()
        {
            //Arrange
            Group group = SampleData.CreateMoneyGrubbers;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GroupService GroupService = new GroupService(dbContext, _Mapper);
            //Act
            await GroupService.InsertAsync(group);
            //Assert
            Group result = await dbContext.Groups.FirstOrDefaultAsync();
            Assert.AreEqual<string>(group.Title, result.Title);
        }

        [TestMethod]
        public async Task UpdateAsync_OneGroup_Success()
        {
            //Arrange
            Group group1 = SampleData.CreateMoneyGrubbers;
            Group group2 = SampleData.CreateJellyFishers;
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            dbContext.Groups.Add(group1);
            dbContext.Groups.Add(group2);//have to add group2 here so its fingerprint properties get set, otherwise Sqlite rejects update becasue fingerpint properties are null
            await dbContext.SaveChangesAsync();
            GroupService GroupService = new GroupService(dbContext, _Mapper);
            //Act
            await GroupService.UpdateAsync(1, group2);
            //Assert
            Group result = await dbContext.Groups.FirstOrDefaultAsync();
            Assert.AreEqual(group2.Title, result.Title);
        }
    }
}
