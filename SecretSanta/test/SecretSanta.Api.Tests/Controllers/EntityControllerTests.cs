using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public abstract class EntityControllerTests<TEntity> where TEntity : EntityBase
    {

        protected abstract Mock<IEntityService<TEntity>> CreateService();
        protected abstract EntityController<TEntity>     CreateController(IEntityService<TEntity> service);

        protected abstract TEntity CreateEntity();

        [TestMethod]
        public async Task PostEntity_AddsToData()
        {
            var entity  = CreateEntity();
            var service = CreateService();

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            var postEntity = await CreateController(service.Object).Post(entity);

            Assert.IsNotNull(postEntity);
            Assert.AreEqual(entity.Id, postEntity.Id);
        }

        [TestMethod]
        public async Task GetEntity_ById_ReturnsCorrectly()
        {
            var entity     = CreateEntity();
            var service    = CreateService();
            var controller = CreateController(service.Object);

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            await controller.Post(entity);

            // Get Entity by ID
            service.Setup(s => s.FetchByIdAsync(1)).ReturnsAsync(entity);
            var postEntity = EntityFromResult(await controller.Get(1));

            Assert.IsNotNull(postEntity);
            Assert.AreEqual(entity.Id, postEntity.Id);
        }

        [TestMethod]
        public async Task GetEntity_NotExisting_ReturnsNull()
        {
            var entity     = CreateEntity();
            var service    = CreateService();
            var controller = CreateController(service.Object);

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            await controller.Post(entity);

            // Get Entity by ID
            service.Setup(s => s.FetchByIdAsync(2)).ReturnsAsync((TEntity?) null);
            var postEntity = EntityFromResult(await controller.Get(2));

            Assert.IsNull(postEntity);
            Assert.AreNotEqual(entity.Id, 2);
        }

        [TestMethod]
        public async Task GetEntities_ReturnsCorrectAmount()
        {
            var entity     = CreateEntity();
            var service    = CreateService();
            var controller = CreateController(service.Object);

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            await controller.Post(entity);

            // Get Entities
            service.Setup(s => s.FetchAllAsync()).ReturnsAsync(new List<TEntity> {entity});
            var entityList = await controller.Get();

            Assert.IsNotNull(entityList);
            Assert.AreEqual(1, entityList.Count());
        }

        [TestMethod]
        public async Task PutEntity_UpdatesCorrectly()
        {
            var entity     = CreateEntity();
            var service    = CreateService();
            var controller = CreateController(service.Object);

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            await controller.Post(entity);

            // Put Entity
            service.Setup(s => s.UpdateAsync(1, null)).ReturnsAsync((TEntity?) null);
            var putEntity = EntityFromResult(await controller.Put(1, entity));

            Assert.IsNull(putEntity);
        }

        [TestMethod]
        public async Task DeleteEntity_RemovesEntityCorrectly()
        {
            var entity     = CreateEntity();
            var service    = CreateService();
            var controller = CreateController(service.Object);

            // Post Entity
            service.Setup(s => s.InsertAsync(entity)).ReturnsAsync(entity);
            await controller.Post(entity);

            // Delete Entity
            service.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);
            var removed = await controller.Delete(1);

            // Get Entities
            service.Setup(s => s.FetchAllAsync()).ReturnsAsync(new List<TEntity>());
            var entityList = await controller.Get();

            Assert.AreEqual(0, entityList.Count());
        }

        private TEntity EntityFromResult(ActionResult<TEntity> actionResult)
        {
            var objectResult = actionResult.Result as OkObjectResult;
            return objectResult?.Value as TEntity;
        }

    }

}
