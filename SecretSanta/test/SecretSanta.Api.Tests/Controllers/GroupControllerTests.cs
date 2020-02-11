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
    public class GroupControllerTests : BaseApiControllerTests<Business.Dto.Group, GroupInput, Data.Group>
    {
        protected override Data.Group CreateEntity()
            => new Data.Group(Guid.NewGuid().ToString());

        [TestMethod]
        public async Task Get_ReturnsGroups_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group = SampleData.CreateSampleGroup();
            context.Groups.Add(group);
            context.SaveChanges();

            // Act
            Uri uri = new Uri("api/Group", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);

            // Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            Business.Dto.Group[] groups = JsonSerializer.Deserialize<Business.Dto.Group[]>(json, options);

            Assert.AreEqual(group.Id, groups[10].Id);
            Assert.AreEqual(group.Title, groups[10].Title);
        }

        [TestMethod]
        public async Task Post_AddGroup_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            GroupInput groupInput = new GroupInput()
            {
                Title = "Title",
            };

            string json = JsonSerializer.Serialize(groupInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            Uri uri = new Uri("api/Group/", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retirevedJson = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Business.Dto.Group retirevedGroup = JsonSerializer.Deserialize<Business.Dto.Group>(retirevedJson, options);

            Assert.AreEqual(groupInput.Title, retirevedGroup.Title);
        }

        [TestMethod]
        public async Task Put_UpdateGroup_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group = SampleData.CreateSampleGroup();
            context.Groups.Add(group);
            context.SaveChanges();

            Business.Dto.GroupInput groupInput = Mapper.Map<Data.Group, Business.Dto.GroupInput>(group);
            groupInput.Title = "updated title";

            string json = JsonSerializer.Serialize(groupInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/Group/{group.Id}", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retirevedJson = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Business.Dto.Group retirevedGroup = JsonSerializer.Deserialize<Business.Dto.Group>(retirevedJson, options);

            Assert.AreEqual(groupInput.Title, retirevedGroup.Title);
        }

        [TestMethod]
        public async Task Put_NullValues_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group = SampleData.CreateSampleGroup();
            context.Groups.Add(group);
            context.SaveChanges();

            Business.Dto.GroupInput groupInput = Mapper.Map<Data.Group, Business.Dto.GroupInput>(group);
            groupInput.Title = null;

            string json = JsonSerializer.Serialize(groupInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await Client.PutAsync($"api/Group/{group.Id}", stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_RemoveGroup_Success()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group1 = SampleData.CreateSampleGroup();
            Data.Group group2 = SampleData.CreateSampleGroup();
            context.Groups.Add(group1);
            context.Groups.Add(group2);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/Group/{group1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            List<Data.Group> groups = await context.Groups.ToListAsync();

            Assert.AreEqual(11, groups.Count);
        }

        [TestMethod]
        public async Task Delete_InvalidId_NotFound()
        {
            // Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            Data.Group group = SampleData.CreateSampleGroup();
            context.Groups.Add(group);
            context.SaveChanges();

            // Act
            HttpResponseMessage response = await Client.DeleteAsync($"api/Group/{42}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
