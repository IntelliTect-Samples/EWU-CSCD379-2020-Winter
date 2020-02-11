using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, GroupInput, GroupInMemoryService>
    {
        protected override BaseApiController<Group, GroupInput> CreateController(GroupInMemoryService service)
            => new GroupController(service);

        protected override GroupInput CreateInputDto()
            => new GroupInput { Title = Guid.NewGuid().ToString() };

        protected override Group CreateDto(int id)
            => new Group { Id = id, Title = Guid.NewGuid().ToString() };

        [TestMethod]
        public async Task Post_InsertsGroup()
        {
            var service = new GroupInMemoryService();
            GroupInput entity = CreateInputDto();
            BaseApiController<Group, GroupInput> controller = CreateController(service);

            Group? result = await controller.Post(entity);

            Assert.AreEqual(entity.Title, result.Title);
            Assert.AreEqual(entity.Title, service.Items.Single().Title);
        }

        [TestMethod]
        public async Task Put_UpdatesGroup()
        {
            var service = new GroupInMemoryService();
            Group group1 = CreateDto(service.Items.Count + 1);
            service.Items.Add(group1);
            GroupInput groupInput = CreateInputDto();
            BaseApiController<Group, GroupInput> controller = CreateController(service);

            ActionResult<Group> result = await controller.Put(group1.Id, groupInput);
            OkObjectResult okResult = (result.Result as OkObjectResult)!;
            Group? groupResult = okResult?.Value as Group;


            Assert.AreEqual(group1.Id, groupResult!.Id);
            Assert.AreEqual(group1.Id, service.Items.Single().Id);
            Assert.AreEqual(groupInput.Title, groupResult.Title);
            Assert.AreEqual(groupInput.Title, service.Items.Single().Title);
        }
    }


    public class GroupInMemoryService : InMemoryEntityService<Group, GroupInput>, IGroupService
    {

    }
}
