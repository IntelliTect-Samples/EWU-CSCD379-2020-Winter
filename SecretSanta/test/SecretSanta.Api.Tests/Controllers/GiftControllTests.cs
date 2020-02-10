using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Business.Tests.Dto;
using SecretSanta.Data;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Data.Gift, Business.Dto.Gift, Business.Dto.GiftInput, GiftInMemoryService>
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#pragma warning restore CS8618 // Justification: Both client and Factory are initialized in the TestSetup method, which has the TestInitialize attribute
        protected override BaseApiController<Business.Dto.Gift, Business.Dto.GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Business.Dto.Gift CreateDto()
        {
            return new Business.Dto.Gift()
            {
                Title = SampleData._LeSpatulaTitle,
                Description = SampleData._LeSpatulaDescription,
                Url = SampleData._LeSpatulaUrl
            };
        }

        protected override Business.Dto.GiftInput CreateInputDto()
        {
            return SampleData.CreateLeSpatula();
        }

        protected override Data.Gift CreateEntity()
            => new Data.Gift(SampleData._LeSpatulaTitle, SampleData._LeSpatulaUrl, SampleData._LeSpatulaDescription, new Data.User(SampleData._SpongebobFirstName, SampleData._SpongebobLastName));

        private  bool DTosAreEqual(Business.Dto.Gift dto1, Business.Dto.Gift dto2)
        {
            if (dto1 is null)
                throw new ArgumentNullException(nameof(dto1));
            if (dto2 is null)
                throw new ArgumentNullException(nameof(dto2));
            if (dto1.Title is null || dto2.Title is null)
                return false;
            if (dto1.Description is null || dto2.Description is null)
                return false;
            if (dto1.Url is null || dto2.Url is null)
                return false;

            if (dto1.Title.CompareTo(dto2.Title) != 0)
                return false;
            if (dto1.Description.CompareTo(dto2.Description) != 0)
                return false;
            if (dto1.Url.CompareTo(dto2.Url) != 0)
                return false;
            return true;
        }

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();

            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();

            Client = Factory.CreateClient();
            SeedData();
        }

        public void SeedData()
        {
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            for(int i = 0; i < 5; i++)
            {
                dbContext.Add(CreateEntity());
                dbContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]
        public async Task Get_AuthorsInDB_ReturnsAllAuthors()
        {
            //Arrange
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            Business.Dto.Gift excpectedGift = CreateDto();
            //Act
            Uri uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.GetAsync(uri);
            //Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(json, options);
            Assert.AreEqual<int>(5, gifts.Length);
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[0]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[1]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[2]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[3]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[4]));
        }
    }

    public class GiftInMemoryService : InMemoryEntityService<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput>, IGiftService
    {

    }
}
