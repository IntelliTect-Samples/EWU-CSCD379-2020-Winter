using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : EntityServiceTests<Gift>
    {
        protected override Gift CreateEntity()
            => new Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        protected override IEntityService<Gift> GetService(ApplicationDbContext dbContext, IMapper mapper)
            => new GiftService(dbContext, mapper);
    }
}