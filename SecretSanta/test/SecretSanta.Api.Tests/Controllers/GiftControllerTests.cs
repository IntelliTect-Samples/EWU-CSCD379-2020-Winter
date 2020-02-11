using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseApiControllerTests
    {
        [TestMethod]
        public async Task Get_ReturnsGift()
        {
            // Arrange
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            Data.Gift arduino = SampleData.CreateArduinoGift();
            dbContext.Gifts.Add(arduino);
            dbContext.SaveChanges();

            // Act
            // Justification: Url's are strings in this project. We aren't using uri objects
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/Gift");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Business.Dto.Gift[] gifts =
                JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);
            Assert.AreEqual(1, gifts.Length);
            Assert.AreEqual(arduino.Id, gifts[0].Id);
            Assert.AreEqual(arduino.Title, gifts[0].Title);
            Assert.AreEqual(arduino.Description, gifts[0].Description);
            Assert.AreEqual(arduino.Url, gifts[0].Url);
            Assert.AreEqual(arduino.UserId, gifts[0].UserId);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.GiftInput ring = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(SampleData.CreateRingGift());
            string jsonData = JsonSerializer.Serialize(ring);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            // Justification: Url's are strings in this project. We aren't using uri objects
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync("api/Gift/3", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesGift()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift ring = SampleData.CreateRingGift();

            context.Gifts.Add(ring);
            context.SaveChanges();

            Business.Dto.GiftInput gift = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(ring);
            gift.Title += "changed";
            gift.Description += "changed";
            gift.Url += "changed";

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{ring.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, options);

            Assert.AreEqual(gift.Title, returnedGift.Title);
            Assert.AreEqual(gift.Description, returnedGift.Description);
            Assert.AreEqual(gift.Url, returnedGift.Url);
            Assert.AreEqual(gift.UserId, returnedGift.UserId);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]
        [DataRow(nameof(Business.Dto.GiftInput.UserId))]
        public async Task Post_WithoutRequiredProperties_BadRequest(string propertyName)
        {
            // Arrange
            Data.Gift arduino = SampleData.CreateArduinoGift();

            Business.Dto.GiftInput ar = Mapper.Map<Data.Gift, Business.Dto.Gift>(arduino);
            System.Type inputType = typeof(Business.Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(ar, null);

            string jsonData = JsonSerializer.Serialize(ar);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            //Justification: URL is type string, not type URI in this project
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{arduino.Id}", stringContent);
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
