using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseApiController<GroupInput, Group>
    {
        public GroupController(IGroupService groupService) 
            : base(groupService)
        { }
    }
}
