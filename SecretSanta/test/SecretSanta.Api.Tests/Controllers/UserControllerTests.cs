using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Business.Dto.User, Business.Dto.UserInput, User, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.User, Business.Dto.UserInput, User> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    public class UserInMemoryService : InMemoryEntityService<Business.Dto.User, Business.Dto.UserInput, User>, IUserService
    {

    }
}
