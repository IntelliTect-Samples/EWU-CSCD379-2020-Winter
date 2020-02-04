using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : EntityServiceTests<User>
    {
        protected override User CreateEntity()
            => new User(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

        protected override IEntityService<User> GetService(ApplicationDbContext dbContext, IMapper mapper)
            => new UserService(dbContext, mapper);
    }
}