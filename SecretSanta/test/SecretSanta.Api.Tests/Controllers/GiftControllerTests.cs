using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Tests.MockServices;
using SecretSanta.Business.Services;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests : ControllerTestBase<GiftController, Gift>
    {
        protected override IEntityService<Gift> CreateService() => new MockGiftService();
        protected override GiftController CreateController(IEntityService<Gift> service)
        {
            return new GiftController((IGiftService)service);
        }

        protected override Gift CreateEntity() => new Gift("Ring Doorbell", "www.ring.com", "The doorbell that saw too much", new User("Inigo", "Montoya"));
    }
}
