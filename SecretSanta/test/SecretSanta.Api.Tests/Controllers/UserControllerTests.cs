using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Tests.MockServices;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : ControllerTestBase<UserController, User>
    {
        protected override IEntityService<User> CreateService() => new MockUserService();
        protected override UserController CreateController(IEntityService<User> service)
        {
            return new UserController((IUserService)service);
        }

        protected override User CreateEntity() => new User("Inigo", "Montoya");
    }
}
