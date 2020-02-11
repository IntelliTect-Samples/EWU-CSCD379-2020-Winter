using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseApiControllerTests<Gift, Business.Dto.Gift, Business.Dto.GiftInput, GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.GiftInput, Business.Dto.Gift> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Gift CreateEntity()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    }

    public class GiftInMemoryService : InMemoryEntityService<Gift, Business.Dto.GiftInput, Business.Dto.Gift>, IGiftService
    {

    }
}
