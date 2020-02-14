using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Business.Dto.Group, Business.Dto.GroupInput, Data.Group, GroupInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Group, Business.Dto.GroupInput, Data.Group> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Business.Dto.Group CreateDto()
        {
            throw new NotImplementedException();
        }

        protected override Data.Group CreateEntity()
            => new Data.Group(Guid.NewGuid().ToString());

        protected override GroupInput CreateInput()
        {
            throw new NotImplementedException();
        }
    }


    public class GroupInMemoryService : InMemoryEntityService<Business.Dto.Group, Business.Dto.GroupInput, Data.Group>, IGroupService
    {

    }
}
