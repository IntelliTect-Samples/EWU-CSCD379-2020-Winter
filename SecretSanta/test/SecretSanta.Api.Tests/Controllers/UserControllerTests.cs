using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Business.Dto.User, Business.Dto.UserInput, Data.User, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.User, Business.Dto.UserInput, Data.User> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override Business.Dto.User CreateDto()
        {
            throw new NotImplementedException();
        }

        protected override Data.User CreateEntity()
            => new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        protected override UserInput CreateInput()
        {
            throw new NotImplementedException();
        }
    }

    public class UserInMemoryService : InMemoryEntityService<Business.Dto.User, Business.Dto.UserInput, Data.User>, IUserService
    {

    }
}
