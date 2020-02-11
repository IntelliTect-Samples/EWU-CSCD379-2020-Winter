using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Business.Dto;
using System;
using SecretSanta.Business.Services;
using AutoMapper;
using Group = SecretSanta.Business.Dto.Group;
using SecretSanta.Business;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests : BaseApiControllerTests<Group, GroupInput, IGroupService>
    {

       // private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();
        protected override BaseApiController<Group, GroupInput> CreateController(IGroupService service)
            => new GroupController(service);

        protected override Group CreateDto() => new Group
        {
            Title = Guid.NewGuid().ToString(),
            Id = new Random().Next()
        };
           
    }
}
