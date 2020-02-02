using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Tests.MockServices;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : ControllerTestBase<GroupController, Group>
    {
        protected override IEntityService<Group> CreateService() => new MockGroupService();
        protected override GroupController CreateController(IEntityService<Group> service)
        {
            return new GroupController((IGroupService)service);
        }

        protected override Group CreateEntity() => new Group("Forest Club");
    }
}
