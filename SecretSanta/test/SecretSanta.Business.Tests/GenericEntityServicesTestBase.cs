using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Data;
using System.Threading.Tasks;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public abstract class GenericEntityServicesTestBase<TEntity> : TestBase where TEntity : EntityBase
    {
        protected abstract IEntityService<TEntity> GetService(ApplicationDbContext dbContext, IMapper mapper);

        protected abstract TEntity CreateEntity(); // needed to nab any entity generically since they have different constructors

        // CRUDDY stuffs
        // Create
        [TestMethod]
        public async Task InsertAsync_Entity_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<TEntity> service = GetService(dbContext, Mapper);

            TEntity entity = CreateEntity();

            // Act
            await service.InsertAsync(entity);

            // Assert
            Assert.IsNotNull(entity.Id);
        }

        // Read
        [TestMethod]
        public async Task FetchById_Entity_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<TEntity> service = GetService(dbContext, Mapper);

            TEntity entity = CreateEntity();

            // Act
            await service.InsertAsync(entity);
            // for some reason the following doesn't work for no warning
            //TEntity fetched = await service.FetchByIdAsync((int) ((entity.Id) ?? throw new ArgumentNullException(entity.Id.GetType().FullName)));
            TEntity fetched = await service.FetchByIdAsync((int) (entity.Id!));

            // Assert
            PropertyInfo[] props = entity.GetType().GetProperties(BindingFlags.Public);
            foreach (var property in props)
            {
                var name = property.Name;
                // I don't like this... everything I tried hasn't worked though will try more but sorta stuck with this for now
                // I really want to use reflection though!
#nullable disable
                entity.GetType().GetProperty(name).GetValue(entity);
                var fetchedValue = entity.GetType().GetProperty(name).GetValue(fetched);
                var entityValue = entity.GetType().GetProperty(name).GetValue(entity);
#nullable enable
                Assert.AreEqual(fetchedValue, entityValue);
            }
        }

        [TestMethod]
        public async Task FetchAll_Entity_Success()
        {
            // Arrange
            using var startupContenxt = new ApplicationDbContext(Options);

            TEntity entity1 = CreateEntity();
            TEntity entity2 = CreateEntity();
            TEntity entity3 = CreateEntity();

            // so Automapper don't map
            startupContenxt.Add(entity1);
            startupContenxt.Add(entity2);
            startupContenxt.Add(entity3);
            startupContenxt.SaveChanges();

            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<TEntity> service = GetService(dbContext, Mapper);

            // Act
            List<TEntity> stuff = await service.FetchAllAsync();

            // Assert
            CollectionAssert.AreEquivalent(
                new[]{entity1.Id, entity2.Id, entity3.Id},
                stuff.Select(e=>e.Id).ToArray());
        }

        // Update
        // trying to consider a way to generically test all...

        // Destroy
        [TestMethod]
        public async Task DeleteAsync_Entity_Success()
        {
            // Arrange
            using var dbContext = new ApplicationDbContext(Options);
            IEntityService<TEntity> service = GetService(dbContext, Mapper);

            TEntity entity = CreateEntity();
            dbContext.Add(entity);
            dbContext.SaveChanges();

            // Act
            var id = (int) (entity.Id!);
            bool resultTrue = await service.DeleteAsync(id);

            // Assert
            Assert.AreEqual(resultTrue, true);
        }
    }
}
