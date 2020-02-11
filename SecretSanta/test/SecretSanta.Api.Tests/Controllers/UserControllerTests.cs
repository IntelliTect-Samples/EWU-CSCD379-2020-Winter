using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, Business.Dto.User, Business.Dto.UserInput, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.UserInput, Business.Dto.User> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    public class UserInMemoryService : InMemoryEntityService<User, Business.Dto.UserInput, Business.Dto.User>, IUserService
    {

    }
}
