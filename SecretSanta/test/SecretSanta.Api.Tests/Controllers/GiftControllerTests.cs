using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseControllerTests
    { 
        [TestMethod]
        public async Task Get_Gift_Success()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateGift1();
            context.Gifts.Add(gift);
            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.GetAsync("api/Gift");
    #pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);

            Assert.AreEqual(gift.Id, gifts[0].Id);
            Assert.AreEqual(gift.Title, gifts[0].Title);
            Assert.AreEqual(gift.Description, gifts[0].Description);
            Assert.AreEqual(gift.Url, gifts[0].Url);
            Assert.AreEqual(gift.UserId, gifts[0].UserId);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            //Arrange
            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(SampleData.CreateGift1());
            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync("api/Gift/42", stringContent);
    #pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesGift()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Gift giftEntity = SampleData.CreateGift1();

            context.Gifts.Add(giftEntity);
            context.SaveChanges();

            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.GiftInput>(giftEntity);
            gift.Title += "changed";
            gift.Description += "changed";
            gift.Url += "changed";

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{giftEntity.Id}", stringContent);
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
        public async Task Delete_WithValidId_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Gift giftEntity1 = SampleData.CreateGift1();
            Gift giftEntity2 = SampleData.CreateGift1();
            Gift giftEntity3 = SampleData.CreateGift1();

            context.Gifts.Add(giftEntity1);
            context.Gifts.Add(giftEntity2);
            context.Gifts.Add(giftEntity3);

            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.DeleteAsync($"api/Gift/{giftEntity1.Id}");
    #pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();

            using ApplicationDbContext contextAct = Factory.GetDbContext();

            List<Gift> giftsAfter = await context.Gifts.ToListAsync();

            Assert.AreEqual(2, giftsAfter.Count);
        }

        [TestMethod]
        public async Task Delete_WithInvalidId_NotFound()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Gift giftEntity = SampleData.CreateGift1();

            context.Gifts.Add(giftEntity);
            context.SaveChanges();

            //Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.DeleteAsync($"api/Gift/{42}");
    #pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]
        [DataRow(nameof(Business.Dto.GiftInput.UserId))]
        public async Task Post_WithOutRequiredProperties_BadRequest(string propertyName)
        {
            // Arrange
            Gift entity = SampleData.CreateGift1();

            //DTO
            Business.Dto.GiftInput gift = Mapper.Map<Gift, Business.Dto.Gift>(entity);
            System.Type inputType = typeof(Business.Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(gift, null);

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            //Justification: URL is type string, not type URI in this project
    #pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await Client.PostAsync($"api/Gift", stringContent);
    #pragma warning restore CA2234 // Pass system uri objects instead of strings

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
