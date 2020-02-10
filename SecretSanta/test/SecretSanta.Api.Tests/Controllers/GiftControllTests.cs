using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Business.Tests.Dto;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllTests : BaseApiControllerTests<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput,GiftInMemoryService>
    {
        protected override BaseApiController<Business.Dto.Gift,Business.Dto.GiftInput> CreateController(GiftInMemoryService service)
            => new GiftController(service);

        protected override Business.Dto.Gift CreateDto()
        {
            return new Business.Dto.Gift();
        }

        protected override Business.Dto.GiftInput CreateInputDto()
        {
            return SampleData.CreateLeSpatula();
        }

        protected override Data.Gift CreateEntity()
            => new Data.Gift(SampleData._LeSpatulaTitle, SampleData._LeSpatulaUrl, SampleData._LeSpatulaDescription, new Data.User(SampleData._SpongebobFirstName, SampleData._SpongebobLastName));

        protected override bool DTosAreEqual(Business.Dto.Gift dto1, Business.Dto.Gift dto2)
        {
            if (dto1 is null)
                throw new ArgumentNullException(nameof(dto1));
            if (dto2 is null)
                throw new ArgumentNullException(nameof(dto2));
            if (dto1.Title is null || dto2.Title is null)
                return false;
            if (dto1.Description is null || dto2.Description is null)
                return false;

            if (dto1.Title.CompareTo(dto2.Title) != 0)
                return false;
            if (dto1.Description.CompareTo(dto2.Description) != 0)
                return false;
            return true;
        }
    }

    public class GiftInMemoryService : InMemoryEntityService<Data.Gift,Business.Dto.Gift,Business.Dto.GiftInput>, IGiftService
    {

    }
}
