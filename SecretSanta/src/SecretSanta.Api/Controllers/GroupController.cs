using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : EntityController<Group>
    {

        public GroupController(IEntityService<Group> service) : base(service)
        {
        }

    }

}
