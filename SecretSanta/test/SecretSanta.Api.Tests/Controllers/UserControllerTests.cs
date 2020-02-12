using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using Dto = SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    /* disabled cause not required
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, Dto.User, Dto.UserInput, UserInMemoryService>
    {
        protected override BaseApiController<Dto.User, Dto.UserInput> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    public class UserInMemoryService : InMemoryEntityService<User, Dto.User, Dto.UserInput>, IUserService
    {

    }
    */
}
