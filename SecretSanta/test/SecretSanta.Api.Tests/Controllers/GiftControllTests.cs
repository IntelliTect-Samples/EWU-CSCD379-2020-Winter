using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using Dto = SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Gift, Dto.Gift, Dto.GiftInput, GiftInMemoryService>
    {
        protected override BaseApiController<Dto.Gift, Dto.GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Gift CreateEntity()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        [TestMethod]
        public async Task Get_FetchesAllItems()
        {

            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift item = CreateEntity();
            context.Gifts.Add(item);
            context.SaveChanges();

            // Act
            var uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
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
            Assert.AreEqual(1, gifts.Length);

            Assert.AreEqual(item.Id, gifts[0].Id);
            Assert.AreEqual(item.Title, gifts[0].Title);
            Assert.AreEqual(item.Description, gifts[0].Description);
            Assert.AreEqual(item.Url, gifts[0].Url);
        }

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            var e = CreateEntity();
            Dto.GiftInput item = Mapper.Map<Gift, Dto.Gift>(e);
            string jsonData = JsonSerializer.Serialize(item);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Gift/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [TestMethod]
        public async Task Put_WithId_UpdatesEntity()
        {
            // Arrange
            var entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            Dto.GiftInput im = new Dto.GiftInput
            {
                Title = entity.Title,
                Description = entity.Description,
                Url = entity.Url,
                UserId = entity.UserId
            };
            im.Title += "changed";
            im.Description += "changed";
            im.Url += "changed";

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var uri = new Uri($"api/Gift/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Dto.Gift returnedGift = JsonSerializer.Deserialize<Dto.Gift>(retunedJson, options);

            // Assert that returnedAuthor matches im values
            // Why?

            // Assert that returnedAuthor matches database value
            // Why?
        }

        [TestMethod]
        [DataRow(nameof(Dto.GiftInput.Title))]
        [DataRow(nameof(Dto.GiftInput.Description))]
        [DataRow(nameof(Dto.GiftInput.Url))]
        public async Task Post_WithoutRequiredField_BadResult(string propertyName)
        {
            // Arrange
            Gift entity = CreateEntity();

            //DTO
            Dto.GiftInput item = Mapper.Map<Gift, Dto.Gift>(entity);
            System.Type inputType = typeof(Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(item, null);

            string jsonData = JsonSerializer.Serialize(item);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            var uri = new Uri($"api/Gift/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }

    public class GiftInMemoryService : InMemoryEntityService<Gift, Dto.Gift, Dto.GiftInput>, IGiftService
    {

    }
}
