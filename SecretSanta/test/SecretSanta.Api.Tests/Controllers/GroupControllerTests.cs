using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using Dto = SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
   [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, Dto.Group, Dto.GroupInput, GroupInMemoryService>
    {
        protected override BaseApiController<Dto.Group, Dto.GroupInput> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Group CreateEntity()
            => new Group(Guid.NewGuid().ToString());
    }


    public class GroupInMemoryService : InMemoryEntityService<Group, Dto.Group, Dto.GroupInput>, IGroupService
    {

    }

}
