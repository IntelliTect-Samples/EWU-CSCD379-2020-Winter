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
    public class GroupServiceTests : TestBase
    {
        [TestMethod]
        public async Task Save_InsertGroup_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GroupService service = new GroupService(dbContext, Mapper);

            Group group1 = SampleData.CreateSampleGroup();
            Group group2 = SampleData.CreateSampleGroup();

            // Act
            await service.InsertAsync(group1);
            await service.InsertAsync(group2);

            // Assert
            Assert.AreNotEqual(group1.Id, group2.Id);
        }

        [TestMethod]
        public async Task Fetch_InsertGroup_HasData()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GroupService service = new GroupService(dbContext, Mapper);

            Group group1 = SampleData.CreateSampleGroup();

            // Act
            await service.InsertAsync(group1);
            Group group2 = await service.FetchByIdAsync(group1.Id);

            // Assert
            Assert.IsNotNull(group2);
            Assert.AreEqual(group1, group2);
        }

        [TestMethod]
        public async Task Delete_InsertGroup_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GroupService service = new GroupService(dbContext, Mapper);

            Group group = SampleData.CreateSampleGroup();

            // Act
            await service.InsertAsync(group);
            bool result = await service.DeleteAsync(group.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Update_ChangeTitle_Success()
        {
            // Arrange
            IMapper Mapper = AutoMapperProfileConfiguration.CreateMapper();
            using ApplicationDbContext dbContext = new ApplicationDbContext(Options);
            GroupService service = new GroupService(dbContext, Mapper);

            Group group = SampleData.CreateSampleGroup();

            // Act
            await service.InsertAsync(group);
            group.Title = "New Title";
            await service.UpdateAsync(group.Id, group);
            Group group2 = await service.FetchByIdAsync(group.Id);

            // Assert
            Assert.AreEqual("New Title", group2.Title);
        }
    }
}
