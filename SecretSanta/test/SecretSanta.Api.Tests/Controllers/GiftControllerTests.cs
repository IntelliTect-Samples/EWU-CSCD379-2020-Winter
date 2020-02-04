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
    public class GiftControllerTests
    {
        [TestMethod]
        public void Constructor_PassService_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();

            // Act
            _ = new EntityController<Gift>(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NoService_Exception()
        {
            // Arrange

            // Act
            _ = new EntityController<Gift>(null!);

            // Assert
        }

        [TestMethod]
        public async Task Get_WithEntities_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift entity = CreateEntity();
            Gift entity2 = CreateEntity();
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(new List<Gift>() { entity, entity2 });

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);

            // Act
            List<Gift> entityResult = (List<Gift>)await controller.Get();

            // Assert
            Assert.AreEqual(2, entityResult.Count);
        }

        [TestMethod]
        public async Task GetById_WithEntities_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift entity = CreateEntity();

            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);
            int id = entity.Id;
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(new TestGift(SampleData.CreateSampleGift(), id));

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);
            await service.Object.InsertAsync(entity);

            // Act
            ActionResult<Gift> entityResult = await controller.Get(entity.Id);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            Gift result = (Gift)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(entity.Title, result.Title);
        }

        [TestMethod]
        public async Task GetById_WithoutEntities_EmptyList()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            int id = 0;

            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync((Gift) null!);

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);

            // Act
            ActionResult<Gift> entityResult = await controller.Get(id);

            // Assert
            Assert.IsTrue(entityResult.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Post_AddEntity_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift entity = SampleData.CreateSampleGift();
            service.Setup(g => g.InsertAsync(entity)).ReturnsAsync(entity);

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);

            // Act
            entity = await controller.Post(entity);

            // Assert
            Assert.IsNotNull(entity.Id);
        }

        [TestMethod]
        public async Task Put_UpdateEntity_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift entity = CreateEntity();

            int id = 1;

            service.Setup(g => g.UpdateAsync(id, entity)).ReturnsAsync(entity);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(entity);

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);

            // Act
            ActionResult<Gift> entityResult = await controller.Put(entity.Id, entity);

            // Assert
            Assert.IsTrue(entityResult.Result is OkObjectResult);
            Gift result = (Gift)((OkObjectResult)entityResult.Result).Value;
            Assert.AreEqual(id, result.Id);
        }

        [TestMethod]
        public async Task Delete_DeleteEntity_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            int id = 1;

            service.Setup(g => g.DeleteAsync(id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(id)).ReturnsAsync(CreateEntity());

            EntityController<Gift> controller = new EntityController<Gift>(service.Object);

            // Act
            ActionResult<bool> entityResult = await controller.Delete(id);

            // Assert

            Assert.IsTrue(entityResult.Result is OkObjectResult);
            bool result = (bool)((OkObjectResult)entityResult.Result).Value;
            Assert.IsTrue(result);
        }

        private Gift CreateEntity()
        {
            return new TestGift(SampleData.CreateSampleGift(), 1);
        }

        private class TestGift : Gift
        {
            public TestGift(Gift gift, int id) : base(gift.Title, gift.Url, gift.Description, SampleData.CreateSampleUser())
            {
                Id = id;
            }
        }
    }
}
