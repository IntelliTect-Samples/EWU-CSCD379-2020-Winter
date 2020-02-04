using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using System;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : EntityControllerTests<Gift>
    {
        protected override EntityController<Gift> CreateController =>
            new GiftController(new MockGiftService());

        protected override Gift CreateEntity =>
            new Gift(Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
    }

    internal class IdGift : Gift
    {
        public IdGift(string title, string url, string description, User user, int id)
            : base(title, url, description, user) =>
            Id = id;
    }

    internal class MockGiftService : MockEntityService<Gift>, IGiftService
    {
        protected override Gift AddId(Gift gift, int id) =>
            new IdGift(gift.Title, gift.Url, gift.Description,
                    gift.User, id);
    }
}
