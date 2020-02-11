using Microsoft.AspNetCore.Http;
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
using System.Linq;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Data.Gift, Business.Dto.Gift, Business.Dto.GiftInput, GiftInMemoryService>
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#pragma warning restore CS8618 // Justification: Both client and Factory are initialized in the TestSetup method, which has the TestInitialize attribute
        private readonly JsonSerializerOptions _JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

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

        private Business.Dto.Gift CreateDifferentDto()
        {
            return new Business.Dto.Gift()
            {
                Title = SampleData._MoneyTitle,
                Description = SampleData._MoneyDescription,
                Url = SampleData._MoneyUrl
            };
        }
           

        protected override Business.Dto.GiftInput CreateInputDto()
        {
            return SampleData.CreateLeSpatula();
        }

        protected override Data.Gift CreateEntity()
            => new Data.Gift(SampleData._LeSpatulaTitle, SampleData._LeSpatulaUrl, SampleData._LeSpatulaDescription, new Data.User(SampleData._SpongebobFirstName, SampleData._SpongebobLastName));

        private Data.Gift CreateDifferentEntity()
            => new Data.Gift(SampleData._MoneyTitle, SampleData._MoneyUrl, SampleData._MoneyDescription, new Data.User(SampleData._MrKrabsFirstName, SampleData._MrKrabsLastName));


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
            Uri uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            //Act
            HttpResponseMessage response = await Client.GetAsync(uri);
            //Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(json, _JsonSerializerOptions);
            Assert.AreEqual<int>(5, gifts.Length);
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[0]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[1]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[2]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[3]));
            Assert.IsTrue(DTosAreEqual(excpectedGift, gifts[4]));
        }

        [TestMethod]
        public async Task Get_ByID_Success()
        {
            //Arrange
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            Data.Gift entity = CreateDifferentEntity();
            dbContext.Gifts.Add(entity);
            dbContext.SaveChanges();
            Business.Dto.Gift expectedGift = CreateDifferentDto();
            Uri uri = new Uri("api/Gift/6", UriKind.RelativeOrAbsolute);
            //Act
            HttpResponseMessage response = await Client.GetAsync(uri);
            //Assert
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Business.Dto.Gift gift = JsonSerializer.Deserialize<Business.Dto.Gift>(json, _JsonSerializerOptions);
            Assert.IsTrue(DTosAreEqual(expectedGift,gift));
        }

        [TestMethod]
        public async Task Get_ByIDDoesntExist_Failure()
        {
            //Arrange
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            Uri uri = new Uri("api/Gift/6", UriKind.RelativeOrAbsolute);
            //Act
            HttpResponseMessage response = await Client.GetAsync(uri);
            //Assert
            Assert.AreEqual<System.Net.HttpStatusCode>(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Post_ValidDtoInput_Success()
        {
            //Arrange
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            Uri uri = new Uri("api/Gift", UriKind.RelativeOrAbsolute);
            Business.Dto.GiftInput giftInput = CreateInputDto();
            string json = JsonSerializer.Serialize(giftInput);
            using StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            //Act
            HttpResponseMessage httpResponse = await Client.PostAsync(uri, stringContent);
            //Assert
            httpResponse.EnsureSuccessStatusCode();
        }
    }

    public class GiftInMemoryService : InMemoryEntityService<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput>, IGiftService
    {

    }
}
