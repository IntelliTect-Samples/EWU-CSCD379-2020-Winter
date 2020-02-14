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
    /// <summary>
    /// Extra Credit
    /// </summary>
    /// 
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Business.Dto.User, Business.Dto.UserInput, Data.User, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.User, Business.Dto.UserInput, Data.User> CreateController(UserInMemoryService service)
            => new UserController(service);

        

        protected override Data.User CreateEntity()
            => new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        protected override Business.Dto.User CreateDto()
        {
            return new Business.Dto.User
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            };
        }
        protected override UserInput CreateInput()
        {
            return new UserInput
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            };
        }

        [TestMethod]
        public async Task UserRead_Get_ReturnsUsers()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.User im = SampleData.CreateUserGaiusBaltar();
            context.Users.Add(im);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/User", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.User[] users =
                JsonSerializer.Deserialize<Business.Dto.User[]>(jsonData, options);

            Assert.AreEqual(im.Id, users[users.Length - 1].Id);
            Assert.AreEqual(im.FirstName, users[users.Length - 1].FirstName);
            Assert.AreEqual(im.LastName, users[users.Length - 1].LastName);
        }

        [TestMethod]
        public async Task UserUpdate_PutWithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.UserInput im = Mapper.Map<Data.User, Business.Dto.User>(SampleData.CreateUserKaraThrace());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/User/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task UserUpdate_PutWithId_UpdatesEntity()
        {
            // Arrange
            var entity = SampleData.CreateUserLauraRoslin();
            //entity.UserId = 1;
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Users.Add(entity);
            context.SaveChanges();

            Business.Dto.UserInput im = new Business.Dto.UserInput
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName

            };
            im.FirstName += "changed";
            im.LastName += "changed";

            string jsonData = JsonSerializer.Serialize(im);
            Console.WriteLine("jsonData: " + jsonData);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            Uri uri = new Uri($"api/User/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.User returnedUser = JsonSerializer.Deserialize<Business.Dto.User>(retunedJson, options);

            // Assert that returnedAuthor matches im values
            Assert.AreEqual(returnedUser.FirstName, im.FirstName);
            Assert.AreEqual(returnedUser.LastName, im.LastName);

            // Assert that returnedAuthor matches database value
            uri = new Uri($"api/User/{entity.Id}", UriKind.RelativeOrAbsolute);
            response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            retunedJson = await response.Content.ReadAsStringAsync();

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var returnedUser2 = JsonSerializer.Deserialize<Business.Dto.User>(retunedJson, options);
            Assert.AreEqual(returnedUser.FirstName, returnedUser2.FirstName);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.UserInput.FirstName))]
        [DataRow(nameof(Business.Dto.UserInput.LastName))]
        public async Task UserCreate_PostWithoutRequiredData_BadRequest(string propertyName)
        {
            // Arrange
            Data.User entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Users.Add(entity);
            context.SaveChanges();

            //DTO
            Business.Dto.UserInput im = Mapper.Map<Data.User, Business.Dto.User>(entity);
            System.Type inputType = typeof(Business.Dto.UserInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(im, null);

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("jsonData: " + jsonData);

            // Act
            Uri uri = new Uri($"api/User", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task UserDelete_InvalidId_Fails()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();

            //Act
            Uri uri = new Uri("api/User/9999", UriKind.RelativeOrAbsolute);
            HttpResponseMessage responseDelete = await Client.DeleteAsync(uri);

            //Assert
            Assert.AreEqual(responseDelete.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task UserDelete_ValidId_Deletes()
        {
            //Arrange
            Data.User entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Users.Add(entity);
            context.SaveChanges();

            Data.User first = await context.Users.FirstAsync();
            int count = await context.Users.CountAsync();

            //Act
            Uri uri = new Uri($"api/User/{first.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            using ApplicationDbContext contextAct = Factory.GetDbContext();
            int newCount = await context.Users.CountAsync();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(newCount, count - 1);

        }
    }

    public class UserInMemoryService : InMemoryEntityService<Business.Dto.User, Business.Dto.UserInput, Data.User>, IUserService
    {

    }
}
