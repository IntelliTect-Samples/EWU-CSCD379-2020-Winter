using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
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
    public class GiftControllTests : BaseApiControllerTests<Business.Dto.Gift, GiftInput, Data.Gift>
    {
        protected override Data.Gift CreateEntity()
            => new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        [TestMethod]
        public async Task Get_ReturnsGifts_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateSampleGift();            
            context.Gifts.Add(gift);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(json, options);

            Assert.AreEqual(gift.Id, gifts[10].Id);
            Assert.AreEqual(gift.Title, gifts[10].Title);
            Assert.AreEqual(gift.Description, gifts[10].Description);
            Assert.AreEqual(gift.Url, gifts[10].Url);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]
        [DataRow(nameof(Business.Dto.GiftInput.UserId))]
        public async Task Post_WithoutRequiredPorperties_BadResult(string propertyName)
        {
            // Arrange
            Data.Gift entity = SampleData.CreateSampleGift();

            GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.Gift>(entity);
            System.Type inputType = typeof(GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(giftInput, null);

            string json = JsonSerializer.Serialize(giftInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PostAsync("api/Gift/", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_UpdateGift_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateSampleGift();
            context.Gifts.Add(gift);
            context.SaveChanges();

            Business.Dto.GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(gift);
            giftInput.Title = "updated title";
            giftInput.Description = "updated desc";
            giftInput.Url = "updated url";

            string json = JsonSerializer.Serialize(giftInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{gift.Id}", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retirevedJson = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Business.Dto.Gift retirevedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retirevedJson, options);

            Assert.AreEqual(giftInput.Title, retirevedGift.Title);
            Assert.AreEqual(giftInput.Description, retirevedGift.Description);
            Assert.AreEqual(giftInput.Url, retirevedGift.Url);
        }

        [TestMethod]
        public async Task Put_NullValues_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateSampleGift();
            context.Gifts.Add(gift);
            context.SaveChanges();

            Business.Dto.GiftInput giftInput = Mapper.Map<Data.Gift, Business.Dto.GiftInput>(gift);
            giftInput.Title = null;
            giftInput.Description = null;
            giftInput.Url = null;

            string json = JsonSerializer.Serialize(giftInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/Gift/{gift.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_RemoveGift_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift1 = SampleData.CreateSampleGift();
            Data.Gift gift2 = SampleData.CreateSampleGift();
            context.Gifts.Add(gift1);
            context.Gifts.Add(gift2);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/Gift/{gift1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            List<Data.Gift> gifts = await context.Gifts.ToListAsync();

            Assert.AreEqual(11, gifts.Count);
        }

        [TestMethod]
        public async Task Delete_InvalidId_NotFound()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Gift gift = SampleData.CreateSampleGift();
            context.Gifts.Add(gift);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/Gift/{42}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}