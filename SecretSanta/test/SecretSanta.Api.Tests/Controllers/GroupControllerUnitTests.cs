using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerUnitTests : BaseControllerUnitTests<Group, Business.Dto.Group, Business.Dto.GroupInput, GroupInMemoryService>
    {
        protected override BaseApiController<Business.Dto.GroupInput, Business.Dto.Group> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Group CreateEntity()
            => new Group(Guid.NewGuid().ToString());
    }


    public class GroupInMemoryService : InMemoryEntityService<Group, Business.Dto.GroupInput, Business.Dto.Group>, IGroupService
    {

    }
}
