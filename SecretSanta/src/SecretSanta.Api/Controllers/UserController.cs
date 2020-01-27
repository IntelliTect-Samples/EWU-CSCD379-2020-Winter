using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<User>
    {
        public UserController(IUserService userService)
            : base(userService)
        { }
    }
}
