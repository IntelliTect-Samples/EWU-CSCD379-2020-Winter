using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Business.Tests
{
    [TestClass]
    public class GiftServiceTests : EntityServiceTests<Business.Dto.Gift, Business.Dto.GiftInput, Data.Gift>
    {
        [TestInitialize]
        public void TestSetup()
        {
            //Gifts need related users, so create one
            using var dbContext = new ApplicationDbContext(Options);
            dbContext.Users.Add(new Data.User("first", "last"));
            dbContext.SaveChanges();
        }

        protected override Data.Gift CreateEntity()
            => new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        protected override GiftInput CreateInputDto()
        {
            return new GiftInput
            {
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                UserId = 1 //From the user we inserted in the initialize
            };
        }

        protected override IEntityService<Business.Dto.Gift, Business.Dto.GiftInput> GetService(ApplicationDbContext dbContext, IMapper mapper)
            => new GiftService(dbContext, mapper);
    }
}