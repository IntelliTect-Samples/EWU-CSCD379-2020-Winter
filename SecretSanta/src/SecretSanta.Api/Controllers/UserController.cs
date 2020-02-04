using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : EntityController<User>
    {
        public UserController(IUserService userService) : base(userService) { }
    }
}

