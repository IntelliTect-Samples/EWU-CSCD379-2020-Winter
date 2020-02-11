using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : BaseApiControllerTests<User, UserInput, UserInMemoryService>
    {
        protected override BaseApiController<User, UserInput> CreateController(UserInMemoryService service)
            => new UserController(service);

        protected override UserInput CreateInputDto()
            => new UserInput { FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString() };

        protected override User CreateDto(int id)
            => new User { Id = id, FirstName = Guid.NewGuid().ToString(), LastName = Guid.NewGuid().ToString() };

        [TestMethod]
        public async Task Post_InsertsUser()
        {
            var service = new UserInMemoryService();
            UserInput entity = CreateInputDto();
            BaseApiController<User, UserInput> controller = CreateController(service);

            User? result = await controller.Post(entity);

            Assert.AreEqual(entity.FirstName, result.FirstName);
            Assert.AreEqual(entity.FirstName, service.Items.Single().FirstName);
        }

        [TestMethod]
        public async Task Put_UpdatesUser()
        {
            var service = new UserInMemoryService();
            User user1 = CreateDto(service.Items.Count + 1);
            service.Items.Add(user1);
            UserInput userInput = CreateInputDto();
            BaseApiController<User, UserInput> controller = CreateController(service);

            ActionResult<User> result = await controller.Put(user1.Id, userInput);
            OkObjectResult okResult = (result.Result as OkObjectResult)!;
            User? userResult = okResult?.Value as User;

            Assert.AreEqual(user1.Id, userResult!.Id);
            Assert.AreEqual(user1.Id, service.Items.Single().Id);
            Assert.AreEqual(userInput.FirstName, userResult.FirstName);
            Assert.AreEqual(userInput.FirstName, service.Items.Single().FirstName);
        }
    }

    public class UserInMemoryService : InMemoryEntityService<User, UserInput>, IUserService
    {

    }
}
