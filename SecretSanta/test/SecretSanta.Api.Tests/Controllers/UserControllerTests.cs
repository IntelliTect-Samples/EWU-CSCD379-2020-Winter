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
    public class UserControllerTests : BaseApiControllerTests<Business.Dto.User, UserInput, Data.User>
    {
        protected override Data.User CreateEntity()
            => new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        [TestMethod]
        public async Task Get_ReturnsUsers_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/User", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            Business.Dto.User[] users = JsonSerializer.Deserialize<Business.Dto.User[]>(json, options);

            Assert.AreEqual(user.Id, users[10].Id);
            Assert.AreEqual(user.FirstName, users[10].FirstName);
            Assert.AreEqual(user.LastName, users[10].LastName);
        }

        [TestMethod]
        public async Task Post_AddUser_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            UserInput userInput = new UserInput()
            {
                FirstName = "First",
                LastName = "Last",
            };

            string json = JsonSerializer.Serialize(userInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            Uri uri = new Uri("api/User/", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retirevedJson = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Business.Dto.User retirevedUser = JsonSerializer.Deserialize<Business.Dto.User>(retirevedJson, options);

            Assert.AreEqual(userInput.FirstName, retirevedUser.FirstName);
            Assert.AreEqual(userInput.LastName, retirevedUser.LastName);
        }

        [TestMethod]
        public async Task Put_UpdateUser_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            Business.Dto.UserInput userInput = Mapper.Map<Data.User, Business.Dto.UserInput>(user);
            userInput.FirstName = "updated first";
            userInput.LastName = "updated last";

            string json = JsonSerializer.Serialize(userInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/User/{user.Id}", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retirevedJson = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Business.Dto.User retirevedUser = JsonSerializer.Deserialize<Business.Dto.User>(retirevedJson, options);

            Assert.AreEqual(userInput.FirstName, retirevedUser.FirstName);
            Assert.AreEqual(userInput.LastName, retirevedUser.LastName);
        }

        [TestMethod]
        public async Task Put_NullValues_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            Business.Dto.UserInput userInput = Mapper.Map<Data.User, Business.Dto.UserInput>(user);
            userInput.FirstName = null;
            userInput.LastName = null;

            string json = JsonSerializer.Serialize(userInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/User/{user.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_RemoveUser_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user1 = SampleData.CreateSampleUser();
            Data.User user2 = SampleData.CreateSampleUser();
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/User/{user1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            List<Data.User> users = await context.Users.ToListAsync();

            Assert.AreEqual(11, users.Count);
        }

        [TestMethod]
        public async Task Delete_InvalidId_NotFound()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User user = SampleData.CreateSampleUser();
            context.Users.Add(user);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/User/{42}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}