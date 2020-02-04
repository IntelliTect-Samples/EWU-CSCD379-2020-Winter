using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        public void Constructor_PassService_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();

            // Act
            _ = new EntityController<Group>(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NoService_Exception()
        {
            // Arrange

            // Act
            _ = new EntityController<Group>(null!);

            // Assert
        }

        [TestMethod]
        public async Task Get_WithEntities_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group entity = CreateEntity();
            Group entity2 = CreateEntity();
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(new List<Group>() { entity, entity2 });

            EntityController<Group> controller = new EntityController<Group>(service.Object);

            // Act
            List<Group> entityResult = (List<Group>)await controller.Get();

            // Assert
            Assert.AreEqual(2, entityResult.Count);
        }

        [TestMethod]
        public async Task GetById_WithEntities_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group entity = CreateEntity();

            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);
            int id = entity.Id;
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(new TestGroup(SampleData.CreateSampleGroup(), id));

            EntityController<Group> controller = new EntityController<Group>(service.Object);
            await service.Object.InsertAsync(entity);

            // Act
            ActionResult<Group> entityResult = await controller.Get(entity.Id);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            Group result = (Group)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(entity.Title, result.Title);
        }

        [TestMethod]
        public async Task GetById_WithoutEntities_EmptyList()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            int id = 0;

            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync((Group)null!);

            EntityController<Group> controller = new EntityController<Group>(service.Object);

            // Act
            ActionResult<Group> entityResult = await controller.Get(id);

            // Assert
            Assert.IsTrue(entityResult.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Post_AddEntity_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group entity = SampleData.CreateSampleGroup();
            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);

            EntityController<Group> controller = new EntityController<Group>(service.Object);

            // Act
            entity = await controller.Post(entity);

            // Assert
            Assert.IsNotNull(entity.Id);
        }

        [TestMethod]
        public async Task Put_UpdateEntity_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            Group entity = CreateEntity();

            int id = 1;

            service.Setup(g => g.UpdateAsync(id, entity)).ReturnsAsync(entity);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(entity);

            EntityController<Group> controller = new EntityController<Group>(service.Object);

            // Act
            ActionResult<Group> entityResult = await controller.Put(entity.Id, entity);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            Group result = (Group)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task Delete_DeleteEntity_Success()
        {
            // Arrange
            Mock<IGroupService> service = new Mock<IGroupService>();
            int id = 1;

            service.Setup(g => g.DeleteAsync(id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(CreateEntity());

            EntityController<Group> controller = new EntityController<Group>(service.Object);

            // Act
            ActionResult<bool> entityResult = await controller.Delete(id);

            // Assert

            Assert.IsTrue(entityResult.Result is OkObjectResult);
            bool result = (bool)((OkObjectResult)entityResult.Result).Value;
            Assert.IsTrue(result);
        }

        private Group CreateEntity()
        {
            return new TestGroup(SampleData.CreateSampleGroup(), 1);
        }

        private class TestGroup : Group
        {
            public TestGroup(Group Group, int id) : base(Group.Title)
            {
                Id = id;
            }
        }
    }
}
