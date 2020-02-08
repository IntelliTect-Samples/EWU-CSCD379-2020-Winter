﻿using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<Business.Dto.User, Business.Dto.UserInput>
    {
        public UserController(IUserService userService)
            : base(userService)
        { }
    }
}
