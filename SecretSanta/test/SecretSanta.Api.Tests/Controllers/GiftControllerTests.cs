using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Gift = SecretSanta.Data.Gift;
using User = SecretSanta.Data.User;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests 
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
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
            Gift gift = SampleData.CreateCoolGift();
            context.Gifts.Add(gift);
            context.SaveChanges();
            var uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // Act
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            Business.Dto.Gift[] gifts =
                JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);
            Assert.AreEqual(1, gifts.Length);

            Assert.AreEqual(gift.Id, gifts[0].Id);
            Assert.AreEqual(gift.Title, gifts[0].Title);
            Assert.AreEqual(gift.Description, gifts[0].Description);
        }

                [TestMethod]
        public async Task Get_ByIdNotFound_NotFound()
        {
            var uri = new Uri($"api/Gift/1", UriKind.RelativeOrAbsolute);

            HttpResponseMessage response = await Client.GetAsync(uri);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [TestMethod]
        public async Task Put_WithInvalidId_NotFound()
        {
            GiftInput gift = Mapper.Map<Gift, Business.Dto.Gift>(SampleData.CreateCoolGift());
            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesEntity()
        {
            Gift entity = SampleData.CreateCoolGift();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            GiftInput gift = Mapper.Map<Gift, Business.Dto.Gift>(entity);
            var uri = new Uri("api/Gift/1", UriKind.RelativeOrAbsolute);

            gift.Title += "changed";

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var options = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
            };


            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();
            string returnedJson = await response.Content.ReadAsStringAsync();

            Gift returnedGift = JsonSerializer.Deserialize<Gift>(returnedJson, options);

            Assert.AreEqual(returnedGift.Title, gift.Title);
            Assert.AreEqual(returnedGift.Description, gift.Description);

        }

        [TestMethod]
        [DataRow(nameof(GiftInput.Title))]
        public async Task Post_WithNullValues_BadResult(string propertyName)
        {
            Data.Gift entity = SampleData.CreateCoolGift();

            GiftInput gift = Mapper.Map<Data.Gift, Business.Dto.Gift>(entity);
            Type inputType = typeof(GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(gift, null);
            var uri = new Uri("api/Gift/", UriKind.RelativeOrAbsolute);

            string jsonData = JsonSerializer.Serialize(gift);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [TestMethod]
        public async Task Post_WithValidInput_Success()
        {
            using ApplicationDbContext context = Factory.GetDbContext();
            User user = SampleData.CreateFredFlintstone();
            context.Users.Add(user);
            context.SaveChanges();

            Gift entity = SampleData.CreateCoolGift();

            GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.Gift>(entity);
            giftInput.UserId = user.Id;

            string jsonData = JsonSerializer.Serialize(giftInput);
            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri($"api/Gift/", UriKind.RelativeOrAbsolute);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string returnedJson = await response.Content.ReadAsStringAsync();
            using ApplicationDbContext assertContext = Factory.GetDbContext();
            Business.Dto.Gift responseGift =
                JsonSerializer.Deserialize<Business.Dto.Gift>(returnedJson, options);
            Data.Gift giftFromDb = await assertContext.Gifts.Where(g => g.Id == responseGift.Id).SingleOrDefaultAsync();

            Assert.AreEqual(giftFromDb.Title, responseGift.Title);
            Assert.AreEqual(giftFromDb.Description, responseGift.Description);
            Assert.AreEqual(giftFromDb.Url, responseGift.Url);
        }

        [TestMethod]
        public async Task Delete_GiftByValidId_Success()
        {
            using ApplicationDbContext context = Factory.GetDbContext();
            Gift gift = SampleData.CreateCoolGift();
            context.Gifts.Add(gift);
            context.SaveChanges();

            var uri = new Uri($"api/Gift/{gift.Id}", UriKind.RelativeOrAbsolute);

            HttpResponseMessage response = await Client.DeleteAsync(uri);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public async Task Delete_InvalidId_NotFound()
        {
            var uri = new Uri("api/Gift/1", UriKind.RelativeOrAbsolute);

            HttpResponseMessage response = await Client.DeleteAsync(uri);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

}
