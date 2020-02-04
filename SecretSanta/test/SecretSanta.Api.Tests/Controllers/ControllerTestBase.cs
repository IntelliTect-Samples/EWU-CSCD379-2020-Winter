using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace SecretSanta.Api.Tests.Controllers
{
    /* reference from Blod Engine
    
    [TestClass]
    public class AuthorControllerTests
    {
        [TestMethod]
        public void Create_AuthorController_Success()
        {
            //Arrange
            var service = new AuthorService();

            //Act
            _ = new AuthorController(service);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange
            
            //Act
            _ = new AuthorController(null!);

            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingAuthor_Success()
        {
            // Arrange
            var service = new AuthorService();
            Author author = SampleData.CreateInigoMontoya();
            author = await service.InsertAsync(author);

            var controller = new AuthorController(service);

            // Act
            ActionResult<Author> rv = await controller.Get(author.Id!.Value);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }
     */
    [TestClass]
    public abstract class ControllerTestBase<TController, TEntity> where TController : IController<TEntity> where TEntity : EntityBase
    {
        protected abstract IEntityService<TEntity> CreateService();
        protected abstract TController CreateController(IEntityService<TEntity> service);

        protected abstract TEntity CreateEntity();

        [TestMethod]
        public void Create_Controller_Success()
        {
            //Arrange

            //Act
            _ = CreateController(CreateService());

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange

            //Act
            _ = CreateController(null!);

            //Assert
        }

        // Get from Id
        [TestMethod]
        public async Task GetById_WithExistingEntity_Success()
        {
            // Arrange
            var service = CreateService();
            TEntity entity = CreateEntity();
            entity = await service.InsertAsync(entity);

            var controller = CreateController(service);

            // Act
            ActionResult<TEntity> rv = await controller.Get(entity.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        // Post
        [TestMethod]
        public async Task PostEntity_Success()
        {
            // Arrange
            var service = CreateService();
            TEntity entity = CreateEntity();
            var controller = CreateController(service);

            // Act
            ActionResult<TEntity> rv = await controller.Post(entity);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        // Put
        [TestMethod]
        public async Task PutEntity_Success()
        {
            // Arrange
            TEntity entity = CreateEntity();
            var service = CreateService();
            entity = await service.InsertAsync(entity);
            var controller = CreateController(service);

            // Act
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType == typeof(string))
                    prop.SetValue(entity, "mew");
            }

            ActionResult<TEntity> rv = await controller.Put(entity.Id, entity);

            // Assert
            props = rv.Result.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType == typeof(string))
                    Assert.AreEqual("mew", prop.GetValue(rv.Result));
            }
        }

        // Delete
        [TestMethod]
        public async Task DeleteEntity_Success()
        {
            // Arrange
            var service = CreateService();
            TEntity entity = CreateEntity();
            entity = await service.InsertAsync(entity);
            var controller = CreateController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(entity.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }
    }
}
