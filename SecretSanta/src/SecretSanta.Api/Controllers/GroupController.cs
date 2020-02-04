using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    //https://localhost/api/Group
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : EntityController<Group>
    {
        public GroupController(IGroupService entityService) : base(entityService)
        {

        }
    }
}