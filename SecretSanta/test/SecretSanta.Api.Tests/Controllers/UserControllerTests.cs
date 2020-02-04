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
    public class UserControllerTests
    {
        [TestMethod]
        public void Constructor_PassService_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();

            // Act
            _ = new EntityController<User>(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NoService_Exception()
        {
            // Arrange

            // Act
            _ = new EntityController<User>(null!);

            // Assert
        }

        [TestMethod]
        public async Task Get_WithEntities_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User entity = CreateEntity();
            User entity2 = CreateEntity();
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(new List<User>() { entity, entity2 });

            EntityController<User> controller = new EntityController<User>(service.Object);

            // Act
            List<User> entityResult = (List<User>)await controller.Get();

            // Assert
            Assert.AreEqual(2, entityResult.Count);
        }

        [TestMethod]
        public async Task GetById_WithEntities_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User entity = CreateEntity();

            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);
            int id = entity.Id;
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(new TestUser(SampleData.CreateSampleUser(), id));

            EntityController<User> controller = new EntityController<User>(service.Object);
            await service.Object.InsertAsync(entity);

            // Act
            ActionResult<User> entityResult = await controller.Get(entity.Id);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            User result = (User)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(entity.FirstName, result.FirstName);
        }

        [TestMethod]
        public async Task GetById_WithoutEntities_EmptyList()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            int id = 0;

            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync((User)null!);

            EntityController<User> controller = new EntityController<User>(service.Object);

            // Act
            ActionResult<User> entityResult = await controller.Get(id);

            // Assert
            Assert.IsTrue(entityResult.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Post_AddEntity_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User entity = SampleData.CreateSampleUser();
            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);

            EntityController<User> controller = new EntityController<User>(service.Object);

            // Act
            entity = await controller.Post(entity);

            // Assert
            Assert.IsNotNull(entity.Id);
        }

        [TestMethod]
        public async Task Put_UpdateEntity_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            User entity = CreateEntity();

            int id = 1;

            service.Setup(g => g.UpdateAsync(id, entity)).ReturnsAsync(entity);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(entity);

            EntityController<User> controller = new EntityController<User>(service.Object);

            // Act
            ActionResult<User> entityResult = await controller.Put(entity.Id, entity);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            User result = (User)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task Delete_DeleteEntity_Success()
        {
            // Arrange
            Mock<IUserService> service = new Mock<IUserService>();
            int id = 1;

            service.Setup(g => g.DeleteAsync(id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(CreateEntity());

            EntityController<User> controller = new EntityController<User>(service.Object);

            // Act
            ActionResult<bool> entityResult = await controller.Delete(id);

            // Assert

            Assert.IsTrue(entityResult.Result is OkObjectResult);
            bool result = (bool)((OkObjectResult)entityResult.Result).Value;
            Assert.IsTrue(result);
        }

        private User CreateEntity()
        {
            return new TestUser(SampleData.CreateSampleUser(), 1);
        }

        private class TestUser : User
        {
            public TestUser(User User, int id) : base(User.FirstName, User.LastName)
            {
                Id = id;
            }
        }
    }
}
