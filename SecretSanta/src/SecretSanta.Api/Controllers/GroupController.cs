using SecretSanta.Business.Dto;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    public class GroupController : BaseApiController<Group, GroupInput>
    {
        public GroupController(IGroupService groupService)
            : base(groupService)
        { }
    }
}
