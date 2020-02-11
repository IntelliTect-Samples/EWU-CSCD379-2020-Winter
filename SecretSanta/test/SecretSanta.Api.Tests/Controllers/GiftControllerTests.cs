using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using System;
using AutoMapper;
using SecretSanta.Business;
using System.Net.Http;
using SecretSanta.Data;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
        //Will not be null
#nullable disable
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#nullable enable
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        public async Task Get_ReturnsGifts()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateGift1();
            context.Gifts.Add(gift);
            context.SaveChanges();
            var uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Business.Dto.Gift[] gifts =
                JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);
            Assert.AreEqual(1, gifts.Length);

            Assert.AreEqual(gift.Id, gifts[0].Id);
            Assert.AreEqual(gift.Title, gifts[0].Title);
            Assert.AreEqual(gift.Description, gifts[0].Description);
            Assert.AreEqual(gift.Url, gifts[0].Url);
            Assert.AreEqual(gift.UserId, gifts[0].UserId);
        }

        [TestMethod]
        public async Task Get_UsingId_ReturnsGift()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateGift1();
            context.Gifts.Add(gift);
            context.SaveChanges();
            var uri = new Uri($"api/Gift/{gift.Id}", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Business.Dto.Gift giftAssert =
                JsonSerializer.Deserialize<Business.Dto.Gift>(jsonData, options);

            Assert.AreEqual(gift.Id, giftAssert.Id);
            Assert.AreEqual(gift.Title, giftAssert.Title);
            Assert.AreEqual(gift.Description, giftAssert.Description);
            Assert.AreEqual(gift.Url, giftAssert.Url);
            Assert.AreEqual(gift.UserId, giftAssert.UserId);
        }

        [TestMethod]
        public async Task Get_UsingInvalidId_NotFound()
        {
            // Arrange
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.GiftInput gift = Mapper.Map<Data.Gift, Business.Dto.Gift>(SampleData.CreateGift1());
            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesGift()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateGift1();
            context.Gifts.Add(gift);
            context.SaveChanges();

            Business.Dto.GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.Gift>(gift);
            giftInput.Title += "changed";
            giftInput.Description += "changed";
            giftInput.Url += "changed";

            string jsonData = JsonSerializer.Serialize(giftInput);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/Gift/{gift.Id}", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string jsonDataAssert = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Business.Dto.Gift giftAssert = JsonSerializer.Deserialize<Business.Dto.Gift>(jsonDataAssert, options);

            Assert.AreEqual(giftInput.Title, giftAssert.Title);
            Assert.AreEqual(giftInput.Description, giftAssert.Description);
            Assert.AreEqual(giftInput.Url, giftAssert.Url);
            Assert.AreEqual(giftInput.UserId, giftAssert.UserId);

            using ApplicationDbContext assertContext = Factory.GetDbContext();
            gift = assertContext.Gifts.Find(gift.Id);

            Assert.AreEqual(giftAssert.Id, gift.Id);
            Assert.AreEqual(giftAssert.Title, gift.Title);
            Assert.AreEqual(giftAssert.Description, gift.Description);
            Assert.AreEqual(giftAssert.Url, gift.Url);
            Assert.AreEqual(giftAssert.UserId, gift.UserId);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]
        [DataRow(nameof(Business.Dto.GiftInput.UserId))]
        public async Task Post_WithoutRequiredProperties_BadRequest(string propertyName)
        {
            // Arrange
            Data.Gift gift = SampleData.CreateGift1();

            //DTO
            Business.Dto.GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.Gift>(gift);
            System.Type inputType = typeof(Business.Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(giftInput, null);

            string jsonData = JsonSerializer.Serialize(giftInput);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/Gift", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Post_WithRequiredProperties_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateInigoMontoya();
            context.Users.Add(user);
            context.SaveChanges();

            Data.Gift gift = SampleData.CreateGift1();
            Business.Dto.GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.Gift>(gift);
            giftInput.UserId = user.Id;
            string jsonData = JsonSerializer.Serialize(giftInput);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/Gift", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string jsonDataAssert = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Business.Dto.Gift giftAssert = JsonSerializer.Deserialize<Business.Dto.Gift>(jsonDataAssert, options);

            Assert.AreEqual(giftInput.Title, giftAssert.Title);
            Assert.AreEqual(giftInput.Description, giftAssert.Description);
            Assert.AreEqual(giftInput.Url, giftAssert.Url);
            Assert.AreEqual(giftInput.UserId, giftAssert.UserId);

            using ApplicationDbContext assertContext = Factory.GetDbContext();
            gift = assertContext.Gifts.Find(1);

            Assert.AreEqual(giftAssert.Id, gift.Id);
            Assert.AreEqual(giftAssert.Title, gift.Title);
            Assert.AreEqual(giftAssert.Description, gift.Description);
            Assert.AreEqual(giftAssert.Url, gift.Url);
            Assert.AreEqual(giftAssert.UserId, gift.UserId);
        }

        [TestMethod]
        public async Task Delete_UsingInvalidId_NotFound()
        {
            //Arrange

            //Act
            Uri uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_UsingValidId_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateGift1();
            context.Gifts.Add(gift);
            context.SaveChanges();
            var uri = new Uri($"api/Gift/{gift.Id}", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode();
            using ApplicationDbContext assertContext = Factory.GetDbContext();

            List<Data.Gift> gifts = await assertContext.Gifts.ToListAsync();

            Assert.AreEqual(0, gifts.Count);
        }
    }
}
