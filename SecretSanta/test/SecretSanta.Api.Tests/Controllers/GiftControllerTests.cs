using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : BaseApiControllerTests<Business.Dto.Gift, GiftInput, GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Gift, GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        //protected override Gift CreateEntity()
        //    => new Gift(Guid.NewGuid().ToString(),
        //        Guid.NewGuid().ToString(),
        //        Guid.NewGuid().ToString(),
        //        new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

        protected override Business.Dto.Gift CreateDto()
            => Mapper.Map<Business.Dto.Gift>(new Data.Gift(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new Data.User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())));
    }

    public class GiftInMemoryService : InMemoryEntityService<Business.Dto.Gift, GiftInput>, IGiftService
    {
        Task<List<Business.Dto.Gift>> IEntityService<Business.Dto.Gift, GiftInput>.FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Business.Dto.Gift> IEntityService<Business.Dto.Gift, GiftInput>.FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Business.Dto.Gift> IEntityService<Business.Dto.Gift, GiftInput>.InsertAsync(GiftInput entity)
        {
            throw new NotImplementedException();
        }

        Task<Business.Dto.Gift?> IEntityService<Business.Dto.Gift, GiftInput>.UpdateAsync(int id, GiftInput entity)
        {
            throw new NotImplementedException();
        }
    }
}
