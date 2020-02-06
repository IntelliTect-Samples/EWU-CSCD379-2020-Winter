using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : BaseApiController<Business.Dto.Group, Business.Dto.GroupInput>
    {
        public GroupController(IGroupService groupService) 
            : base(groupService)
        { }
    }
}
