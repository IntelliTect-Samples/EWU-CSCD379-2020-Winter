using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;

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
