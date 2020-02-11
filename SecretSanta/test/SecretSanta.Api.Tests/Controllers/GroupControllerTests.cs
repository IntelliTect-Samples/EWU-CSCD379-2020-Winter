using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, GroupInput, GroupInMemoryService>
    {

        protected override BaseApiController<Group, GroupInput> CreateController(GroupInMemoryService service) =>
            new GroupController(service);

        protected override Group CreateDto() => new Group {Title = Guid.NewGuid().ToString()};

    }

    public class GroupInMemoryService : InMemoryEntityService<Group, GroupInput>, IGroupService
    {

    }

}
