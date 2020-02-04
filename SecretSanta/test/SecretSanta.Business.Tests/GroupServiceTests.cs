using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : EntityServiceTests<Group>
    {
        protected override Group CreateEntity() 
            => new Group(Guid.NewGuid().ToString());

        protected override IEntityService<Group> GetService(ApplicationDbContext dbContext, IMapper mapper) 
            => new GroupService(dbContext, mapper);
    }
}
