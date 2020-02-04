using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class EntityControllerTests<TEntity>
        where TEntity : EntityBase
    {
        protected abstract EntityController<TEntity> CreateController { get; }

        protected abstract TEntity CreateEntity { get; }

        [TestMethod]
        public void Create_Success() =>
            _ = CreateController;

        [TestMethod]
        public async Task Post_Success()
        {
            var controller = CreateController;
            var entity = CreateEntity;

            ActionResult<TEntity> response = await controller.Post(entity);

            Assert.IsNotNull(response.Value.Id);
        }

        [TestMethod]
        public async Task Get_Success()
        {
            var controller = CreateController;

            IEnumerable<TEntity> response = await controller.Get();

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Get_WithId_Success()
        {
            var controller = CreateController;
            _ = controller.Post(CreateEntity);

            ActionResult<TEntity> response = await controller.Get(0);

            Assert.IsTrue(response.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Get_WithBadId_Failure()
        {
            var controller = CreateController;

            ActionResult<TEntity> response = await controller.Get(0);

            Assert.IsTrue(response.Result is NotFoundResult);

        }

        [TestMethod]
        public async Task Put_Success()
        {
            var controller = CreateController;
            _ = controller.Post(CreateEntity);

            ActionResult<TEntity> response = await controller.Put(0, CreateEntity);

            Assert.IsTrue(response.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Put_Failure()
        {
            var controller = CreateController;

            ActionResult<TEntity> response = await controller.Put(0, CreateEntity);

            Assert.IsTrue(response.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_Success()
        {
            var controller = CreateController;
            _ = controller.Post(CreateEntity);

            IActionResult response = await controller.Delete(0);
            ActionResult<TEntity> response2 = await controller.Get(0);

            Assert.IsTrue(response is OkResult);
            Assert.IsTrue(response2.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_Failure()
        {
            var controller = CreateController;

            IActionResult response = await controller.Delete(0);

            Assert.IsTrue(response is NotFoundResult);
        }
    }
}

