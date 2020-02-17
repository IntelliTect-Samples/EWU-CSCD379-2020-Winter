using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Gift, GiftInput, GiftInMemoryService>
    {
        protected override BaseApiController<Gift, GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Gift CreateEntity()
            => new Gift
            {
                Description = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Url = Guid.NewGuid().ToString(),
                UserId = 1
            };
    }

    public class GiftInMemoryService : InMemoryEntityService<Gift, GiftInput>, IGiftService
    {
        private int NextId { get; set; } 
        protected override Gift Convert(GiftInput dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));
            return new Gift
            {
                Id = NextId++,
                Description = dto.Description,
                Title = dto.Title,
                Url = dto.Url,
                UserId = dto.UserId
            };
        }
    }
}
