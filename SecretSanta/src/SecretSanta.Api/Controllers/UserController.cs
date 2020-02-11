using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<Business.Dto.User, Business.Dto.UserInput, User>
    {
        public UserController(IUserService userService)
            : base(userService)
        { }
    }
}
