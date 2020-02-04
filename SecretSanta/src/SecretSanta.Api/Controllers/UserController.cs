using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : EntityController<User>
    {

        public UserController(IEntityService<User> service) : base(service)
        {
        }

    }

}
