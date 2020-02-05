using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class UserControllerTests : EntityControllerTests<User>
    {

        protected override Mock<IEntityService<User>> CreateService() => new Mock<IEntityService<User>>();

        protected override EntityController<User> CreateController(IEntityService<User> service) =>
            new UserController(service);

        protected override User CreateEntity() => new User("Brett", "Henning");

    }

}
