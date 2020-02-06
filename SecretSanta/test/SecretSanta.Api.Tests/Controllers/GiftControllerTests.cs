using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseApiControllerTests<Business.Dto.Gift, GiftInput, GiftInMemoryService>
    {
        protected override BaseApiController<Gift> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Gift CreateEntity()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        protected override Business.Dto.Gift CreateDto()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    }

    public class GiftInMemoryService : InMemoryEntityService<Dto.Gift, GiftInput>, IGiftService
    {

    }
}
