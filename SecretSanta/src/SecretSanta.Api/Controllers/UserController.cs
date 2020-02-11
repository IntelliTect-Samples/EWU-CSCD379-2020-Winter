using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;
using SecretSanta.Business.Dto;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<User, UserInput>
    {
        public UserController(IUserService userService)
            : base(userService)
        { }
    }
}
