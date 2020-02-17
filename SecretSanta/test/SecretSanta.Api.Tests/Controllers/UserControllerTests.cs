using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, UserInput, UserInMemoryService>
    {
        protected override BaseApiController<User, UserInput> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override User CreateEntity()
            => new User
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            };
    }

    public class UserInMemoryService : InMemoryEntityService<User, UserInput>, IUserService
    {
        private int NextId { get; set; }

        protected override User Convert(UserInput dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            return new User
            {
                Id = NextId++,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }
    }
}
