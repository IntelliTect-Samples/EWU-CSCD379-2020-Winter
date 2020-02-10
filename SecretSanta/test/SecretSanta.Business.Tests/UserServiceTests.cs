using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class UserServiceTests : EntityServiceTests<Business.Dto.User, Business.Dto.UserInput, Data.User>
    {
        protected override Data.User CreateEntity()
            => new Data.User(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

        protected override Business.Dto.UserInput CreateInputDto()
        {
            return new Business.Dto.UserInput
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString()
            };
        }

        protected override IEntityService<Business.Dto.User, Business.Dto.UserInput> GetService(ApplicationDbContext dbContext, IMapper mapper)
            => new UserService(dbContext, mapper);
    }
}