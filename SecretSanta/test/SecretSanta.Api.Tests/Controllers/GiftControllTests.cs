using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
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
    public class GiftControllTests : BaseApiControllerTests<Business.Dto.Gift, Business.Dto.GiftInput, Data.Gift, GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Gift, Business.Dto.GiftInput, Data.Gift> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Data.Gift CreateEntity()
            => new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        protected override Business.Dto.Gift CreateDto()
        {
            return new Business.Dto.Gift
            {
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                UserId = 1
            };
        }

        protected override GiftInput CreateInput()
        {
            return new GiftInput
            {
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                UserId = 1
            };
        }

        [TestMethod]
        public async Task GiftRead_Get_ReturnsGifts()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift im = SampleData.CreateGiftViper();
            context.Gifts.Add(im);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift[] gifts =
                JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);
            //Assert.AreEqual(1, gifts.Length);

            Assert.AreEqual(im.Id, gifts[gifts.Length - 1].Id);
            Assert.AreEqual(im.Title, gifts[gifts.Length - 1].Title);
            Assert.AreEqual(im.Description, gifts[gifts.Length - 1].Description);
        }

        [TestMethod]
        public async Task GiftUpdate_PutWithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.GiftInput im = Mapper.Map<Data.Gift, Business.Dto.Gift>(SampleData.CreateGiftCylonDetector());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GiftUpdate_PutWithId_UpdatesEntity()
        {
            // Arrange
            var entity = SampleData.CreateGiftFTLDrive();
            //entity.UserId = 1;
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            Business.Dto.GiftInput im = new Business.Dto.GiftInput
            {
                Title = entity.Title,
                Description = entity.Description,
                Url = entity.Url,
                UserId = entity.UserId
                
            };
            im.Title += "changed";
            im.Description += "changed";

            string jsonData = JsonSerializer.Serialize(im);
            Console.WriteLine("jsonData: " + jsonData);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            Uri uri = new Uri($"api/Gift/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, options);

            // Assert that returnedAuthor matches im values
            Console.WriteLine("returnedGift: " + returnedGift.Title + " " + returnedGift.Description + " " + returnedGift.UserId);
            Assert.AreEqual(returnedGift.Title, im.Title);
            Assert.AreEqual(returnedGift.Description, im.Description);
            Assert.AreEqual(returnedGift.UserId, im.UserId);

            // Assert that returnedAuthor matches database value
            uri = new Uri($"api/Gift/{entity.Id}", UriKind.RelativeOrAbsolute);
            response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            retunedJson = await response.Content.ReadAsStringAsync();

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var returnedGift2 = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, options);
            Assert.AreEqual(returnedGift.Title, returnedGift2.Title);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]
        [DataRow(nameof(Business.Dto.GiftInput.Description))]
        public async Task GiftCreate_PostWithoutRequiredData_BadRequest(string propertyName)
        {
            // Arrange
            Data.Gift entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            //DTO
            Business.Dto.GiftInput im = Mapper.Map<Data.Gift, Business.Dto.Gift>(entity);
            System.Type inputType = typeof(Business.Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(im, null);

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("jsonData: " + jsonData);

            // Act
            Uri uri = new Uri($"api/Gift", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);
            
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task GiftDelete_InvalidId_Fails()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();

            //Act
            Uri uriDelete = new Uri("api/Gift/9999", UriKind.RelativeOrAbsolute);
            HttpResponseMessage responseDelete = await Client.DeleteAsync(uriDelete);

            //Assert
            Assert.AreEqual(responseDelete.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task GiftDelete_ValidId_Deletes()
        {
            //Arrange
            Data.Gift entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            Data.Gift first = await context.Gifts.FirstAsync();
            int count = await context.Gifts.CountAsync();

            //Act
            Uri uri = new Uri($"api/Gift/{first.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            using ApplicationDbContext contextAct = Factory.GetDbContext();
            int newCount = await context.Gifts.CountAsync();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(newCount, count - 1);
            
        }


    }

    public class GiftInMemoryService : InMemoryEntityService<Business.Dto.Gift, Business.Dto.GiftInput, Data.Gift>, IGiftService
    {

    }
}
