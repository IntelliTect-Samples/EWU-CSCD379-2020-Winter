using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Controllers
{

    [TestClass]
    public class GiftControllerTests : EntityControllerTests<Gift>
    {

        protected override Mock<IEntityService<Gift>> CreateService() => new Mock<IEntityService<Gift>>();

        protected override EntityController<Gift> CreateController(IEntityService<Gift> service) =>
            new GiftController(service);

        protected override Gift CreateEntity() => new Gift("Tater Tot",
                                                           "tater-tot.com",
                                                           "A single Tater Tot for your enjoyment!",
                                                           new User("Tater", "Tot"));

    }

}
