using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using System;
using User = SecretSanta.Business.Dto.User;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, UserInput, IUserService>
    {
       // private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();
        protected override BaseApiController<User, UserInput> CreateController(IUserService service)
            => new UserController(service);

        protected override User CreateDto()
            => new User{
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Id = new Random().Next(),
                SantaId = new Random().Next()
            };

    }

}
