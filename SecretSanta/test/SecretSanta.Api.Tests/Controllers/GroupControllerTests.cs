using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : EntityControllerTests<Group>
    {
        protected override EntityController<Group> CreateController =>
            new GroupController(new MockGroupService());

        protected override Group CreateEntity =>
            new Group(Guid.NewGuid().ToString());
    }

    internal class IdGroup : Group
    {
        public IdGroup(string title, int id) : base(title) =>
            Id = id;
    }

    internal class MockGroupService : MockEntityService<Group>, IGroupService
    {
        protected override Group AddId(Group group, int id) =>
            new IdGroup(group.Title, id);
    }
}
