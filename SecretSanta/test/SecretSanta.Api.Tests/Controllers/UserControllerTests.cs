using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    /*
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, UserInMemoryService>
    {
        protected override BaseApiController<User> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    public class UserInMemoryService : InMemoryEntityService<User>, IUserService
    {

    }
    */
}
