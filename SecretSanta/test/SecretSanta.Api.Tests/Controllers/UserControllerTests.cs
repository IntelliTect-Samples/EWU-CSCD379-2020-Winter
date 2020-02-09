using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Business.Dto.User, UserInput, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.User, UserInput> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    public class UserInMemoryService : InMemoryEntityService<User>, IUserService
    {

    }
}
