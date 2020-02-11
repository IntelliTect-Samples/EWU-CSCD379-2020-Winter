using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class GiftControllerTests : BaseApiControllerTests<Gift, GiftInput, GiftInMemoryService>
    {

        protected override BaseApiController<Gift, GiftInput> CreateController(GiftInMemoryService service) =>
            new GiftController(service);

        protected override Gift CreateDto() => new Gift
        {
            Title       = Guid.NewGuid().ToString(),
            Url         = Guid.NewGuid().ToString(),
            Description = Guid.NewGuid().ToString(),
            UserId      = new User {FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString()}.Id
        };

    }

    public class GiftInMemoryService : InMemoryEntityService<Gift, GiftInput>, IGiftService
    {

    }

}
