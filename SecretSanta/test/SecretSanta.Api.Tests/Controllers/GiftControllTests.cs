using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Gift, GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Gift,Business.Dto.GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Gift CreateEntity()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    }

    public class GiftInMemoryService : InMemoryEntityService<Gift>, IGiftService
    {

    }
}
