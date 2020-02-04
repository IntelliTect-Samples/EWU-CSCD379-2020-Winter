using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests : EntityControllerTests<User>
    {
        protected override EntityController<User> CreateController =>
            new UserController(new MockUserService());

        protected override User CreateEntity =>
            new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }

    internal class IdUser : User
    {
        public IdUser(string firstName, string lastName, int id)
            : base(firstName, lastName) =>
            Id = id;
    }

    internal class MockUserService : MockEntityService<User>, IUserService
    {
        protected override User AddId(User user, int id) =>
            new IdUser(user.FirstName, user.LastName, id);
    }
}
