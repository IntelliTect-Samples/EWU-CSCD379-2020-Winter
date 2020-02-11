using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using SecretSanta.Data;
using System.Net.Http;

namespace SecretSanta.Api.Tests.Controllers
{
    public abstract class BaseContollerTests
    {
#nullable disable // Initialized in TestSetup()
        protected SecretSantaWebApplicationFactory Factory { get; set; }
        protected HttpClient Client { get; set; }
#nullable enable

        protected IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();
            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup() =>
            Factory.Dispose();
    }
}
