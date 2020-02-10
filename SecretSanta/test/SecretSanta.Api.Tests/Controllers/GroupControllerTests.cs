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
    public class GroupControllerTests : BaseApiControllerTests<Data.Group,Business.Dto.Group,Business.Dto.GroupInput, GroupInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Group,Business.Dto.GroupInput> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Data.Group CreateEntity()
            => new Data.Group(SampleData._JellySpottersTitle);

        protected override Business.Dto.Group CreateDto()
        {
            return new Business.Dto.Group();
        }

        protected override Business.Dto.GroupInput CreateInputDto()
        {
            return SampleData.CreateJellySpotters();
        }

    }


    public class GroupInMemoryService : InMemoryEntityService<Data.Group,Business.Dto.Group,Business.Dto.GroupInput>, IGroupService
    {

    }
}
