using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GroupServiceTests : EntityServiceTests<Business.Dto.Group, Business.Dto.GroupInput, Data.Group>
    {
        protected override Data.Group CreateEntity() 
            => new Data.Group(Guid.NewGuid().ToString());

        protected override GroupInput CreateInputDto()
        {
            return new GroupInput
            {
                Title = Guid.NewGuid().ToString()
            };
        }

        protected override IEntityService<Business.Dto.Group, Business.Dto.GroupInput> GetService(ApplicationDbContext dbContext, IMapper mapper) 
            => new GroupService(dbContext, mapper);
    }
}
