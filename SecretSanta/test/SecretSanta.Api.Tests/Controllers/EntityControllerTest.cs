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
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    public abstract class EntityControllerTest<TEntity> where TEntity : EntityBase
    {
        protected abstract TEntity CreateInstance();
        private EntityService<TEntity> Service { get; }

        public EntityControllerTest(EntityService<TEntity> entityService)
        {
            Service = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        [TestMethod]
        public void Create_EntityController_Success()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullService_ThrowsException()
        {
            //Arrange, Act & Assert
            _ = new EntityController<TEntity>(null!);
        }

        [TestMethod]
        public async Task GetAll_WithExistingEntitys_Success()
        {
            //Arrange
            TEntity entity = CreateInstance();
            await Service.InsertAsync(entity);

            var controller = new EntityController<TEntity>(Service);

            //Act
            IEnumerable<TEntity> entities = await controller.Get();

            //Assert
            Assert.AreEqual(1, entities.ToList().Count);
        }

        [TestMethod]
        public async Task GetById_ExistingEntity_Success()
        {
            //Arrange
            TEntity entity = CreateInstance();
            entity = await Service.InsertAsync(entity);

            var controller = new EntityController<TEntity>(Service);

            //Act
            ActionResult<TEntity> rv = await controller.Get(entity.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_ExistingEntity_Throws404Error()
        {
            //Arrange
            var controller = new EntityController<TEntity>(Service);

            //Act
            ActionResult<TEntity> rv = await controller.Get(0);

            //Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Create_Post_Success()
        {
            //Arrange
            TEntity entity = CreateInstance();

            var controller = new EntityController<TEntity>(Service);

            //Act
            ActionResult<TEntity> rv = await controller.Post(entity);

            //Assert
            Assert.IsTrue(rv.Value != null);
        }

        [TestMethod]
        public async Task Update_Entity_Success()
        {
            //Arrange
            TEntity entity1 = CreateInstance();
            TEntity entity2 = CreateInstance();

            entity1 = await Service.InsertAsync(entity1);
            entity2 = await Service.InsertAsync(entity2);

            var controller = new EntityController<TEntity>(Service);

            //Act
            ActionResult<TEntity> rv = await controller.Put(entity1.Id, entity2);

            //Assert
            Assert.AreEqual(entity2.Id, rv.Value.Id);
        }

        [TestMethod]
        public async Task Delete_EntityWithId_Success()
        {
            TEntity entity = CreateInstance();
            entity = await Service.InsertAsync(entity);
            var controller = new EntityController<TEntity>(Service);

            //Act
            ActionResult<TEntity> rv = await controller.Delete(entity.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }

    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        private Dictionary<int, TEntity> Items { get; } = new Dictionary<int, TEntity>();
        protected abstract TEntity CreateWithId(TEntity entity, int id);

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Items.Remove(id));
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            var items = Items.Values.ToList();
            return Task.FromResult(items);
        }

        public Task<TEntity?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out TEntity? entity))
            {
                Task<TEntity?> task1 = Task.FromResult<TEntity?>(entity);
                return task1;
            }
            Task<TEntity?> task2 = Task.FromResult<TEntity?>(null!);
            return task2;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {

            int id = Items.Count + 1;
            Items[id] = CreateWithId(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            Items[id] = entity;
            return Task.FromResult<TEntity?>(entity);
        }
    }
}
