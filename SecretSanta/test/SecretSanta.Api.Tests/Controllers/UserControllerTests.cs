using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, UserInput, UserInMemoryService>
    {

        protected override BaseApiController<User, UserInput> CreateController(UserInMemoryService service) =>
            new UserController(service);

        protected override User CreateDto() =>
            new User {FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString()};

    }

    public class UserInMemoryService : InMemoryEntityService<User, UserInput>, IUserService
    {

    }

}
