using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public abstract class BaseApiControllerTests<TEntity, TService> 
        where TEntity : EntityBase
        where TService : InMemoryEntityService<TEntity>, new()
    {
        protected abstract BaseApiController<TEntity> CreateController(TService service);

        protected abstract TEntity CreateEntity();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_RequiresService()
        {
            new ThrowingController();
        }

        [TestMethod]
        public async Task Get_FetchesAllItems()
        {
            TService service = new TService();
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());
            service.Items.Add(CreateEntity());

            BaseApiController<TEntity> controller = CreateController(service);

            IEnumerable<TEntity> items = await controller.Get();

            CollectionAssert.AreEqual(service.Items.ToList(), items.ToList());
        }

        [TestMethod]
        public async Task Get_WhenEntityDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TEntity> controller = CreateController(service);

            IActionResult result = await controller.Get(1);

            Assert.IsTrue(result is NotFoundResult);
        }


        [TestMethod]
        public async Task Get_WhenEntityExists_ReturnsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TEntity> controller = CreateController(service);

            IActionResult result = await controller.Get(entity.Id);

            var okResult = result as OkObjectResult;
            
            Assert.AreEqual(entity, okResult?.Value);
        }

        [TestMethod]
        public async Task Put_UpdatesItem()
        {
            TService service = new TService();
            TEntity entity1 = CreateEntity();
            service.Items.Add(entity1);
            TEntity entity2 = CreateEntity();
            BaseApiController<TEntity> controller = CreateController(service);

            TEntity? result = await controller.Put(entity1.Id, entity2);

            Assert.AreEqual(entity2, result);
            Assert.AreEqual(entity2, service.Items.Single());
        }

        [TestMethod]
        public async Task Post_InsertsItem()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            BaseApiController<TEntity> controller = CreateController(service);

            TEntity? result = await controller.Post(entity);

            Assert.AreEqual(entity, result);
            Assert.AreEqual(entity, service.Items.Single());
        }

        [TestMethod]
        public async Task Delete_WhenItemDoesNotExist_ReturnsNotFound()
        {
            TService service = new TService();
            BaseApiController<TEntity> controller = CreateController(service);

            IActionResult result = await controller.Delete(1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public async Task Delete_WhenItemExists_ReturnsOk()
        {
            TService service = new TService();
            TEntity entity = CreateEntity();
            service.Items.Add(entity);
            BaseApiController<TEntity> controller = CreateController(service);

            IActionResult result = await controller.Delete(entity.Id);

            Assert.IsTrue(result is OkResult);
        }

        private class ThrowingController : BaseApiController<TEntity>
        {
            public ThrowingController() : base(null!)
            { }
        }
    }

    public class InMemoryEntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        public IList<TEntity> Items { get; } = new List<TEntity>();

        public Task<bool> DeleteAsync(int id)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                return Task.FromResult(Items.Remove(found));
            }
            return Task.FromResult(false);
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            return Task.FromResult(Items.ToList());
        }

        public Task<TEntity> FetchByIdAsync(int id)
        {
            return Task.FromResult(Items.FirstOrDefault(x => x.Id == id));
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            Items.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            if (Items.FirstOrDefault(x => x.Id == id) is { } found)
            {
                Items[Items.IndexOf(found)] = entity;
                return Task.FromResult<TEntity?>(entity);
            }
            return Task.FromResult(default(TEntity));
        }
    }
}
