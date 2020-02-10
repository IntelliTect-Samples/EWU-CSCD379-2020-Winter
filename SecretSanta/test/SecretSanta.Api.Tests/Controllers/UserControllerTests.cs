using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Business.Tests.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<Data.User,Business.Dto.User,Business.Dto.UserInput, UserInMemoryService>
    {
        protected override BaseApiController<Business.Dto.User,Business.Dto.UserInput> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override Data.User CreateEntity()
            => new Data.User(SampleData._MrKrabsFirstName, SampleData._MrKrabsLastName);

        protected override Business.Dto.UserInput CreateInputDto()
        {
            return SampleData.CreateMrKrabs();
        }

        protected override Business.Dto.User CreateDto()
        {
            return new Business.Dto.User();
        }

       
    }

    public class UserInMemoryService : InMemoryEntityService<Data.User,Business.Dto.User,Business.Dto.UserInput>, IUserService
    {

    }
}
