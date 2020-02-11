using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class BaseApiControllerTests
    {
        // Justification: Properties are initialized in the TestSetup
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected SecretSantaWebApplicationFactory Factory { get; set; }
        protected HttpClient Client { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        [TestInitialize]
        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();
            using ApplicationDbContext dbContext = Factory.GetDbContext();
            dbContext.Database.EnsureCreated();
            Client = Factory.CreateClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }
    }
}
