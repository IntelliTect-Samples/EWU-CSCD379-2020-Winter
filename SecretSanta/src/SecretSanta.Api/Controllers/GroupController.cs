using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : EntityController<Group>
    {
        public GroupController(IGroupService groupService) : base(groupService) { }
    }
}

