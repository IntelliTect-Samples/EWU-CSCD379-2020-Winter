using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class GroupControllerTests : EntityControllerTests<Group>
    {

        protected override Mock<IEntityService<Group>> CreateService() => new Mock<IEntityService<Group>>();

        protected override EntityController<Group> CreateController(IEntityService<Group> service) =>
            new GroupController(service);

        protected override Group CreateEntity() => new Group("Dunder Mifflin Paper Company");

    }

}
