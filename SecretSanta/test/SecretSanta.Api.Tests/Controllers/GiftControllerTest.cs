using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : EntityControllerTest<Gift>
    {
        public GiftControllerTests() : base(new TestableGiftService())
        {

        }
        protected override Gift CreateInstance()
        {
            return SampleData.CreateGift1();
        }
        private class TestableGiftService : EntityService<Gift>
        {
            protected override Gift CreateWithId(Gift entity, int id)
            {
                return new TestGift(entity, id);
            }
        }
        private class TestGift : Gift
        {
            public TestGift(Gift entity, int id)
                : base(entity.Title, entity.Url, entity.Description, entity.User)
            {
                Id = id;
            }
        }
    }
}
