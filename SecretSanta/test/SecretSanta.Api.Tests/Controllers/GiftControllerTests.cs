using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();

            // Act
            _ = new GiftController(service.Object);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            _ = new GiftController(null!);
        }

        [TestMethod]
        public async Task Post_Gift_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift gift = SampleData.CreateArduinoGift();
            service.Setup(g => g.InsertAsync(gift)).ReturnsAsync(gift);
            GiftController controller = new GiftController(service.Object);

            // Act
            gift = await controller.Post(gift);

            // Assert
            Assert.IsNotNull(gift.Id);
        }

        [TestMethod]
        public async Task Put_Gift_Success()
        {
            // Arrange
            var service = new Mock<IGiftService>();
            Gift gift = SampleData.CreateArduinoGift();
            TestGift testGift = new TestGift(gift, gift.Id);
            service.Setup(g => g.UpdateAsync(gift.Id, gift)).ReturnsAsync(testGift);
            service.Setup(g => g.FetchByIdAsync(gift.Id)).ReturnsAsync(testGift);
            var controller = new GiftController(service.Object);

            // Act
            ActionResult<Gift> result = await controller.Put(gift.Id, gift);
            OkObjectResult okResult = (OkObjectResult) result.Result;
            Gift gResult = (Gift) okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(gResult.Id, gift.Id);
        }

        [TestMethod]
        public async Task Fetch_Gift_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift arduino = SampleData.CreateArduinoGift();
            Gift ring = SampleData.CreateRingGift();
            List<Gift> gifts = new List<Gift>();
            gifts.Add(arduino);
            gifts.Add(ring);
            service.Setup(g => g.FetchAllAsync()).ReturnsAsync(gifts);
            GiftController controller = new GiftController(service.Object);

            // Act
            List<Gift> returnGifts = (List<Gift>) await controller.Get();

            // Assert
            Assert.AreEqual(2, returnGifts.Count);
        }

        [TestMethod]
        public async Task Delete_Gift_Success()
        {
            // Arrange
            Mock<IGiftService> service = new Mock<IGiftService>();
            Gift arduino = SampleData.CreateArduinoGift();
            TestGift testGift = new TestGift(arduino, arduino.Id);
            service.Setup(g => g.DeleteAsync(arduino.Id)).ReturnsAsync(true);
            service.Setup(g => g.FetchByIdAsync(arduino.Id)).ReturnsAsync(testGift);
            GiftController controller = new GiftController(service.Object);

            // Act
            ActionResult<bool> result = await controller.Delete(arduino.Id);
            OkObjectResult okResult = (OkObjectResult)result.Result;
            bool gResult = (bool)okResult.Value;

            // Assert
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.AreEqual(true, gResult);
        }

        private class TestGift : Gift
        {
            public TestGift(Gift gift, int id)
                : base(gift.Title, gift.Url, gift.Description, gift.User)
            {
                Id = id;
            }
        }
    }
}
