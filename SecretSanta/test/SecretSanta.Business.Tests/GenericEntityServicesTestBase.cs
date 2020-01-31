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
            TEntity fetched = await service.FetchByIdAsync((int) entity.Id);

            // Assert
            var props = entity.GetType().GetProperties(BindingFlags.Public);
            foreach (var property in props)
            {
                var name = property.Name;
                entity.GetType().GetProperty(name).GetValue(entity);
                Assert.AreEqual(entity.GetType().GetProperty(name).GetValue(fetched), entity.GetType().GetProperty(name).GetValue(entity));
            }
        }

        // Update

        // Destroy
    }
}
