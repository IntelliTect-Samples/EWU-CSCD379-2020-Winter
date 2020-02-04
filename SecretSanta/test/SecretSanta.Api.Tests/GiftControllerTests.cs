using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            var service = new TestGiftService();

            _ = new GiftController(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new GiftController(null!);
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_Success()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            // Act
            ActionResult<Gift> rv = await controller.Get(gift.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_IdNotFound()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            // Act
            ActionResult<Gift> rv = await controller.Get(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Get_ReturnsListOfGifts_Success()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            Gift gift2 = SampleData.CreateGift2();
            gift = await service.InsertAsync(gift);
            gift2 = await service.InsertAsync(gift2);

            var controller = new GiftController(service);

            // Act
            IEnumerable<Gift> rv = await controller.Get();

            // Assert
            Assert.AreEqual<int>(2, rv.Count());
        }

        [TestMethod]
        public async Task Put_UpdatesGift_Success()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            Gift gift2 = SampleData.CreateGift2();
            gift = await service.InsertAsync(gift);
            gift2 = await service.InsertAsync(gift2);

            var controller = new GiftController(service);

            // Act
            ActionResult<Gift> rv = await controller.Put(gift.Id, gift2);

            // Assert
            Assert.AreEqual<string>(SampleData.GiftTitle2, rv.Value.Title);
        }

        [TestMethod]
        public async Task Delete_Gift_Success()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(gift.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task Delete_Gift_IdNotFound()
        {
            // Arrange
            var service = new TestGiftService();
            Gift gift = SampleData.CreateGift1();
            gift = await service.InsertAsync(gift);

            var controller = new GiftController(service);

            // Act
            ActionResult<bool> rv = await controller.Delete(3);

            // Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }
    }

    public class TestGiftService : IGiftService
    {
        private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Items.Remove(id));
        }

        public Task<List<Gift>> FetchAllAsync()
        {
            return Task.FromResult(Items.Values.ToList());
        }

        public Task<Gift> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Gift? gift))
            {
                Task<Gift> task1 = Task.FromResult<Gift>(gift);
                return task1;
            }
            Task<Gift> task2 = Task.FromResult<Gift>(null!);
            return task2;
        }

        public Task<Gift> InsertAsync(Gift entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGift(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Gift?> UpdateAsync(int id, Gift entity)
        {
            Items[id] = entity;
            return Task.FromResult<Gift?>(Items[id]);
        }

        private class TestGift : Gift
        {
            public TestGift(Gift gift, int id)
                : base((gift ?? throw new ArgumentNullException(nameof(gift))).Title, gift.Url, gift.Description, gift.User)
            {
                Id = id;
            }
        }
    }
}
