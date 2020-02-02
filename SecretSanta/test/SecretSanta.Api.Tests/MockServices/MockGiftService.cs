using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.MockServices
{
    public class MockGiftService : MockService<Gift>, IEntityService<Gift>, IGiftService
    {
        protected override Gift MockEntity(Gift entity, int id) => new TestGift(entity, id);
    }

    public class TestGift : Gift
    {
        public TestGift(Gift gift, int id)
            : base(gift == null ? throw new ArgumentNullException(nameof(gift)) : gift.Title, gift.Url, gift.Description, gift.User)
        {
            if (gift == null)
                throw new ArgumentNullException(nameof(gift));
            Id = id;
        }
    }
}
