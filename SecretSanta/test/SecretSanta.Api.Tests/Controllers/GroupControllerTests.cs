using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, GroupInMemoryService>
    {
        protected override BaseApiController<Group> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override Group CreateEntity()
            => new Group(Guid.NewGuid().ToString());
    }


    public class GroupInMemoryService : InMemoryEntityService<Group>, IGroupService
    {

    }
}
