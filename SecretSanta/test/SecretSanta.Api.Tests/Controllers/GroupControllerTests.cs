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
    public class GroupControllerTests : BaseApiControllerTests<Business.Dto.Group, Business.Dto.GroupInput, Data.Group, GroupInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Group, Business.Dto.GroupInput, Data.Group> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        

        protected override Data.Group CreateEntity()
            => new Data.Group(Guid.NewGuid().ToString());

        protected override Business.Dto.Group CreateDto()
        {
            return new Business.Dto.Group
            {
                Title = Guid.NewGuid().ToString()
            };
        }
        protected override GroupInput CreateInput()
        {
            return new GroupInput
            {
                Title = Guid.NewGuid().ToString()
            };
        }

        [TestMethod]
        public async Task GroupRead_Get_ReturnsGroups()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group im = SampleData.CreateGroupCylonShip();
            context.Groups.Add(im);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/Group", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string jsonData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Group[] groups =
                JsonSerializer.Deserialize<Business.Dto.Group[]>(jsonData, options);

            Assert.AreEqual(im.Id, groups[groups.Length - 1].Id);
            Assert.AreEqual(im.Title, groups[groups.Length - 1].Title);
            
        }

        [TestMethod]
        public async Task GroupUpdate_PutWithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.GroupInput im = Mapper.Map<Data.Group, Business.Dto.Group>(SampleData.CreateGroupColonialFleet());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new Uri("api/Group/42", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GroupUpdate_PutWithId_UpdatesEntity()
        {
            // Arrange
            var entity = SampleData.CreateGroupColonialFleet();
            //entity.UserId = 1;
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Groups.Add(entity);
            context.SaveChanges();

            Business.Dto.GroupInput im = new Business.Dto.GroupInput
            {
                Title = entity.Title

            };
            im.Title += "changed";
            

            string jsonData = JsonSerializer.Serialize(im);
            Console.WriteLine("jsonData: " + jsonData);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Act
            Uri uri = new Uri($"api/Group/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Group returnedGroup = JsonSerializer.Deserialize<Business.Dto.Group>(retunedJson, options);

            // Assert that returnedAuthor matches im values
            Assert.AreEqual(returnedGroup.Title, im.Title);
            

            // Assert that returnedAuthor matches database value
            uri = new Uri($"api/Group/{entity.Id}", UriKind.RelativeOrAbsolute);
            response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            retunedJson = await response.Content.ReadAsStringAsync();

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var returnedGroup2 = JsonSerializer.Deserialize<Business.Dto.Group>(retunedJson, options);
            Assert.AreEqual(returnedGroup.Title, returnedGroup2.Title);
        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GroupInput.Title))]
        public async Task GroupCreate_PostWithoutRequiredData_BadRequest(string propertyName)
        {
            // Arrange
            Data.Group entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Groups.Add(entity);
            context.SaveChanges();

            //DTO
            Business.Dto.GroupInput im = Mapper.Map<Data.Group, Business.Dto.Group>(entity);
            System.Type inputType = typeof(Business.Dto.GroupInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(im, null);

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            Console.WriteLine("jsonData: " + jsonData);

            // Act
            Uri uri = new Uri($"api/Group", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task GroupDelete_InvalidId_Fails()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();

            //Act
            Uri uriDelete = new Uri("api/Group/9999", UriKind.RelativeOrAbsolute);
            HttpResponseMessage responseDelete = await Client.DeleteAsync(uriDelete);

            //Assert
            Assert.AreEqual(responseDelete.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task GroupDelete_ValidId_Deletes()
        {
            //Arrange
            Data.Group entity = CreateEntity();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Groups.Add(entity);
            context.SaveChanges();

            Data.Group first = await context.Groups.FirstAsync();
            int count = await context.Groups.CountAsync();

            //Act
            Uri uri = new Uri($"api/Group/{first.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.DeleteAsync(uri);

            using ApplicationDbContext contextAct = Factory.GetDbContext();
            int newCount = await context.Groups.CountAsync();
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(newCount, count - 1);

        }
    }


    public class GroupInMemoryService : InMemoryEntityService<Business.Dto.Group, Business.Dto.GroupInput, Data.Group>, IGroupService
    {

    }
}
