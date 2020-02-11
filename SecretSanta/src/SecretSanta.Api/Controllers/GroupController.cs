using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseApiController<Group, GroupInput>
    {
        public GroupController(IGroupService groupService) 
            : base(groupService)
        { }
    }
}
