using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            var service = new TestGroupService();

            _ = new GroupController(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new GroupController(null!);
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_Success()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            // Act
            ActionResult<Group> rv = await controller.Get(group.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_IdNotFound()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            // Act
            ActionResult<Group> rv = await controller.Get(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Get_ReturnsListOfGroups_Success()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            Group group2 = SampleData.CreateGroup();
            group = await service.InsertAsync(group);
            group2 = await service.InsertAsync(group2);

            var controller = new GroupController(service);

            // Act
            IEnumerable<Group> rv = await controller.Get();

            // Assert
            Assert.AreEqual<int>(2, rv.Count());
        }

        [TestMethod]
        public async Task Put_UpdatesGroup_Success()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            Group group2 = new Group("New Test Title");
            group = await service.InsertAsync(group);
            group2 = await service.InsertAsync(group2);

            var controller = new GroupController(service);

            // Act
            ActionResult<Group> rv = await controller.Put(group.Id, group2);

            // Assert
            Assert.AreEqual<string>("New Test Title", rv.Value.Title);
        }

        [TestMethod]
        public async Task Delete_Group_Success()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(group.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Delete_Group_IdNotFound()
        {
            // Arrange
            var service = new TestGroupService();
            Group group = SampleData.CreateGroup();
            group = await service.InsertAsync(group);

            var controller = new GroupController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }
    }

    public class TestGroupService : IGroupService
    {
        private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Items.Remove(id));
        }

        public Task<List<Group>> FetchAllAsync()
        {
            return Task.FromResult(Items.Values.ToList());
        }

        public Task<Group> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Group? group))
            {
                Task<Group> task1 = Task.FromResult<Group>(group);
                return task1;
            }
            Task<Group> task2 = Task.FromResult<Group>(null!);
            return task2;
        }

        public Task<Group> InsertAsync(Group entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGroup(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Group?> UpdateAsync(int id, Group entity)
        {
            Items[id] = entity;
            return Task.FromResult<Group?>(Items[id]);
        }

        private class TestGroup : Group
        {
            public TestGroup(Group group, int id)
                : base((group ?? throw new ArgumentNullException(nameof(group))).Title)
            {
                Id = id;
            }
        }
    }
}
