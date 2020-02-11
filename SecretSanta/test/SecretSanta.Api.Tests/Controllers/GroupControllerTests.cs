using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Business.Dto.Group, Business.Dto.GroupInput, Group, GroupInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Group, Business.Dto.GroupInput, Group> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Group CreateEntity()
            => new Group(Guid.NewGuid().ToString());
    }


    public class GroupInMemoryService : InMemoryEntityService<Business.Dto.Group, Business.Dto.GroupInput, Group>, IGroupService
    {

    }
}
