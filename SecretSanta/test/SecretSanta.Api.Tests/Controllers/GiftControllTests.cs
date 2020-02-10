using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Business.Tests.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput,GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Gift,Business.Dto.GiftInput> CreateController()
            => new GiftController(new GiftInMemoryService());

        protected override Business.Dto.Gift CreateDto()
        {
            return new Business.Dto.Gift();
        }

        protected override Business.Dto.GiftInput CreateInputDto()
        {
            return SampleData.CreateLeSpatula();
        }

        protected override Data.Gift CreateEntity()
            => new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    }

    public class GiftInMemoryService : InMemoryEntityService<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput>, IGiftService
    {

    }
}
